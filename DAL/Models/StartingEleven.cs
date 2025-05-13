using DAL.Converters;
using DAL.Enums;
using Newtonsoft.Json;

namespace DAL.Models;

public partial class StartingEleven
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("captain")]
    public bool Captain { get; set; }

    [JsonProperty("shirt_number")]
    public long ShirtNumber { get; set; }

    [JsonProperty("position")]
    [JsonConverter(typeof(PositionConverter))]
    public Position Position { get; set; }
}

public class StartingElevenComparer : IEqualityComparer<StartingEleven>
{
    public bool Equals(StartingEleven x, StartingEleven y)
    {
        return x.Name == y.Name && x.ShirtNumber == y.ShirtNumber && x.Position == y.Position;
    }

    public int GetHashCode(StartingEleven obj)
    {
        return HashCode.Combine(obj.Name, obj.ShirtNumber, obj.Position);
    }
}