using System.Text.Json.Serialization;

namespace YAMPFS_LIB;

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class PatcherConfig
{
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
    [JsonPropertyName("room")]
    public required string Room;

    [JsonInclude]
    [JsonPropertyName("instance_id")]
    public required uint InstanceID;

    [JsonInclude]
    [JsonPropertyName("game_object_name")]
    public required string GameObjectName;

    [JsonInclude]
    [JsonPropertyName("item_key")]
    public required string ItemKey;

    [JsonInclude]
    [JsonPropertyName("item_val")]
    public int ItemValue = 1;

    [JsonInclude]
    [JsonPropertyName("item_name")]
    public required string ItemName;

    [JsonInclude]
    [JsonPropertyName("item_desc")]
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

    public bool IsExpansion()
    {
        return (ItemKey == "Missiles Max" || ItemKey == "Power Bombs Max");
    }
}