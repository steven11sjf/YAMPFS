import os
import platform
import re
import shutil
import subprocess
import tempfile
from pathlib import Path

import setuptools
from setuptools.command.build_py import build_py
from setuptools.dist import Distribution

ARCH_MAP = {
    "x86_64": "x64",
    "amd64": "x64",
    "arm64": "arm64",
    "aarch64": "arm64",
}

def get_target_archs():
    if archflags := os.environ.get("ARCHFLAGS"):  # noqa: SIM102
        if archs := re.findall(r"-arch\s+(\S+)", archflags):
            return archs

    return [platform.machine()]


def get_dotnet_rids():
    system = platform.system()
    archs = get_target_archs()

    rids = []
    for machine in archs:
        arch = ARCH_MAP.get(machine.lower())

        if arch is None:
            raise RuntimeError(f"Unsupported machine architecture: {arch}")

        if system == "Darwin":
            rids.append(f"osx-{arch}")
        elif system == "Linux":
            rids.append(f"linux-{arch}")
        elif system == "Windows":
            rids.append(f"win-{arch}")
        else:
            raise RuntimeError(f"Unsupported platform: {system}")

    return rids

def is_lipo_mergeable(path: Path):
    result = subprocess.run(["lipo", "-info", str(path)], capture_output=True, text=True, check=True)
    return result.returncode == 0

def publish_single(rid: str, out_dir: Path):
    """Run the dotnet publish command for the specified rid"""
    subprocess.run(["dotnet", "publish", "YAMPFS-LIB", "-c", "Release", "-o", str(out_dir), "-r", rid, "--self-contained", "true"], check=True)

def merge_publish_outputs(per_rid_dirs: dict[str, Path], final_dir: Path):
    """Merge multiple single-arch publish outputs into a universal2 output dir"""
    final_dir.mkdir(parents=True, exist_ok=True)
    dirs = list(per_rid_dirs.values())
    reference_dir = dirs[0]

    for root, _dirs, files in os.walk(reference_dir):
        rel_root = Path(root).relative_to(reference_dir)
        for filename in files:
            rel_path = rel_root / filename
            dest_path = final_dir / rel_path
            dest_path.parent.mkdir(parents=True, exist_ok=True)
            candidate_paths = [d / rel_path for d in dirs]
            candidate_paths = [p for p in candidate_paths if p.exists()]

            if len(candidate_paths) > 1 and is_lipo_mergeable(candidate_paths[0]):
                subprocess.run(["lipo", "-create", "-output", str(dest_path), *map(str, candidate_paths)], check=True)
            else:
                shutil.copy2(candidate_paths[0], dest_path)


class BuildPyCommand(build_py):
    def run(self):
        rids = get_dotnet_rids()
        final_dir = Path("mpo_yampfs/yampfs")

        if len(rids) == 1:
            publish_single(rids[0], final_dir)
        else:
            with tempfile.TemporaryDirectory() as tmp:
                tmp_path = Path(tmp)
                per_rid_dirs = {}
                for rid in rids:
                    rid_dir = tmp_path / rid
                    publish_single(rid, rid_dir)
                    per_rid_dirs[rid] = rid_dir

                merge_publish_outputs(per_rid_dirs, final_dir)

        build_py.run(self)

class BinaryDistribution(Distribution):
    def has_ext_modules(self):
        return True

setuptools.setup(
    cmdclass={
        "build_py": BuildPyCommand,
    },
    distclass=BinaryDistribution,
)