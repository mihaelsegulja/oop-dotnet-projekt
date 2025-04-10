using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DAL.Models;

public partial class GroupResult
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("letter")]
    public string Letter { get; set; }

    [JsonProperty("ordered_teams")]
    public List<Result> OrderedTeams { get; set; }
}