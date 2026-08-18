using System;
using System.Collections.Generic;
using System.Text;
using Underanalyzer;
using Underanalyzer.Compiler;
using Underanalyzer.Decompiler;
using UndertaleModLib;
using UndertaleModLib.Compiler;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;

namespace YAMPR_LIB;

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
        var ctx = new DecompileContext(globalCtx!, code, decompileSettings);
        return ctx.DecompileToString();
    }
    
    public static void CompileGMLCode(this UndertaleCode code, string newCode)
    {
        CompileGroup group = new(gmData);
        group.QueueCodeReplace(code, newCode);
        group.Compile();
    }


}
