using DAL.Models;

namespace DAL.Repositories;

public interface IRepository
{
    /// <summary>
    /// Gets all teams.
    /// </summary>
    /// <returns>A list of teams</returns>
    Task<IList<Team>> GetTeams();

    /// <summary>
    /// Gets all matches.
    /// </summary>
    /// <returns>A list of matches</returns>
    Task<IList<Match>> GetMatches();

    /// <summary>
    /// Gets matches for a specific team identified by FIFA code.
    /// </summary>
    /// <param name="fifaCode">The FIFA code of the team (e.g., "CRO" for Croatia)</param>
    /// <returns>A list of matches for the specified team</returns>
    Task<IList<Match>> GetMatchesByTeam(string fifaCode);

    /// <summary>
    /// Gets team results and standings.
    /// </summary>
    /// <returns>A list of team results</returns>
    Task<IList<Result>> GetTeamResults();

    /// <summary>
    /// Gets group results.
    /// </summary>
    /// <returns>A list of group results</returns>
    Task<IList<Result>> GetGroupResults();
    
    /// <summary>
    /// Gets players from a country identified by FIFA code.
    /// </summary>
    /// <param name="fifaCode">The FIFA code of the team (e.g., "CRO" for Croatia)</param>
    /// <returns>A list of players for the specified country</returns>
    Task<IList<StartingEleven>> GetPlayersByCountry(string fifaCode);
}