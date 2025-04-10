using Newtonsoft.Json;

namespace DAL.Models;

public partial class TeamResult
{
    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("goals")]
    public long Goals { get; set; }

    [JsonProperty("penalties")]
    public long Penalties { get; set; }
}