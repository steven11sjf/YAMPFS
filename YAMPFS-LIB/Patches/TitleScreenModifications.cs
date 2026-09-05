using UndertaleModLib;

namespace YAMPFS_LIB.Patches;


public class TitleScreenModifications
{
    public static void Apply(UndertaleData gmData, PatcherConfig config)
    {
        var codeMenuTitlePreCreate = gmData.Code.ByName("gml_Object_menu_title_PreCreate_0");
        codeMenuTitlePreCreate.ReplaceGMLCode("1.0.4", $"{config.Identifier.RDVVersion} / {config.Identifier.PatcherVersion}\\n{config.Identifier.WordHash} ({config.Identifier.Hash})");
    }
}