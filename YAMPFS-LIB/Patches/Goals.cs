using System;
using System.Collections.Generic;
using System.Text;
using UndertaleModLib;

namespace YAMPFS_LIB.Patches;

public class Goals
{
    public static void Apply(UndertaleData gmData, PatcherConfig config)
    {
        // apply required artifacts and bosses to ridley spawner
        var ridleySpawnerStep2 = gmData.Code.ByName("gml_Object_spawner_meta_ridley_Step_2");

        var script = "if (abs(x - par_player.x) < 210";
        script += $" && dz(\"Rando Artifact Count\") >= {config.Goals.RequiredArtifacts}";

        foreach (var boss in config.Goals.RequiredBosses)
        {
            script += $" && dz(\"{boss}\")";
        }

        script += ")";

        ridleySpawnerStep2.ReplaceGMLCode(
            ridleySpawnerStep2.SelectBetween(
                "if (abs(x - par_player.x) < 64 && ",
                " && dz(\"Classic Mode\"))"),
            script);

        // apply goal description
        var fileSelectStep0 = gmData.Code.ByName("gml_Object_submenu_file_select_Step_0");
        fileSelectStep0.ReplaceGMLCode(
            fileSelectStep0.SelectBetween(
                "function mode_description()",
                "Cipher Keys\");\n}"),
            $$"""
            function mode_description()
            {
                return "{{config.Goals.GoalDescription}}";
            }
            """);
    }
}
