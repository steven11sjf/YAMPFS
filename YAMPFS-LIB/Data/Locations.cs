using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YAMPFS_LIB.Data;

public class ItemInstance
{
    [JsonInclude]
    public required string Room;
    [JsonInclude]
    public uint InstanceID;
}

public class ItemLocation
{
    [JsonInclude]
    public required int PickupIndex;
    [JsonInclude]
    public List<ItemInstance> Instances = [];
    [JsonInclude]
    public List<string> SpawningScriptNames = [];
    [JsonInclude]
    public string OriginalObjectName = "";
}

public class AllItemLocations
{
    [JsonInclude]
    public List<ItemLocation> Locations = [];

    public static List<ItemLocation> GetItemLocationData()
    {
        return JsonSerializer.Deserialize<List<ItemLocation>>("""
            [
                {
                    "PickupIndex": 0,
                    "Instances": [
                    {
                        "Room": "cho_Antechamber",
                        "InstanceID": 100024
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_ice_beam"
                },
                {
                    "PickupIndex": 1,
                    "Instances": [
                    {
                        "Room": "cho_Burn_Dome_subB",
                        "InstanceID": 100223
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 2,
                    "Instances": [
                    {
                        "Room": "cho_Crossway",
                        "InstanceID": 100338
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 3,
                    "Instances": [
                    {
                        "Room": "cho_Dynamo",
                        "InstanceID": 100423
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 4,
                    "Instances": [
                    {
                        "Room": "cho_Dynamo",
                        "InstanceID": 100427
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 5,
                    "Instances": [
                    {
                        "Room": "cho_Elder_Chamber",
                        "InstanceID": 100501
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_9"
                },
                {
                    "PickupIndex": 6,
                    "Instances": [
                    {
                        "Room": "cho_Furnace_subC",
                        "InstanceID": 100723
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 7,
                    "Instances": [
                    {
                        "Room": "cho_Furnace_subD",
                        "InstanceID": 100761
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 8,
                    "Instances": [
                    {
                        "Room": "cho_Gathering_Hall",
                        "InstanceID": 100936
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 9,
                    "Instances": [
                    {
                        "Room": "cho_Hall_of_the_Elders",
                        "InstanceID": 101020
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 10,
                    "Instances": [
                    {
                        "Room": "cho_Hive_Totem",
                        "InstanceID": 101140
                    }
                    ],
                    "SpawningScriptNames": [
                        "gml_Object_obj_dead_hive_mecha_Step_2",
                        "gml_Object_obj_spawner_hive_mecha_Step_2"
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 11,
                    "Instances": [
                    {
                        "Room": "cho_Magma_Pool",
                        "InstanceID": 101186
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_power_bomb"
                },
                {
                    "PickupIndex": 12,
                    "Instances": [
                    {
                        "Room": "cho_Main_Plaza",
                        "InstanceID": 101296
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 13,
                    "Instances": [
                    {
                        "Room": "cho_Main_Plaza",
                        "InstanceID": 101306
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 14,
                    "Instances": [
                    {
                        "Room": "cho_Main_Plaza",
                        "InstanceID": 101305
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 15,
                    "Instances": [
                    {
                        "Room": "cho_Main_Plaza",
                        "InstanceID": 101326
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 16,
                    "Instances": [
                    {
                        "Room": "cho_Ruined_Fountain",
                        "InstanceID": 101714
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 17,
                    "Instances": [
                    {
                        "Room": "cho_Ruined_Gallery",
                        "InstanceID": 101726
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 18,
                    "Instances": [
                    {
                        "Room": "cho_Ruined_Gallery",
                        "InstanceID": 101770
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 19,
                    "Instances": [
                    {
                        "Room": "cho_Ruined_Nursery",
                        "InstanceID": 101792
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 20,
                    "Instances": [
                    {
                        "Room": "cho_Ruined_Shrine",
                        "InstanceID": 101873
                    }
                    ],
                    "SpawningScriptNames": [
                        "gml_Object_obj_spawner_beetle_horde_Destroy_0"
                    ],
                    "OriginalObjectName": "obj_upgrade_morph_ball"
                },
                {
                    "PickupIndex": 21,
                    "Instances": [
                    {
                        "Room": "cho_Ruined_Shrine",
                        "InstanceID": 101876
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 22,
                    "Instances": [
                    {
                        "Room": "cho_Ruined_Shrine",
                        "InstanceID": 101904
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 23,
                    "Instances": [
                    {
                        "Room": "cho_Sunchamber",
                        "InstanceID": 102277
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_varia_suit"
                },
                {
                    "PickupIndex": 24,
                    "Instances": [
                    {
                        "Room": "cho_Tower_Chamber",
                        "InstanceID": 102402
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_4"
                },
                {
                    "PickupIndex": 25,
                    "Instances": [
                    {
                        "Room": "cho_Tower_of_Light",
                        "InstanceID": 102443
                    }
                    ],
                    "SpawningScriptNames": [
                        "gml_Object_obj_manager_tower_of_light_Step_2"
                    ],
                    "OriginalObjectName": "obj_upgrade_wavebuster"
                },
                {
                    "PickupIndex": 26,
                    "Instances": [
                    {
                        "Room": "cho_Training_Chamber_Access",
                        "InstanceID": 102513
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 27,
                    "Instances": [
                    {
                        "Room": "cho_Training_Chamber",
                        "InstanceID": 102559
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 28,
                    "Instances": [
                    {
                        "Room": "cho_Transport_Access_North",
                        "InstanceID": 102628
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 29,
                    "Instances": [
                    {
                        "Room": "cho_Vault",
                        "InstanceID": 102799
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 30,
                    "Instances": [
                    {
                        "Room": "cho_Watery_Hall_Access",
                        "InstanceID": 102867
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 31,
                    "Instances": [
                    {
                        "Room": "cho_Watery_Hall",
                        "InstanceID": 102886
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_charge_beam"
                },
                {
                    "PickupIndex": 32,
                    "Instances": [
                    {
                        "Room": "cho_Watery_Hall",
                        "InstanceID": 102889
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 33,
                    "Instances": [
                    {
                        "Room": "mag_Fiery_Shores",
                        "InstanceID": 106340
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_power_bomb"
                },
                {
                    "PickupIndex": 34,
                    "Instances": [
                    {
                        "Room": "mag_Fiery_Shores",
                        "InstanceID": 106393
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 35,
                    "Instances": [
                    {
                        "Room": "mag_Lava_Lake_subC",
                        "InstanceID": 106923
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_7"
                },
                {
                    "PickupIndex": 36,
                    "Instances": [
                    {
                        "Room": "mag_Magmoor_Workstation",
                        "InstanceID": 107045
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 37,
                    "Instances": [
                    {
                        "Room": "mag_Plasma_Processing",
                        "InstanceID": 107731
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_plasma_beam"
                },
                {
                    "PickupIndex": 38,
                    "Instances": [
                    {
                        "Room": "mag_Shore_Tunnel",
                        "InstanceID": 107799
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_ice_spreader"
                },
                {
                    "PickupIndex": 39,
                    "Instances": [
                    {
                        "Room": "mag_Storage_Cavern",
                        "InstanceID": 107852
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 40,
                    "Instances": [
                    {
                        "Room": "mag_Transport_Tunnel_A",
                        "InstanceID": 107998
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 41,
                    "Instances": [
                    {
                        "Room": "mag_Triclops_Pit",
                        "InstanceID": 108398
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 42,
                    "Instances": [
                    {
                        "Room": "mag_Warrior_Shrine",
                        "InstanceID": 108571
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_1"
                },
                {
                    "PickupIndex": 43,
                    "Instances": [
                    {
                        "Room": "pha_Elite_Control_Access",
                        "InstanceID": 108865
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 44,
                    "Instances": [
                    {
                        "Room": "pha_Elite_Research_subB",
                        "InstanceID": 109306
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 45,
                    "Instances": [
                    {
                        "Room": "pha_Fungal_Hall_Access",
                        "InstanceID": 109678
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 46,
                    "Instances": [
                    {
                        "Room": "pha_Fungal_Hall_B",
                        "InstanceID": 109817
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 47,
                    "Instances": [
                    {
                        "Room": "pha_Main_Quarry",
                        "InstanceID": 109978
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 48,
                    "Instances": [
                    {
                        "Room": "pha_Metroid_Quarantine_A",
                        "InstanceID": 110125
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 49,
                    "Instances": [
                    {
                        "Room": "pha_Metroid_Quarantine_B",
                        "InstanceID": 110300
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 50,
                    "Instances": [
                    {
                        "Room": "pha_Phazon_Mining_Tunnel",
                        "InstanceID": 110859
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_11"
                },
                {
                    "PickupIndex": 51,
                    "Instances": [
                    {
                        "Room": "pha_Phazon_Processing_Center",
                        "InstanceID": 110988
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 52,
                    "Instances": [
                    {
                        "Room": "pha_Processing_Center_Access",
                        "InstanceID": 111083
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 53,
                    "Instances": [
                    {
                        "Room": "pha_Security_Access_A",
                        "InstanceID": 111446
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 54,
                    "Instances": [
                    {
                        "Room": "pha_Storage_Depot_A",
                        "InstanceID": 111520
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_flamethrower"
                },
                {
                    "PickupIndex": 55,
                    "Instances": [
                    {
                        "Room": "pha_Storage_Depot_B",
                        "InstanceID": 111550
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_grapple_beam"
                },
                {
                    "PickupIndex": 56,
                    "Instances": [
                    {
                        "Room": "pha_Ventilation_Shaft",
                        "InstanceID": 111662
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 57,
                    "Instances": [
                    {
                        "Room": "phe_Chapel_of_the_Elders_subB",
                        "InstanceID": 111813
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_wave_beam"
                },
                {
                    "PickupIndex": 58,
                    "Instances": [
                    {
                        "Room": "phe_Chozo_Ice_Temple_subD",
                        "InstanceID": 112067
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_8"
                },
                {
                    "PickupIndex": 59,
                    "Instances": [
                    {
                        "Room": "phe_Frost_Cave",
                        "InstanceID": 112417
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 60,
                    "Instances": [
                    {
                        "Room": "phe_Gravity_Chamber_subB",
                        "InstanceID": 112574
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 61,
                    "Instances": [
                    {
                        "Room": "phe_Gravity_Chamber",
                        "InstanceID": 112603
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_gravity_suit"
                },
                {
                    "PickupIndex": 62,
                    "Instances": [
                    {
                        "Room": "phe_Ice_Ruins_East",
                        "InstanceID": 112834
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 63,
                    "Instances": [
                    {
                        "Room": "phe_Ice_Ruins_East",
                        "InstanceID": 112852
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 64,
                    "Instances": [
                    {
                        "Room": "phe_Ice_Ruins_West",
                        "InstanceID": 112920
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_power_bomb"
                },
                {
                    "PickupIndex": 65,
                    "Instances": [
                    {
                        "Room": "phe_Observatory",
                        "InstanceID": 113161
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_super_missile"
                },
                {
                    "PickupIndex": 66,
                    "Instances": [
                    {
                        "Room": "phe_Phendrana_Canyon_subB",
                        "InstanceID": 113207
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_boost_ball"
                },
                {
                    "PickupIndex": 67,
                    "Instances": [
                    {
                        "Room": "phe_Phendrana_Shorelines_subB",
                        "InstanceID": 113290
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 68,
                    "Instances": [
                    {
                        "Room": "phe_Phendrana_Shorelines_subB",
                        "InstanceID": 113362
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 69,
                    "Instances": [
                    {
                        "Room": "phe_Quarantine_Monitor",
                        "InstanceID": 113794
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 70,
                    "Instances": [
                    {
                        "Room": "phe_Research_Core",
                        "InstanceID": 113908
                    }
                    ],
                    "SpawningScriptNames": [
                        "gml_Object_obj_jelzap_particle_Create_0",
                        "gml_Object_par_upgrade_Create_0"
                    ],
                    "OriginalObjectName": "obj_upgrade_spazer"
                },
                {
                    "PickupIndex": 71,
                    "Instances": [
                    {
                        "Room": "phe_Research_Lab_Aether_subB",
                        "InstanceID": 114003
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 72,
                    "Instances": [
                    {
                        "Room": "phe_Research_Lab_Aether",
                        "InstanceID": 114035
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 73,
                    "Instances": [
                    {
                        "Room": "phe_Research_Lab_Hydra_subB",
                        "InstanceID": 114079
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 74,
                    "Instances": [
                    {
                        "Room": "phe_Ruined_Courtyard",
                        "InstanceID": 114203
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 75,
                    "Instances": [
                    {
                        "Room": "phe_Security_Cave",
                        "InstanceID": 114398
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_power_bomb"
                },
                {
                    "PickupIndex": 76,
                    "Instances": [
                    {
                        "Room": "phe_Storage_Cave",
                        "InstanceID": 114533
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_10"
                },
                {
                    "PickupIndex": 77,
                    "Instances": [
                    {
                        "Room": "phe_Transport_Access",
                        "InstanceID": 114579
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 78,
                    "Instances": [
                    {
                        "Room": "tal_Alcove",
                        "InstanceID": 114813
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_space_jump"
                },
                {
                    "PickupIndex": 79,
                    "Instances": [
                    {
                        "Room": "tal_Arbor_Chamber",
                        "InstanceID": 114824
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 80,
                    "Instances": [
                    {
                        "Room": "tal_Biohazard_Containment",
                        "InstanceID": 114955
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 81,
                    "Instances": [
                    {
                        "Room": "tal_Cargo_Freight_Lift_to_Deck_Gamma",
                        "InstanceID": 115324
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 82,
                    "Instances": [
                    {
                        "Room": "tal_Crash_Site",
                        "InstanceID": 115552
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 83,
                    "Instances": [
                    {
                        "Room": "tal_Great_Tree_Chamber",
                        "InstanceID": 115777
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 84,
                    "Instances": [
                    {
                        "Room": "tal_Hydro_Access_Tunnel",
                        "InstanceID": 115967
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_energy_tank"
                },
                {
                    "PickupIndex": 85,
                    "Instances": [],
                    "SpawningScriptNames": [
                        "gml_Object_obj_boss_golden_guard_Create_0",
                        "gml_Object_obj_death_golden_guard_Destroy_0"
                    ],
                    "OriginalObjectName": "obj_artifact_3"
                },
                {
                    "PickupIndex": 86,
                    "Instances": [
                    {
                        "Room": "tal_Landing_Site",
                        "InstanceID": 116131
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 87,
                    "Instances": [
                    {
                        "Room": "tal_Life_Grove_Tunnel",
                        "InstanceID": 116227
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 88,
                    "Instances": [
                    {
                        "Room": "tal_Life_Grove",
                        "InstanceID": 116279
                    }
                    ],
                    "OriginalObjectName": "obj_upgrade_screw_attack"
                },
                {
                    "PickupIndex": 89,
                    "Instances": [
                    {
                        "Room": "tal_Life_Grove",
                        "InstanceID": 116309
                    }
                    ],
                    "OriginalObjectName": "obj_artifact_6"
                },
                {
                    "PickupIndex": 90,
                    "Instances": [
                    {
                        "Room": "tal_Overgrown_Cavern",
                        "InstanceID": 116472
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 91,
                    "Instances": [
                    {
                        "Room": "tal_Root_Cave",
                        "InstanceID": 116748
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 92,
                    "Instances": [
                    {
                        "Room": "tal_Transport_Tunnel_B",
                        "InstanceID": 117250
                    }
                    ],
                    "OriginalObjectName": "obj_expansion_missile"
                },
                {
                    "PickupIndex": 93,
                    "Instances": [],
                    "SpawningScriptNames": [
                        "gml_Object_obj_dying_incincerator_Destroy_0",
                        "gml_Object_obj_spawner_incinerator_drone_Create_0"
                    ],
                    "OriginalObjectName": "obj_upgrade_bomb"
                },
                {
                    "PickupIndex": 94,
                    "Instances": [],
                    "SpawningScriptNames": [
                        "gml_Object_par_upgrade_Destroy_0",
                        "gml_Object_obj_spawner_omega_pirate_Destroy_0"
                    ],
                    "OriginalObjectName": "obj_upgrade_phazon_suit"
                },
                {
                    "PickupIndex": 95,
                    "Instances": [],
                    "SpawningScriptNames": [
                    "gml_Object_obj_spawner_thardus_Destroy_0"
                    ],
                    "OriginalObjectName": "obj_upgrade_spider_ball"
                },
                {
                    "PickupIndex": 96,
                    "Instances": [],
                    "SpawningScriptNames": [
                        "gml_Object_obj_spawner_invisible_drone_Destroy_0"
                    ],
                    "OriginalObjectName": "obj_expansion_power_bomb"
                },
                {
                    "PickupIndex": 97,
                    "Instances": [],
                    "SpawningScriptNames": [
                        "gml_Object_obj_control_tower_tower_Destroy_0"
                    ],
                    "OriginalObjectName": "obj_artifact_2"
                },
                {
                    "PickupIndex": 98,
                    "Instances": [
                    {
                        "Room": "tal_Artifact_Temple_subB",
                        "InstanceID": 114854
                    },
                    {
                        "Room": "tal_Cipher_Site",
                        "InstanceID": 115370
                    }
                    ],
                    "SpawningScriptNames": [
                        "gml_Object_obj_spawner_meta_ridley_Step_2"
                    ],
                    "OriginalObjectName": "obj_artifact_0"
                },
                {
                    "PickupIndex": 99,
                    "Instances": [],
                    "SpawningScriptNames": [
                        "gml_Object_obj_spawner_phazon_elite_Destroy_0"
                    ],
                    "OriginalObjectName": "obj_artifact_5"
                }
            ]
            """)!;
    }
}