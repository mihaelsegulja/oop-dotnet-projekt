using DAL.Enums;
using DAL.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DAL.Repositories;

internal class WebApiRepository : IRepository
{
    private readonly RestClient _client;
    private readonly string _baseUrl = "https://worldcup-vua.nullbit.hr";
    private readonly WorldCupGender _gender;

    public WebApiRepository(WorldCupGender gender)
    {
        _gender = gender;

        var clientOptions = new RestClientOptions(_baseUrl)
        {
            ThrowOnAnyError = true,
            Timeout = TimeSpan.FromMilliseconds(5000)
        };

        _client = new RestClient(clientOptions);
    }

    public async Task<IList<Team>> GetTeams()
    {
        string endpoint = GetEndpointPath("teams");
        return await ExecuteRequestAsync<List<Team>>(endpoint);
    }

    public async Task<IList<Match>> GetMatches()
    {
        string endpoint = GetEndpointPath("matches");
        return await ExecuteRequestAsync<List<Match>>(endpoint);
    }

    public async Task<IList<Match>> GetMatchesByTeam(string fifaCode)
    {
        if (string.IsNullOrEmpty(fifaCode))
            throw new ArgumentNullException(nameof(fifaCode), "FIFA code cannot be null or empty");

        string endpoint = GetEndpointPath("matches/country");
        var request = new RestRequest(endpoint);
        request.AddParameter("fifa_code", fifaCode);

        return await ExecuteRequestAsync<List<Match>>(request);
    }

    public async Task<IList<Result>> GetTeamResults()
    {
        string endpoint = GetEndpointPath("teams/results");
        return await ExecuteRequestAsync<List<Result>>(endpoint);
    }

    public async Task<IList<Result>> GetGroupResults()
    {
        string endpoint = GetEndpointPath("teams/group_results");
        return await ExecuteRequestAsync<List<Result>>(endpoint);
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
    /// Constructs the endpoint path based on the gender.
    /// </summary>
    /// <param name="endpoint">The endpoint name</param>
    /// <returns>The complete endpoint path</returns>
    private string GetEndpointPath(string endpoint)
    {
        string genderPath = _gender == WorldCupGender.Men ? "men" : "women";
        return $"{genderPath}/{endpoint}";
    }

    /// <summary>
    /// Executes a request using a string endpoint.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="endpoint">The endpoint to request</param>
    /// <returns>The deserialized response</returns>
    private async Task<T> ExecuteRequestAsync<T>(string endpoint) where T : class, new()
    {
        var request = new RestRequest(endpoint);
        return await ExecuteRequestAsync<T>(request);
    }

    /// <summary>
    /// Executes a request using a RestRequest object.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The request to execute</param>
    /// <returns>The deserialized response</returns>
    private async Task<T> ExecuteRequestAsync<T>(RestRequest request) where T : class, new()
    {
        try
        {
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"API request failed: {response.ErrorMessage} - Status code: {response.StatusCode}");
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                throw new Exception("API returned empty response");
            }

            var result = JsonConvert.DeserializeObject<T>(response.Content);

            if (result == null)
            {
                throw new Exception("Failed to deserialize API response");
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving data from API: {ex.Message}", ex);
        }
    }
}
