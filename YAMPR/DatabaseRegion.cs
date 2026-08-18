using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using Underanalyzer.Decompiler.AST;

namespace YAMPR;

public class DatabaseDockNode : DatabaseNode
{
    [JsonInclude]
    [JsonPropertyName("dock_type")]
    public string DockType = "Door";

    [JsonInclude]
    [JsonPropertyName("default_connection")]
    public Dictionary<string, string> DefaultConn = [];

    [JsonInclude]
    [JsonPropertyName("default_dock_weakness")]
    public string DefaultDockWeakness = "Normal";

    [JsonInclude]
    [JsonPropertyName("exclude_from_dock_rando")]
    public bool ExcludeFromDockRando = false;

    [JsonInclude]
    [JsonPropertyName("incompatible_dock_weaknesses")]
    public List<string> IncompatibleDockWeaknesses = [];

    [JsonInclude]
    [JsonPropertyName("override_default_open_requirement")]
    public string? OverrideDefaultOpenRequirement;

    [JsonInclude]
    [JsonPropertyName("override_default_lock_requirement")]
    public string? OverrideDefaultLockRequirement;

    [JsonInclude]
    [JsonPropertyName("ui_custom_name")]
    public string? UiCustomName;
}
public class DatabaseNode
{
    [JsonInclude]
    [JsonPropertyName("node_type")]
    public string NodeType = "generic";

    [JsonInclude]
    [JsonPropertyName("heal")]
    public bool Heal;

    [JsonInclude]
    [JsonPropertyName("coordinates")]
    public Vector3 Coordinates = new();

    [JsonInclude]
    [JsonPropertyName("description")]
    public string Description = "";

    [JsonInclude]
    [JsonPropertyName("layers")]
    public List<string> Layers = ["Default"];

    [JsonInclude]
    [JsonPropertyName("extra")]
    public Dictionary<string, object> Extra = [];

    [JsonInclude]
    [JsonPropertyName("valid_starting_location")]
    public bool ValidStartingLocation;

    [JsonInclude]
    [JsonPropertyName("connections")]
    public Dictionary<String, object> Connections = [];
}
public class DatabaseArea
{
    [JsonInclude]
    [JsonPropertyName("default_node")]
    public string? DefaultNode;

    [JsonInclude]
    [JsonPropertyName("hint_features")]
    public List<string> HintFeatures = [];

    [JsonInclude]
    [JsonPropertyName("extra")]
    public Dictionary<string, object> Extra = [];

    [JsonInclude]
    [JsonPropertyName("nodes")]
    public Dictionary<string, DatabaseNode> Nodes = [];
}

public class DatabaseRegion
{
    [JsonInclude]
    [JsonPropertyName("name")]
    public required string Name;

    [JsonInclude]
    [JsonPropertyName("extra")]
    public Dictionary<string, object> Extra = [];

    [JsonInclude]
    [JsonPropertyName("areas")]
    public required Dictionary<string, DatabaseArea> Areas;
}

public class Database
{
    public Dictionary<string, DatabaseRegion> Regions;

    public Database()
    {
        Regions = [];
        Regions["Chozo_Ruins"] = new DatabaseRegion
        {
            Name = "Chozo Ruins",
            Areas = []
        };
        Regions["Impact_Crater"] = new DatabaseRegion
        {
            Name = "Impact Crater",
            Areas = []
        };
        Regions["Magmoor_Caverns"] = new DatabaseRegion
        {
            Name = "Magmoor Caverns",
            Areas = []
        };
        Regions["Phazon_Mines"] = new DatabaseRegion
        {
            Name = "Phazon Mines",
            Areas = []
        };
        Regions["Phendrana_Drifts"] = new DatabaseRegion
        {
            Name = "Phendrana Drifts",
            Areas = []
        };
        Regions["Tallon_Overworld"] = new DatabaseRegion
        {
            Name = "Tallon Overworld",
            Areas = []
        };
        Regions["Wompwomp"] = new DatabaseRegion
        {
            Name = "Wompwomp",
            Areas = []
        };
    }

    public DatabaseArea GetArea(string regionName, string areaName)
    {
        DatabaseRegion region = regionName switch
        {
            "cho" => Regions["Chozo_Ruins"],
            "imp" => Regions["Impact_Crater"],
            "mag" => Regions["Magmoor_Caverns"],
            "pha" => Regions["Phazon_Mines"],
            "phe" => Regions["Phendrana_Drifts"],
            "tal" => Regions["Tallon_Overworld"],
            _ => Regions["Wompwomp"],
        };

        region.Areas.TryGetValue(areaName, out DatabaseArea? area);
        if (area == null)
        {
            region.Areas[areaName] = new DatabaseArea
            {
                Nodes = []
            };
            area = region.Areas[areaName];
        }

        return area;
    }

    public DatabaseNode GetNode(string regionName, string areaName, string nodeName)
    {
        DatabaseArea area = GetArea(regionName, areaName);
        area.Nodes.TryGetValue(nodeName, out DatabaseNode? node);

        if (node == null)
        {
            area.Nodes[nodeName] = new DatabaseNode();
            node = area.Nodes[nodeName];
        }

        return node;
    }
}
