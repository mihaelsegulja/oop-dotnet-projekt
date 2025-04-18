using DAL.Enums;
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

    public FileRepository(string basePath, WorldCupGender gender)
    {
        _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
        _gender = gender;

        string genderPath = GetGenderPath();
        if (!Directory.Exists(genderPath))
        {
            throw new DirectoryNotFoundException($"World Cup data directory not found: {genderPath}");
        }
    }

    /// <summary>
    /// Gets all teams from the teams.json file.
    /// </summary>
    /// <returns>A list of teams</returns>
    public async Task<List<Team>> GetTeams()
    {
        string filePath = GetFilePath("teams");
        return await ReadJsonFileAsync<List<Team>>(filePath);
    }

    /// <summary>
    /// Gets all matches from the matches.json file.
    /// </summary>
    /// <returns>A list of matches</returns>
    public async Task<List<Match>> GetMatches()
    {
        string filePath = GetFilePath("matches");
        return await ReadJsonFileAsync<List<Match>>(filePath);
    }

    /// <summary>
    /// Gets matches for a specific team identified by FIFA code.
    /// </summary>
    /// <param name="fifaCode">The FIFA code of the team (e.g., "CRO" for Croatia)</param>
    /// <returns>A list of matches for the specified team</returns>
    public async Task<List<Match>> GetMatchesByTeam(string fifaCode)
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

    /// <summary>
    /// Gets team results from the results.json file.
    /// </summary>
    /// <returns>A list of team results</returns>
    public async Task<List<Result>> GetTeamResults()
    {
        string filePath = GetFilePath("results");
        return await ReadJsonFileAsync<List<Result>>(filePath);
    }

    /// <summary>
    /// Gets group results from the group_results.json file.
    /// </summary>
    /// <returns>A list of group results</returns>
    public async Task<List<Result>> GetGroupResults()
    {
        string filePath = GetFilePath("group_results");
        return await ReadJsonFileAsync<List<Result>>(filePath);
    }

    /// <summary>
    /// Gets the gender of the World Cup data.
    /// </summary>
    /// <returns>The gender of the World Cup data</returns>
    public WorldCupGender GetWorldCupGender()
    {
        return _gender;
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
        catch (JsonException ex)
        {
            throw new JsonException($"Error parsing JSON file {filePath}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading file {filePath}: {ex.Message}", ex);
        }
    }
}
