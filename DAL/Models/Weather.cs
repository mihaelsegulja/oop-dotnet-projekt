﻿using DAL.Converters;
using DAL.Enums;
using Newtonsoft.Json;

namespace DAL.Models;

public partial class Weather
{
    [JsonProperty("humidity")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Humidity { get; set; }

    [JsonProperty("temp_celsius")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long TempCelsius { get; set; }

    [JsonProperty("temp_farenheit")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long TempFarenheit { get; set; }

    [JsonProperty("wind_speed")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long WindSpeed { get; set; }

    [JsonProperty("description")]
    public Description Description { get; set; }
}