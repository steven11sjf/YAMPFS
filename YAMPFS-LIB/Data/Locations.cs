using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YAMPFS_LIB.Data;

public class ItemInstance
{
    [JsonInclude]
    public required string Room;
    [JsonInclude]
    public uint InstanceID;
}

public class ItemLocation
{
    [JsonInclude]
    public required int PickupIndex;
    [JsonInclude]
    public List<ItemInstance> Instances = [];
    [JsonInclude]
    public List<string> SpawningScriptNames = [];
    [JsonInclude]
    public string OriginalObjectName = "";
}

public class AllItemLocations
{
    [JsonInclude]
    public List<ItemLocation> Locations = [];

    public static List<ItemLocation> GetItemLocationData()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "Locations.json");
        AllItemLocations locs = JsonSerializer.Deserialize<AllItemLocations>(File.ReadAllText(path))!;
        return locs.Locations;
    }
}