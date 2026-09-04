from pathlib import Path

from mpo_yampfs import load_wrapper


def test_export(mpo_104_path, tmp_path, test_files_dir):
    output_path: Path = tmp_path.joinpath("out")
    configuration = test_files_dir.read_json("starter_preset.json")

    def dummy_progress_update(msg, pct):
        pass

    with load_wrapper() as wrapper:
        wrapper.patch_game(mpo_104_path, output_path, configuration, dummy_progress_update)