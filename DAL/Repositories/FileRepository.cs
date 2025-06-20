﻿using DAL.Enums;
using DAL.Models;
using Newtonsoft.Json;

namespace DAL.Repositories;

internal class FileRepository : IRepository
{
    private readonly string _basePath;
    private readonly WorldCupGender _gender;
    private readonly Dictionary<string, string> _fileNames = new()
    {
        { "teams", "teams.json" },
        { "matches", "matches.json" },
        { "results", "results.json" },
        { "group_results", "group_results.json" }
    };

    public FileRepository(WorldCupGender gender)
    {
        _gender = gender;

        _basePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data"
        );

        string genderPath = GetGenderPath();
        if (!Directory.Exists(genderPath))
        {
            throw new DirectoryNotFoundException($"World Cup data directory not found: {genderPath}");
        }
    }
    
    public async Task<IList<Team>> GetTeams()
    {
        string filePath = GetFilePath("teams");
        return await ReadJsonFileAsync<List<Team>>(filePath);
    }
    
    public async Task<IList<Match>> GetMatches()
    {
        string filePath = GetFilePath("matches");
        return await ReadJsonFileAsync<List<Match>>(filePath);
    }
    
    public async Task<IList<Match>> GetMatchesByTeam(string fifaCode)
    {
        if (string.IsNullOrEmpty(fifaCode))
            throw new ArgumentNullException(nameof(fifaCode), "FIFA code cannot be null or empty");

        var matches = await GetMatches();

        // Filter matches where the team is either home or away
        return matches.Where(m =>
            (m.HomeTeam?.Code == fifaCode) ||
            (m.AwayTeam?.Code == fifaCode)
        ).ToList();
    }
    
    public async Task<IList<Result>> GetTeamResults()
    {
        string filePath = GetFilePath("results");
        return await ReadJsonFileAsync<List<Result>>(filePath);
    }
    
    public async Task<IList<Result>> GetGroupResults()
    {
        string filePath = GetFilePath("group_results");
        return await ReadJsonFileAsync<List<Result>>(filePath);
    }

    public async Task<IList<StartingEleven>> GetPlayersByCountry(string fifaCode)
    {
        if (string.IsNullOrEmpty(fifaCode))
            throw new ArgumentNullException(nameof(fifaCode), "FIFA code cannot be null or empty");

        var matches = await GetMatchesByTeam(fifaCode);

        var players = new HashSet<StartingEleven>(new StartingElevenComparer());

        foreach (var match in matches)
        {
            if (match.HomeTeam?.Code == fifaCode)
            {
                players.UnionWith(match.HomeTeamStatistics.StartingEleven);
                players.UnionWith(match.HomeTeamStatistics.Substitutes);
            }
            if (match.AwayTeam?.Code == fifaCode)
            {
                players.UnionWith(match.AwayTeamStatistics.StartingEleven);
                players.UnionWith(match.AwayTeamStatistics.Substitutes);
            }
        }

        return players.ToList();
    }

    /// <summary>
    /// Gets the path to the gender-specific folder.
    /// </summary>
    /// <returns>The path to the gender folder</returns>
    private string GetGenderPath()
    {
        string genderFolder = _gender == WorldCupGender.Men ? "men" : "women";
        return Path.Combine(_basePath, genderFolder);
    }

    /// <summary>
    /// Gets the full file path for a specific data file.
    /// </summary>
    /// <param name="fileKey">The key of the file</param>
    /// <returns>The full file path</returns>
    private string GetFilePath(string fileKey)
    {
        if (!_fileNames.ContainsKey(fileKey))
            throw new ArgumentException($"Unknown file key: {fileKey}");

        return Path.Combine(GetGenderPath(), _fileNames[fileKey]);
    }

    /// <summary>
    /// Reads and deserializes a JSON file asynchronously.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to</typeparam>
    /// <param name="filePath">The path to the JSON file</param>
    /// <returns>The deserialized object</returns>
    private async Task<T> ReadJsonFileAsync<T>(string filePath) where T : class
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"JSON file not found: {filePath}");

        try
        {
            string jsonContent = await File.ReadAllTextAsync(filePath);

            if (string.IsNullOrEmpty(jsonContent))
                throw new InvalidDataException($"JSON file is empty: {filePath}");

            var result = JsonConvert.DeserializeObject<T>(jsonContent);

            if (result == null)
                throw new JsonException($"Failed to deserialize JSON file: {filePath}");

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading file {filePath}: {ex.Message}", ex);
        }
    }
}
