import platform
import subprocess

import setuptools
from setuptools.command.build_py import build_py
from setuptools.dist import Distribution


def get_dotnet_rid():
    system = platform.system()
    machine = platform.machine().lower()

    arch_map = {
        "x86_64": "x64",
        "amd64": "x64",
        "arm64": "arm64",
        "aarch64": "arm64",
    }
    arch = arch_map.get(machine)
    if arch is None:
        raise RuntimeError(f"Unsupported machine architecture: {machine}")

    if system == "Darwin":
        return f"osx-{arch}"
    elif system == "Linux":
        return f"linux-{arch}"
    elif system == "Windows":
        return f"win-{arch}"
    else:
        raise RuntimeError(f"Unsupported platform: {system}")

class BuildPyCommand(build_py):
    def run(self):
        rid = get_dotnet_rid()
        subprocess.run(['dotnet', 'publish', 'YAMPFS-LIB', '-c', 'Release', '-o', 'mpo_yampfs/yampfs', '-r', rid, '--self-contained', 'true'], check=True)
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