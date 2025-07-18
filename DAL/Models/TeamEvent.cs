﻿using DAL.Converters;
using DAL.Enums;
using Newtonsoft.Json;

namespace DAL.Models;

public partial class TeamEvent
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("type_of_event")]
    [JsonConverter(typeof(TypeOfEventConverter))]
    public TypeOfEvent TypeOfEvent { get; set; }

    [JsonProperty("player")]
    public string Player { get; set; }

    [JsonProperty("time")]
    public string Time { get; set; }
}