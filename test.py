import mpo_yampfs

PATCH_DATA = {
    "starting_items": {
        "energy_tanks": 1,
        "missiles": 0,
        "upgrades": [
            "Morph Ball",
            "Screw Attack",
            "Plasma Beam",
            "Grapple Beam"
        ],
        "aeon": [
            "Death Ball"
        ]
    },
    "pickups": {
        "items": [
            {
                "room": "tal_Alcove",
                "instance_id": 114792,
                "game_object_name": "obj_upgrade_boost_ball",
                "item_key": "Boost Ball",
                "item_val": 1,
                "item_name": "Boostma Balls!",
                "item_desc": "We all love this guy. Comes with a Boost-Sprint for fun too!",
                "aeons": [
                    "Boost-Sprint"
                ],
                "sprite": "",
                "fanfare": "bgmFanfareItem"
            }
        ],
        "require_main_missiles": True,
        "require_pb_detonator": True,
        "require_power_beam": False
    }
}

def status_update(message: str, val: float):
    print(f"{val}: {message}")

with mpo_yampfs.load_wrapper() as wrapper:
    wrapper.patch_game("a:/mp_origins", "a:/mpo_modded", PATCH_DATA, status_update)