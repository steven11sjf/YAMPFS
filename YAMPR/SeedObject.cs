using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace YAMPR_LIB;

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class SeedObject
{
    [JsonInclude]
    [JsonPropertyName("starting_items")]
    public StartingItems StartingItems = new();
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