using Underanalyzer.Decompiler;
using UndertaleModLib;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;

namespace YAMPFS_LIB;

public static class ExtensionMethods
{
    private static UndertaleData? gmData = null;
    private static GlobalDecompileContext? globalCtx = null;
    private static DecompileSettings decompileSettings = new();

    static ExtensionMethods()
    {
        gmData = Patcher.gmData;
        globalCtx = Patcher.decompileContext;
    }

    public static string GetGMLCode(this UndertaleCode code)
    {
        if (Patcher.CodeCache.TryGetValue(code, out string? cached))
        {
            return cached;
        }

        var ctx = new DecompileContext(globalCtx!, code, decompileSettings);
        cached = ctx.DecompileToString();
        Patcher.CodeCache[code] = cached;
        return cached;
    }

    public static void ReplaceGMLCode(this UndertaleCode code, string vanilla, string modified, bool optional=false)
    {
        var func = code.GetGMLCode();
        if (!func.Contains(vanilla))
        {
            throw new ApplicationException($"Replacement code not found in function {code.Name}.\n\nString: {vanilla}");
        }

        Patcher.CodeCache[code] = func.Replace(vanilla, modified);
    }

    public static void SubstituteGMLCode(this UndertaleCode code, string modified)
    {
        Patcher.CodeCache[code] = modified;
    }
}
