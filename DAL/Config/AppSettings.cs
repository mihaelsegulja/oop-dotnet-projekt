using DAL.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DAL.Config;

public class AppSettings
{
    [JsonProperty("language_and_region")]
    public string LanguageAndRegion { get; set; } = "en-US";

    [JsonProperty("world_cup_gender")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WorldCupGender WorldCupGender { get; set; } = WorldCupGender.Men;

    [JsonProperty("fav_team")]
    public string? FavTeam { get; set; }

    [JsonProperty("fav_players")]
    public List<string>? FavPlayersList { get; set; } = new List<string>();

    [JsonProperty("wpf_window_width")]
    public double WpfWindowWidth { get; set; } = 800;

    [JsonProperty("wpf_window_height")]
    public double WpfWindowHeight { get; set; } = 600;
}
