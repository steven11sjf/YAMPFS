using UndertaleModLib;

namespace YAMPFS_LIB.Patches;

public class FileSelectModifications
{
    public static void Apply(UndertaleData data, PatcherConfig config)
    {
        // load short seed hash from each file and store in an array
        var codeFileSelectCreate0 = data.Code.ByName("gml_Object_submenu_file_select_Create_0");
        var codeFileSelectStep0 = data.Code.ByName("gml_Object_submenu_file_select_Step_0");

        codeFileSelectCreate0.AppendGMLCode(
            "map7 = ds_map_create();\n",
            "rdvhashes = array_create(8);\n");

        for (int i = 0; i < 8; i++)
        {
            codeFileSelectCreate0.AppendGMLCode(
                $"    classic{i} = ds_zero_map(map{i}, \"Classic Mode\");\n",
                $$"""
                    var tmp = ds_zero_map(map{{i}}, "RDV Short Hash");
                    if (tmp == 0)
                    {
                        rdvhashes[{{i}}] = "UNKNOWN";
                    } else {
                        rdvhashes[{{i}}] = tmp;
                    }

                """);

            codeFileSelectStep0.AppendGMLCode(
                $"string(ds_map_find_value(map{i}, \"Deaths\"))",
                $" + \"Hash: \" + string(rdvhashes[{i}])"
                );
        }

        // store in newgame
        var codeGameCreate = data.Code.ByName("gml_Object_obj_game_Create_0");
        codeGameCreate.AppendGMLCode(
            "function start_game()\n{\n",
            $"    ds_write(\"RDV Short Hash\", \"{config.Identifier.Hash}\");\n");

        // on load game, check hash
        var codeFileSelectStep2 = data.Code.ByName("gml_Object_submenu_file_select_Step_2");
        var script = $$"""
                    var hash = rdvhashes[selection]
                    if (sub_selection != 1 && newish != undefined && hash != "{{config.Identifier.Hash}}")
                    {
                        with (instance_create_layer(0, 0, "Instances", obj_message_in_game))
                        {
                            message_0 = string(hash) + txt(" does not match expected hash {{config.Identifier.Hash}}!");
                        }
            
                        exit;
                    }

            """;

        // Enforce remix or classic on new files
        if (config.AcceptedModes != MPOMode.ANY)
        {
            var blockedMode = (config.AcceptedModes == MPOMode.REMIX) ? 1 : 0;
            script += $$"""
                        if (newish == undefined && sub_selection == {{blockedMode}})
                        {
                            with (instance_create_layer(0, 0, "Instances", obj_message_in_game))
                            {
                                message_0 = "Mode selection is restricted to {{config.AcceptedModes}}";
                            }

                            exit;
                        }

                """;
        }

        script += "        with (menu_title)\n";
        codeFileSelectStep2.ReplaceGMLCode("        with (menu_title)\n", script);
    }
}
