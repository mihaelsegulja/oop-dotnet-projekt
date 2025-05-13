using DAL.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DAL.Config;

public class AppSettings
{
    [JsonProperty("language_and_region")]
    public string LanguageAndRegion { get; set; }

    [JsonProperty("world_cup_gender")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WorldCupGender WorldCupGender { get; set; }

    [JsonProperty("fav_team")]
    public string? FavTeam { get; set; }

    [JsonProperty("fav_players")]
    public List<string>? FavPlayersList { get; set; } = new List<string>();
}
