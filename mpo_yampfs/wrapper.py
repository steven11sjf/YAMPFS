import json
import os
import platform
import shutil
import subprocess
import sys
import tempfile
from collections.abc import Callable
from contextlib import contextmanager
from pathlib import Path

# TODO: revise this to be cleaner. Following things to keep in mind here
# 1. AFAIK the DLLs must be in path before loading the CLR. test whether that's truly the case.
#    Maybe we can remove from path when we're done?
# 2. While we figured out how to deal with non system installations via the DOTNET_ROOT env var,
#    should we also provide a way to just pass a `dotnet_root` param to the wrapper which gets
#    passed to pythonnet?
dotnet_os = "unknown"
dotnet_arch = "unknown"
system = platform.system()
if system == "Windows":
    dotnet_os = "win"
elif system == "Darwin":
    dotnet_os = "osx"
elif system == "Linux":
    dotnet_os = "linux"  # Might break for musl, I dont care.
else:
    raise ValueError("Couldn't determine the OS handle for dotnet cleanup!")

arch = platform.machine()
if arch == "AMD64" or arch == "x86_64":
    dotnet_arch = "x64"
elif arch == "arm64" or arch == "aarch64":
    dotnet_arch = "arm64"
else:
    raise ValueError("Couldn't determine the architecture handle for dotnet cleanup!")

dotnet_rid = dotnet_os + "-" + dotnet_arch

yampfs_path = os.fspath(Path(__file__).with_name(name="yampfs"))
sys.path.append(yampfs_path)
for dll in Path(__file__).parent.joinpath("yampfs").iterdir():
    if not dll.is_file():
        continue
    sys.path.append(os.fspath(dll))

from pythonnet import load


class YampfsException(Exception):
    pass


# TODO: add docstrings for methods when not in alpha and has stableish API
class Wrapper:
    def __init__(self, lib):
        self.csharp_patcher = lib

    def get_csharp_version(self) -> str:
        return self.csharp_patcher.Version

    # TODO: ADD TESTS!!!
    def patch_game(
        self,
        input_path: Path,
        output_path: Path,
        patch_data: dict,
        progress_update: Callable[[str, float], None],
    ):
        with tempfile.TemporaryDirectory(delete=False) as tempdir:# Copy to input dir to temp dir first to do operations there
            progress_update("Copying to temporary path...", 0)
            shutil.copytree(input_path, tempdir.name, dirs_exist_ok=True)

            # Get data.win path. Both of these *need* to be strings, as otherwise patcher won't accept them.
            output_data_win: str = os.fspath(
                _prepare_environment_and_get_data_win_path(tempdir.name)
            )
            input_data_win: str = shutil.move(output_data_win, output_data_win + "_orig")
            input_data_win_path = Path(input_data_win)

            # Temp write patch_data into json file for yampfs later
            progress_update("Creating json file...", 0.3)
            json_file: str = os.fspath(
                input_data_win_path.parent.joinpath("yampfs-data.json")
            )
            with open(json_file, "w+") as f:
                f.write(json.dumps(patch_data, indent=2))

            # Patch data.win
            progress_update("Patching data file...", 0.6)
            self.csharp_patcher.Main(input_data_win, output_data_win, json_file)

            # Move temp dir to output dir and get rid of it. Also delete original data.win
            # Also delete the json if we're on a race seed.
            progress_update("Moving to output directory...", 0.8)
            shutil.copytree(tempdir.name, output_path, dirs_exist_ok=True)
            if not patch_data.get("configuration_identifier", {}).get("contains_spoiler", False):
                input_data_win_path.parent.joinpath("yampfs-data.json").unlink()
            input_data_win_path.unlink()

            progress_update("Exporting finished!", 1)


def _load_cs_environment():
    # Load dotnet runtime
    load("coreclr")
    import clr

    clr.AddReference("YAMPFS-LIB")

@contextmanager
def load_wrapper() -> Wrapper:
    try:
        _load_cs_environment()
        from YAMPFS_LIB import Patcher as CSharp_Patcher
        yield Wrapper(CSharp_Patcher)
    except Exception as e:
        raise e


def _prepare_environment_and_get_data_win_path(folder: str) -> Path:
    current_platform = platform.system()
    folderPath = Path(folder)
    if current_platform == "Windows":
        return folderPath.joinpath("data.win")

    elif current_platform == "Linux":
        # Linux can have the game packed in an AppImage. If it exists, extract it first
        # Also extraction for some reason only does it into CWD with no way to change it, so we specify it.
        appimage = folderPath.joinpath("AM2R.AppImage")
        if appimage.exists():
            subprocess.run([appimage, "--appimage-extract"], cwd=folder)
            appimage.unlink()
            # shutil doesn't support moving a directory like this, so I copy + delete
            squashfsPath = folderPath.joinpath("squashfs-root")
            shutil.copytree(squashfsPath, folder, dirs_exist_ok=True)
            shutil.rmtree(squashfsPath)
            return folderPath.joinpath("usr", "bin", "assets", "game.unx")
        else:
            return folderPath.joinpath("assets", "game.unx")

    elif current_platform == "Darwin":
        return folderPath.joinpath("AM2R.app", "Contents", "Resources", "game.ios")

    else:
        raise ValueError(f"Unknown system: {platform.system()}")