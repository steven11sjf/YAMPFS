using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UndertaleModLib;
using UndertaleModLib.Decompiler;
using YAMPR;

namespace YAMPR_LIB.Patches;

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

    public static void Apply(UndertaleData gmData, GlobalDecompileContext ctx, SeedObject seedObject)
    {
        var gameCreateCode = gmData.Code.ByName("gml_Object_obj_game_Create_0");
        var decompiled = gameCreateCode.GetGMLCode();
        var newCode = "";
        var items = seedObject.StartingItems;
        Debug.WriteLine(InitialCreationCode);
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

        if (!decompiled.Contains(InitialCreationCode))
        {
            Debug.WriteLine("fuck");
        }

        var final = decompiled.Replace(InitialCreationCode, newCode);
        Debug.WriteLine(final);
        gameCreateCode.CompileGMLCode(final);
    }
}
