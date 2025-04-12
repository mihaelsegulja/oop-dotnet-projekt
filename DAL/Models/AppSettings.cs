using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Models;

public class AppSettings
{
    [JsonProperty("language_and_region")]
    public string LanguageAndRegion { get; set; }
    [JsonProperty("world_cup_gender")]
    public WorldCupGender WorldCupGender { get; set; }
}