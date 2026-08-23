
using UndertaleModLib;

namespace YAMPFS_LIB.Patches;

public class StartLocation
{
    public static readonly string VanillaStartLocationCode =
        """
            spawn(obj_intro_0);
            room_goto(rm_Intro_0);

        """;

    public static void Apply(UndertaleData gmData, PatcherConfig config)
    {
        var gameCreateCode = gmData.Code.ByName("gml_Object_obj_game_Create_0");

        var newCode = $"""
                room_goto({config.StartLocation.Room});
                instance_create({config.StartLocation.X}, {config.StartLocation.Y}, obj_samus);
            """;

        if (config.StartLocation.Room == "tal_Landing_Site")
        {
            newCode += $"""
                    ds_write("Ship", 1);
                    obj_samus.pose = 100;
                    obj_samus.y += 32;
                """;
        }
        gameCreateCode.ReplaceGMLCode(VanillaStartLocationCode, newCode);
    }
}
