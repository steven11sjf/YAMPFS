using System.Text.Json.Serialization;

namespace YAMPFS_LIB;

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class PatcherConfig
{
    [JsonInclude]
    [JsonPropertyName("identifier")]
    public ConfigurationIdentifier Identifier = new();

    [JsonInclude]
    [JsonPropertyName("starting_items")]
    public StartingItems StartingItems = new();

    [JsonInclude]
    [JsonPropertyName("starting_location")]
    public StartLocation StartLocation = new();

    [JsonInclude]
    [JsonPropertyName("pickups")]
    public PickupConfig PickupConfig = new();
}

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class ConfigurationIdentifier
{
    [JsonInclude]
    [JsonPropertyName("hash")]
    public string Hash = "UNKNOWN";

    [JsonInclude]
    [JsonPropertyName("word_hash")]
    public string WordHash = "Default Word Hash";

    [JsonInclude]
    [JsonPropertyName("randovania_version")]
    public string RDVVersion = "Unknown";

    [JsonInclude]
    [JsonPropertyName("patcher_version")]
    public string PatcherVersion = "";
}

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class StartingItems
{
    [JsonInclude]
    [JsonPropertyName("energy_tanks")]
    public int EnergyTanks = 0;

    [JsonInclude]
    [JsonPropertyName("missiles")]
    public int Missiles = 15;

    [JsonInclude]
    [JsonPropertyName("power_bombs")]
    public int PowerBombs = 0;

    [JsonInclude]
    [JsonPropertyName("upgrades")]
    public List<string> Upgrades = [];

    [JsonInclude]
    [JsonPropertyName("aeon")]
    public List<string> Aeon = [];
}

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class StartLocation
{
    [JsonInclude]
    [JsonPropertyName("room")]
    public string Room = "tal_Landing_Site";

    [JsonInclude]
    [JsonPropertyName("x")]
    public int X = 400;

    [JsonInclude]
    [JsonPropertyName("y")]
    public int Y = 516;
}

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class PickupConfig
{
    [JsonInclude]
    [JsonPropertyName("items")]
    public List<PickupEntry> Items = [];

    [JsonInclude]
    [JsonPropertyName("require_main_missiles")]
    public bool RequireMainMissiles = false;

    [JsonInclude]
    [JsonPropertyName("require_pb_detonator")]
    public bool RequirePBDetonator = false;

    [JsonInclude]
    [JsonPropertyName("require_power_beam")]
    public bool RequirePowerBeam = false;
}

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class PickupEntry
{
    [JsonInclude]
    [JsonPropertyName("pickup_index")]
    public required int PickupIndex;

    [JsonInclude]
    [JsonPropertyName("item_key")]
    public required string ItemKey;

    [JsonInclude]
    [JsonPropertyName("item_val")]
    public int ItemValue = 1;

    [JsonInclude]
    [JsonPropertyName("additional_items")]
    public Dictionary<string, string> AdditionalItems = [];

    [JsonInclude]
    [JsonPropertyName("item_display_name")]
    public required string ItemDisplayName;

    [JsonInclude]
    [JsonPropertyName("item_description")]
    public required string ItemDescription;

    [JsonInclude]
    [JsonPropertyName("aeons")]
    public List<string> Aeons = [];

    [JsonInclude]
    [JsonPropertyName("sprite")]
    public required string Sprite;

    [JsonInclude]
    [JsonPropertyName("fanfare")]
    public string Fanfare = "bgmFanfareItem";

    [JsonInclude]
    [JsonPropertyName("artifact_idx")]
    public int ArtifactIndex = -1;

    public bool IsExpansion()
    {
        return (ItemKey == "Missiles Max" || ItemKey == "Power Bombs Max");
    }
}