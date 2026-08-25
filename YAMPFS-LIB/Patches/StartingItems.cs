using UndertaleModLib;

namespace YAMPFS_LIB.Patches;

public class StartingItems
{
    private static readonly string InitialCreationCode = 
        """
            ds_write("Energy", 99);
            ds_write("Energy Tanks", ds_zero("Energy Tanks Max"));
            ds_write("Varia Suit");
            ds_write("Morph Ball");
            ds_write("Morph Ball Bomb");
            ds_write("Charge Beam");
            ds_write("Grapple Beam");
            ds_write("Missiles", 15);
            ds_write("Missiles Max", 15);
        """;

    public static void Apply(UndertaleData gmData, PatcherConfig config)
    {
        // add starting etanks/missiles and aeon abilities to the start game function

        var gameCreateCode = gmData.Code.ByName("gml_Object_obj_game_Create_0");
        var newCode = "";
        var items = config.StartingItems;
        // TODO: add variable etank sizes
        newCode += $"    ds_write(\"Energy\", {items.EnergyTanks * 100 + 99});\n";
        newCode += "    ds_write(\"Energy Tanks\", ds_zero(\"Energy Tanks Max\"));\n";
        newCode += $"    ds_write(\"Missiles\", {items.Missiles});\n";
        newCode += $"    ds_write(\"Missiles Max\", {items.Missiles});\n";
        foreach (var upgradeName in items.Upgrades)
        {
            newCode += $"    ds_write(\"{upgradeName}\");\n";
        }
        newCode += "    aeon_array = dz(\"Aeon\");\n";
        newCode += "    stat_array = dz(\"Aeon Status\");\n";
        foreach (var aeonName in items.Aeon)
        {
            newCode += $"    array_push(stat_array, 0);\n";
            newCode += $"    array_push(aeon_array, \"{aeonName}\");\n";
        }

        gameCreateCode.ReplaceGMLCode(InitialCreationCode, newCode);

        // remove the requirement to have artifacts to use the aeon menu
        var menuStepCode = gmData.Code.ByName("gml_Object_menu_save_point_Step_0");
        menuStepCode.ReplaceGMLCode("if (skips < 12)", "if (0 < array_length(dz(\"Aeon\")))");
    }
}
