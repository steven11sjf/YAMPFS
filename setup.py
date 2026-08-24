import platform
import subprocess

import setuptools
from setuptools.command.build_py import build_py
from setuptools.dist import Distribution


class BuildPyCommand(build_py):
    def run(self):
        subprocess.run(['dotnet', 'publish', 'YAMPFS-LIB', '-c', 'Release', '-o', 'mpo_yampfs/yampfs', '-r', platform.platform()], check=True)
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