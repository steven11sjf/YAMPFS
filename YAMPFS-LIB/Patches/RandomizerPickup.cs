using UndertaleModLib;
using UndertaleModLib.Models;
using YAMPFS_LIB.Data;

namespace YAMPFS_LIB.Patches;

public class RandomizerPickup
{

    public static void ConvertPickupsToGameObjects(UndertaleData gmData)
    {
        var locations = AllItemLocations.GetItemLocationData();

        foreach (var loc in locations)
        {
            var go = new UndertaleGameObject()
            {
                Name = gmData.Strings.MakeString($"obj_rando_pickup_{loc.PickupIndex}"),
                ParentId = gmData.GameObjects.ByName(loc.OriginalObjectName)
            };

            gmData.GameObjects.Add(go);

            if (loc.InstanceID != -1)
            {
                var instance = gmData.Rooms.ByName(loc.Room).GameObjects.ByInstanceID((uint)loc.InstanceID);
                instance.ObjectDefinition = go;
            }

            foreach (var scriptName in loc.SpawningScriptNames)
            {
                var script = gmData.Code.ByName(scriptName);
                script.ReplaceGMLCode(loc.OriginalObjectName, $"obj_rando_pickup_{loc.PickupIndex}");
            }
        }
    }

    public static void Apply(UndertaleData gmData, PatcherConfig config)
    {
        ConvertPickupsToGameObjects(gmData);

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

        var par_upgrade = gmData.GameObjects.ByName("par_upgrade");

        // patch each item
        foreach (var pe in config.PickupConfig.Items)
        {
            var randoPickupGO = gmData.GameObjects.ByName($"obj_rando_pickup_{pe.PickupIndex}");

            randoPickupGO.ParentId = par_upgrade;

            // write new PreCreate code
            var codePreCreate = randoPickupGO.EventHandlerFor(EventType.PreCreate, gmData);
            var script = $"""
                event_inherited();
                self.ds_name = "{pe.ItemKey}";
                self.upgrade_name = "{pe.ItemDisplayName}";
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

            codePreCreate.SubstituteGMLCode(script);

            // assign sprite
            var spriteName = pe.Sprite;
            randoPickupGO.Sprite = gmData.Sprites.ByName(spriteName);

            // TODO decouple artifact index from sprite
            if (spriteName == "sprChozoArtifacts")
            {
                var codeCreate = randoPickupGO.EventHandlerFor(EventType.Create, gmData);
                codeCreate.SubstituteGMLCode($"""
                    event_inherited();
                    image_speed = 0;
                    image_index = {pe.ArtifactIndex};
                    alarm[0] = 6;

                    """);

                var codeDestroy = randoPickupGO.EventHandlerFor(EventType.Destroy, gmData);
                codeDestroy.SubstituteGMLCode($"""
                    event_inherited();
                    temp_array = dz("Chozo Artifacts");
                    art = {pe.ArtifactIndex};
                    array_set(temp_array, art, artifact_names_short(art));
                    ds_write("Chozo Artifacts", temp_array);

                    """);
            }

        }
    }
}
