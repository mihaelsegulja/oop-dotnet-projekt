﻿using Newtonsoft.Json;

namespace DAL.Models;

public partial class Result
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("alternate_name")]
    public object AlternateName { get; set; }

    [JsonProperty("fifa_code")]
    public string FifaCode { get; set; }

    [JsonProperty("group_id")]
    public long GroupId { get; set; }

    [JsonProperty("group_letter")]
    public string GroupLetter { get; set; }

    [JsonProperty("wins")]
    public long Wins { get; set; }

    [JsonProperty("draws")]
    public long Draws { get; set; }

    [JsonProperty("losses")]
    public long Losses { get; set; }

    [JsonProperty("games_played")]
    public long GamesPlayed { get; set; }

    [JsonProperty("points")]
    public long Points { get; set; }

    [JsonProperty("goals_for")]
    public long GoalsFor { get; set; }

    [JsonProperty("goals_against")]
    public long GoalsAgainst { get; set; }

    [JsonProperty("goal_differential")]
    public long GoalDifferential { get; set; }
}