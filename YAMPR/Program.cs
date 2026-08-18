using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using Underanalyzer.Decompiler;
using UndertaleModLib;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;
using YAMPR_LIB.Patches;

namespace YAMPR_LIB;

public class Patcher
{
    internal static UndertaleData? gmData;
    internal static GlobalDecompileContext? decompileContext;

    //internal static Dictionary<UndertaleCode, DecompiledCode> CodeCache = new(1024);

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

    public static string CleanRoomName(string name)
    {
        return name.Replace("_", " ")[4..];
    }

    public static void Main(string mpoPath, string outputMpoPath, string jsonPath)
    {
        SeedObject? seedObject = JsonSerializer.Deserialize<SeedObject>(File.ReadAllText(jsonPath));

        if (seedObject == null)
        {
            throw new ApplicationException($"Json object at path {jsonPath} could not be parsed!");
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();

        using (FileStream fs = new FileInfo(mpoPath).OpenRead())
        {
            gmData = UndertaleIO.Read(fs);
        }

        sw.Stop();
        var afterRead = sw.Elapsed;

        Console.WriteLine("Read data file.");
        decompileContext = new GlobalDecompileContext(gmData);

        Patches.StartingItems.Apply(gmData, decompileContext, seedObject);

        using (FileStream fs = new FileInfo(outputMpoPath).OpenWrite())
        {
            UndertaleIO.Write(fs, gmData, Console.WriteLine);
        }
        //Database db = new Database();

        //foreach (var room in gmData.Rooms)
        //{
        //    // skip dummy rooms and init
        //    string roomName = room.Name.Content;
        //    if (roomName.StartsWith("_dummy") || roomName.StartsWith("fri_") || roomName.StartsWith("rm") || roomName.StartsWith("tst") || roomName == "init")
        //    {
        //        continue;
        //    }

        //    var roomNameClean = CleanRoomName(roomName);
        //    var area = db.GetArea(roomName[..3], roomNameClean);
        //    area.Extra["room_name"] = roomName;
        //    area.Extra["map_name"] = roomName;
        //    area.Extra["room_width"] = room.Width;
        //    area.Extra["room_height"] = room.Height;

        //    var nodeNames = new List<string>();

        //    foreach (var obj in room.GameObjects)
        //    {
        //        string objDefName = obj.ObjectDefinition.Name.Content;
        //        if (objDefName.StartsWith("obj_hatch_") && !objDefName.StartsWith("obj_hatch_cover"))
        //        {
        //            var preCC = obj.PreCreateCode.GetGMLCode();
        //            var dest_room = preCC.Split("\n").Single(line => line.StartsWith("self.dest_room = "));
        //            dest_room = dest_room[17..(dest_room.Length - 1)];

        //            string nodeName = "Door to " + CleanRoomName(dest_room);
        //            while (nodeNames.Contains(nodeName))
        //            {
        //                nodeName += "^";
        //            }
        //            nodeNames.Add(nodeName);

        //            area.Nodes.TryGetValue(nodeName, out DatabaseNode? node);

        //            if (node == null)
        //            {
        //                area.Nodes[nodeName] = new DatabaseDockNode();
        //                node = area.Nodes[nodeName];
        //            }
        //            var doorNode = (DatabaseDockNode)node;

        //            doorNode.NodeType = "dock";
        //            doorNode.Coordinates = new(obj.X, obj.Y, 0);

        //            doorNode.Extra["instance_id"] = obj.InstanceID;
        //            doorNode.Extra["facing"] = obj.Rotation;
        //            doorNode.Extra["dest_room"] = dest_room;
        //        }

        //        else if (objDefName.StartsWith("obj_expansion_"))
        //        {
        //            string expansionType = objDefName switch
        //            {
        //                "obj_expansion_missile" => "Missile Expansion",
        //                "obj_expansion_energy_tank" => "Energy Tank",
        //                "obj_expansion_power_bomb" => "Power Bomb Tank",
        //                _ => "Unknown Expansion"
        //            };

        //            string nodeName = "Pickup (" + expansionType + ")";
        //            while (nodeNames.Contains(nodeName))
        //            {
        //                nodeName += "^";
        //            }
        //            nodeNames.Add(nodeName);
        //            DatabaseNode node = db.GetNode(roomName[..3], roomNameClean, nodeName);
        //            node.NodeType = "pickup";
        //            node.Coordinates = new(obj.X, obj.Y, 0);
        //            node.Extra["instance_id"] = obj.InstanceID;
        //            node.Extra["object_name"] = obj.ObjectDefinition.Name.Content;
        //            node.Extra["item_type"] = expansionType;
        //            node.Extra["is_major"] = "minor";
        //            continue;
        //            // TODO add expansion
        //        }

        //        else if (objDefName.StartsWith("obj_artifact_"))
        //        {
        //            // TODO add artifact
        //            string artifactType = objDefName switch
        //            {
        //                "obj_artifact_1" => "Artifact 1",
        //                "obj_artifact_2" => "Artifact 2",
        //                "obj_artifact_3" => "Artifact 3",
        //                "obj_artifact_4" => "Artifact 4",
        //                "obj_artifact_5" => "Artifact 5",
        //                "obj_artifact_6" => "Artifact 6",
        //                "obj_artifact_7" => "Artifact 7",
        //                "obj_artifact_8" => "Artifact 8",
        //                "obj_artifact_9" => "Artifact 9",
        //                "obj_artifact_10" => "Artifact 10",
        //                "obj_artifact_11" => "Artifact 11",
        //                "obj_artifact_12" => "Artifact 12",
        //                _ => "BZZT"
        //            };

        //            if (artifactType == "BZZT")
        //            {
        //                continue;
        //            }

        //            string nodeName = "Pickup (" + artifactType + ")";
        //            while (nodeNames.Contains(nodeName))
        //            {
        //                nodeName += "^";
        //            }
        //            nodeNames.Add(nodeName);
        //            DatabaseNode node = db.GetNode(roomName[..3], roomNameClean, nodeName);
        //            node.NodeType = "pickup";
        //            node.Coordinates = new(obj.X, obj.Y, 0);
        //            node.Extra["instance_id"] = obj.InstanceID;
        //            node.Extra["object_name"] = obj.ObjectDefinition.Name.Content;
        //            node.Extra["item_type"] = artifactType;
        //            node.Extra["is_major"] = "major";
        //            continue;
        //        }

        //        else if (objDefName.StartsWith("obj_upgrade_"))
        //        {
        //            var preCC = obj.ObjectDefinition.EventHandlerFor(EventType.PreCreate, gmData).GetGMLCode();
        //            var item_name = preCC.Split("\n").Single(line => line.StartsWith("self.upgrade_name = "));
        //            item_name = item_name[21..(item_name.Length - 2)].Replace("_", " ");

        //            string nodeName = "Pickup (" + item_name + ")";
        //            while (nodeNames.Contains(nodeName))
        //            {
        //                nodeName += "^";
        //            }
        //            nodeNames.Add(nodeName);
        //            DatabaseNode node = db.GetNode(roomName[..3], roomNameClean, nodeName);
        //            node.NodeType = "pickup";
        //            node.Coordinates = new(obj.X, obj.Y, 0);
        //            node.Extra["instance_id"] = obj.InstanceID;
        //            node.Extra["object_name"] = obj.ObjectDefinition.Name.Content;
        //            node.Extra["item_type"] = item_name;
        //            node.Extra["is_major"] = "major";
        //            continue;
        //        }
        //    }
        //}

        //var serializerOptions = new JsonSerializerOptions()
        //{
        //    IncludeFields = true,
        //    WriteIndented = true,
        //};
        //foreach ((var regionName, var region) in db.Regions)
        //{
        //    // Debug.WriteLine("Writing json:\n\n" + JsonSerializer.Serialize(region));
        //    File.WriteAllText(jsonPath + "\\" + region.Name + ".json", JsonSerializer.Serialize(region, serializerOptions));
        //}
    }
}