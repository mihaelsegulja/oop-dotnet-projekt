using DAL.Enums;

namespace DAL.Repositories;

/// <summary>
/// Factory for creating and managing repositories to access World Cup data.
/// </summary>
public static class RepositoryFactory
{
    private static readonly AppSettingsRepository _appSettingsRepository = new();

    /// <summary>
    /// Gets the settings repository.
    /// </summary>
    /// <returns>A settings repository</returns>
    public static IAppSettingsRepository GetAppSettingsRepository() => _appSettingsRepository;

    /// <summary>
    /// Gets a data repository.
    /// </summary>
    /// <param name="gender">World Cup gender to get data for</param>
    /// <param name="type">Repository type to use (defaults to AutoDetect)</param>
    /// <returns>A repository</returns>
    public static IRepository GetRepository(RepositoryType? type = null, WorldCupGender? gender = null)
    {
        type ??= RepositoryType.AutoDetect;

        if (gender == null)
        {
            var settings = _appSettingsRepository.LoadSettings();
            gender = settings.WorldCupGender;
        }

        return type switch
        {
            RepositoryType.WebApi => new WebApiRepository(gender.Value),
            RepositoryType.FileSystem => new FileRepository(gender.Value),
            RepositoryType.AutoDetect => IsWebApiAvailable()
                ? new WebApiRepository(gender.Value)
                : new FileRepository(gender.Value),
            _ => throw new ArgumentException("Wrong option!")
        };
    }

    /// <summary>
    /// Checks if the Web API is available.
    /// </summary>
    /// <returns>True if the Web API is reachable, otherwise false.</returns>
    private static bool IsWebApiAvailable()
    {
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            var response = client.GetAsync("https://worldcup-vua.nullbit.hr").Result;
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
