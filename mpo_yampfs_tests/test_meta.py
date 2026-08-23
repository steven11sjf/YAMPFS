from importlib.metadata import version
from unittest.mock import patch

import pytest
from pythonnet import load, unload

from mpo_yampfs import load_wrapper


@pytest.fixture(scope="session", autouse=True)
def _mock_load_unload():
    with patch("mpo_yampfs.wrapper._load_cs_environment") as mocked_load:
        load("coreclr")
        import clr

        clr.AddReference("YAMPFS-LIB")
        from YAMPFS_LIB import Patcher as CSharp_Patcher

        yield
        unload()


def test_correct_versions():
    cs_version = ""
    with load_wrapper() as w:
        cs_version = w.get_csharp_version()
    python_version = version("mpo_yampfs")

    assert cs_version != ""
    assert cs_version == python_version


def test_throw_correct_exception():
    with pytest.raises(Exception) as excinfo:
        with load_wrapper() as w:
            import System

            raise System.Exception("Dummy Exception")

    assert excinfo.type is System.Exception
    assert "Dummy Exception" == str(excinfo.value)