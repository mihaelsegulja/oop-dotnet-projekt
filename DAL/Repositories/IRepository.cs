using DAL.Enums;
using DAL.Models;

namespace DAL.Repositories;

public interface IRepository
{
    /// <summary>
    /// Gets all teams.
    /// </summary>
    /// <returns>A list of teams</returns>
    Task<List<Team>> GetTeams();

    /// <summary>
    /// Gets all matches.
    /// </summary>
    /// <returns>A list of matches</returns>
    Task<List<Match>> GetMatches();

    /// <summary>
    /// Gets matches for a specific team identified by FIFA code.
    /// </summary>
    /// <param name="fifaCode">The FIFA code of the team (e.g., "CRO" for Croatia)</param>
    /// <returns>A list of matches for the specified team</returns>
    Task<List<Match>> GetMatchesByTeam(string fifaCode);

    /// <summary>
    /// Gets team results and standings.
    /// </summary>
    /// <returns>A list of team results</returns>
    Task<List<Result>> GetTeamResults();

    /// <summary>
    /// Gets group results.
    /// </summary>
    /// <returns>A list of group results</returns>
    Task<List<Result>> GetGroupResults();
}