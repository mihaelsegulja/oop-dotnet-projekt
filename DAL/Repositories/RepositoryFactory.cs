using DAL.Enums;
using DAL.Models;

namespace DAL.Repositories;

/// <summary>
/// Factory for creating and managing repositories to access World Cup data.
/// </summary>
public partial class RepositoryFactory
{
    private readonly AppSettingsRepository _appSettingsRepository;

    /// <summary>
    /// Initializes a new instance of the RepositoryFactory with a custom settings file.
    /// </summary>
    public RepositoryFactory()
    {
        _appSettingsRepository = new AppSettingsRepository();
    }

    public AppSettings GetAppSettings()
    {
        return _appSettingsRepository.LoadSettings();
    }

    public void SaveAppSettings(AppSettings appSettings)
    {
        _appSettingsRepository.SaveSettings(appSettings);
    }

    /// <summary>
    /// Gets a repository.
    /// </summary>
    /// <param name="gender">World Cup gender to get data for</param>
    /// <param name="type">Repository type to use (defaults to AutoDetect)</param>
    /// <returns>A repository</returns>
    public IRepository GetRepository(RepositoryType? type = null, WorldCupGender? gender = null)
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
    private bool IsWebApiAvailable()
    {
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(3);
            var response = client.GetAsync("https://worldcup-vua.nullbit.hr").Result;
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
