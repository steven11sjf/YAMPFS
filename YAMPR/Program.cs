using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using UndertaleModLib;
using UndertaleModLib.Compiler;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;

namespace YAMPR_LIB;

public class Patcher
{
    internal static UndertaleData? gmData;
    internal static GlobalDecompileContext? decompileContext;

    internal static Dictionary<UndertaleCode, string> CodeCache = new(1024);

    private static string CreateVersionString()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        string? version = fileVersionInfo.ProductVersion;

        if (version is null) return "";

        string[] split = version.Split('.');
        string major = split[0];
        string minor = split[1];
        string build = split[2];
        if (build.Contains('+'))
            build = build[..build.IndexOf('+')];
        build = build.Replace('-', '.');
        if (build.Length > 2 && build[2..].StartsWith("rc"))
            build = build[0] + build[2..];

        return $"{major}.{minor}.{build}";
    }

    public static void Main(string mpoPath, string outputMpoPath, string jsonPath)
    {
        Console.WriteLine($"Starting YAMPR {CreateVersionString()}");

        PatcherConfig? config = JsonSerializer.Deserialize<PatcherConfig>(File.ReadAllText(jsonPath)) 
            ?? throw new ApplicationException($"Json object at path {jsonPath} could not be parsed!");
        var sw = new Stopwatch();
        sw.Start();

        using (FileStream fs = new FileInfo(mpoPath).OpenRead())
        {
            gmData = UndertaleIO.Read(fs);
        }

        sw.Stop();
        var afterRead = sw.Elapsed;
        Console.WriteLine("Read data file.");
        sw.Start();

        decompileContext = new GlobalDecompileContext(gmData);

        Patches.StartingItems.Apply(gmData, config);
        Patches.StartLocation.Apply(gmData, config);
        Patches.RandomizerPickup.Apply(gmData, config);

        // compile all code units
        CompileGroup group = new(gmData);
        foreach ((var code, var replacement) in CodeCache)
        {
            Debug.WriteLine($"New code for {code.Name}:");
            Debug.WriteLine(replacement);
            group.QueueCodeReplace(code, replacement);
        }
        var res = group.Compile();

        if (!res.Successful)
        {
            throw new ApplicationException("Error compiling GML:\n\n" + res.PrintAllErrors(true));
        }

        sw.Stop();
        var patchingTime = sw.Elapsed;
        Console.WriteLine("Generated patched data file.");
        sw.Start();

        // write data.win
        using (FileStream fs = new FileInfo(outputMpoPath).OpenWrite())
        {
            UndertaleIO.Write(fs, gmData, Console.WriteLine);
        }

        sw.Stop();
        var writingTime = sw.Elapsed;
        Console.WriteLine($"Wrote patched file to {outputMpoPath}.");
        Console.WriteLine($"Data patched in {(patchingTime-afterRead).TotalSeconds} seconds ({afterRead.TotalSeconds} read, {(writingTime-patchingTime).TotalSeconds} write).");
    }
}