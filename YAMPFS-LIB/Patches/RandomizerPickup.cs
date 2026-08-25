using UndertaleModLib;
using UndertaleModLib.Models;

namespace YAMPFS_LIB.Patches;

public class RandomizerPickup
{
    public static void Apply(UndertaleData gmData, PatcherConfig config)
    {
        // patch out text and fanfare change for first missile expansion
        // TODO: make game respect Missile Launcher item
        if (config.PickupConfig.RequireMainMissiles)
        {
            var step0 = gmData.Code.ByName("gml_Object_obj_expansion_missile_Step_0");
            step0.SubstituteGMLCode("");

            var colSamus = gmData.Code.ByName("gml_Object_obj_expansion_missile_Collision_obj_samus");
            colSamus.ReplaceGMLCode("ds_write(\"Missile Launcher\", 1);\n", "");
        }

        // patch out text and fanfare change for first power bomb
        // TODO: make game respect Power Bomb Detonator item
        if (config.PickupConfig.RequirePBDetonator)
        {
            var step0 = gmData.Code.ByName("gml_Object_obj_expansion_power_bomb_Step_0");
            step0.SubstituteGMLCode("");

            var colSamus = gmData.Code.ByName("gml_Object_obj_expansion_power_bomb_Collision_obj_samus");
            colSamus.ReplaceGMLCode("ds_write(\"Power Bomb Detonator\", 1);\n", "");
        }

        // patch draw function to check if it is an artifact
        var draw0 = gmData.Code.ByName("gml_Object_par_upgrade_Draw_0");
        draw0.ReplaceGMLCode("if (is_aeon)", "if (is_artifact)");

        // patch par_upgrade.PreCreate to initialize self.is_artifact
        var preCreate0 = gmData.Code.ByName("gml_Object_par_upgrade_PreCreate_0");
        preCreate0.ReplaceGMLCode("true;\n", """
            true;
            
            var obj_name = object_get_name(object_index);
            if (string_starts_with(obj_name, "obj_artifact_"))
            {
                self.is_artifact = true;
            }
            else
            {
                self.is_artifact = false;
            }

            """);


        // patch each item
        foreach (var pe in config.PickupConfig.Items)
        {
            var newObj = gmData.GameObjects.ByName(pe.GameObjectName);
            var inst = gmData.Rooms.ByName(pe.Room).GameObjects.ByInstanceID(pe.InstanceID) 
                ?? throw new ApplicationException($"Did not find item in room {pe.Room} with InstanceID {pe.InstanceID}");
            inst.ObjectDefinition = newObj;

            // write new PreCreate code
            var script = $"""
                event_inherited();
                self.ds_name = "{pe.ItemKey}";
                self.upgrade_name = "{pe.ItemName}";
                self.description = "{pe.ItemDescription}";
                self.ds_adding = {pe.IsExpansion().ToString().ToLower()};
                self.ds_value = {pe.ItemValue};
                self._fanfare = {pe.Fanfare};

                """;

            var aeonCount = pe.Aeons.Count;
            script += $"self.is_aeon = {(aeonCount != 0).ToString().ToLower()};\n";
            for (int i = 0; i < 4; i++)
            {
                var aeonName = (i < aeonCount) ? pe.Aeons[i] : "Nothing";
                script += $"self.aeon_{i} = \"{aeonName}\";\n";
            }

            script += "self.sends_message = true;\n";

            // initialize PreCreateCode and replace GML
            inst.PreCreateCode ??= UndertaleCode.CreateEmptyEntry(gmData, $"gml_roomCC_Instance_{inst.InstanceID}_PreCreate");
            inst.PreCreateCode.SubstituteGMLCode(script);
        }
    }
}
