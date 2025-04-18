using DAL.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DAL.Models;

public class AppSettings
{
    [JsonProperty("language_and_region")]
    public string LanguageAndRegion { get; set; }

    [JsonProperty("world_cup_gender")]
    [JsonConverter(typeof(StringEnumConverter))]
    public WorldCupGender WorldCupGender { get; set; }
}
