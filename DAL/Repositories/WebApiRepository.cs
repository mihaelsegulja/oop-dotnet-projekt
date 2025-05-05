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

    /// <summary>
    /// Gets all teams.
    /// </summary>
    /// <returns>A list of teams</returns>
    public async Task<IList<Team>> GetTeams()
    {
        string endpoint = GetEndpointPath("teams");
        return await ExecuteRequestAsync<List<Team>>(endpoint);
    }

    /// <summary>
    /// Gets a list of all matches.
    /// </summary>
    /// <returns>A list of matches</returns>
    public async Task<IList<Match>> GetMatches()
    {
        string endpoint = GetEndpointPath("matches");
        return await ExecuteRequestAsync<List<Match>>(endpoint);
    }

    /// <summary>
    /// Gets matches for a specific team identified by FIFA code.
    /// </summary>
    /// <param name="fifaCode">The FIFA code of the team (e.g., "CRO" for Croatia)</param>
    /// <returns>A list of matches for the specified team</returns>
    public async Task<IList<Match>> GetMatchesByTeam(string fifaCode)
    {
        if (string.IsNullOrEmpty(fifaCode))
            throw new ArgumentNullException(nameof(fifaCode), "FIFA code cannot be null or empty");

        string endpoint = GetEndpointPath("matches/country");
        var request = new RestRequest(endpoint);
        request.AddParameter("fifa_code", fifaCode);

        return await ExecuteRequestAsync<List<Match>>(request);
    }

    /// <summary>
    /// Gets team results and standings.
    /// </summary>
    /// <returns>A list of team results</returns>
    public async Task<IList<Result>> GetTeamResults()
    {
        string endpoint = GetEndpointPath("teams/results");
        return await ExecuteRequestAsync<List<Result>>(endpoint);
    }

    /// <summary>
    /// Gets group results.
    /// </summary>
    /// <returns>A list of group results</returns>
    public async Task<IList<Result>> GetGroupResults()
    {
        string endpoint = GetEndpointPath("teams/group_results");
        return await ExecuteRequestAsync<List<Result>>(endpoint);
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

    /// <summary>
    /// Disposes the RestClient when the repository is no longer needed.
    /// </summary>
    public void Dispose()
    {
        _client?.Dispose();
    }
}
