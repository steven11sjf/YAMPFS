import os
import sys
from contextlib import contextmanager
from pathlib import Path
from typing import Iterator, Union

PathLike = Union[str, "os.PathLike[str]"]

# DLLs live next to this file so pythonnet can resolve YAMPFS-LIB and its
# dependencies (UndertaleModLib, Underanalyzer, ...) purely by adding this
# directory to sys.path before the CLR is loaded.
# https://github.com/randovania/YAMS/blob/2.8.0/am2r_yams/wrapper.py#L12-L20
_native_path = os.fspath(Path(__file__).with_name("native"))
sys.path.append(_native_path)


class Wrapper:
    """Thin wrapper around the C# ``Patcher`` class."""

    def __init__(self, patcher):
        self._patcher = patcher

    def patch(
        self,
        mpo_path: PathLike,
        output_mpo_path: PathLike,
        config_path: PathLike,
    ) -> None:
        """Run Patcher.Main(mpoPath, outputMpoPath, configPath)."""
        self._patcher.Main(
            os.fspath(mpo_path),
            os.fspath(output_mpo_path),
            os.fspath(config_path),
        )


def _load_cs_environment() -> None:
    from pythonnet import get_runtime_info, load

    if get_runtime_info() is None:
        load("coreclr")

    import clr

    clr.AddReference("YAMPFS-LIB")


@contextmanager
def load_wrapper() -> Iterator[Wrapper]:
    _load_cs_environment()
    from YAMPFS_LIB import Patcher as CSharpPatcher

    yield Wrapper(CSharpPatcher)


def patch(
    mpo_path: PathLike,
    output_mpo_path: PathLike,
    config_path: PathLike,
) -> None:
    """One-shot convenience wrapper around Patcher.Main for simple scripts."""
    with load_wrapper() as wrapper:
        wrapper.patch(mpo_path, output_mpo_path, config_path)
