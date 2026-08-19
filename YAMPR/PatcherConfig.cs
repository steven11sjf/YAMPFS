using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace YAMPR_LIB;

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class PatcherConfig
{
    [JsonInclude]
    [JsonPropertyName("starting_items")]
    public StartingItems StartingItems = new();

    [JsonInclude]
    [JsonPropertyName("starting_location")]
    public StartLocation StartLocation = new();
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