using DAL.Enums;
using DAL.Models;
using System.Net.NetworkInformation;

namespace DAL.Repositories;

/// <summary>
/// Factory for creating and managing repositories to access World Cup data.
/// </summary>
public class RepositoryFactory
{
    private readonly AppSettingsRepository _appSettingsRepository;

    /// <summary>
    /// The type of repository to use for data access.
    /// </summary>
    public enum RepositoryType
    {
        /// <summary>
        /// Use online API for data access
        /// </summary>
        WebApi,

        /// <summary>
        /// Use local JSON files for data access
        /// </summary>
        FileSystem,

        /// <summary>
        /// Try WebApi first, fallback to FileSystem if unavailable
        /// </summary>
        AutoDetect
    }

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
    /// <returns>A repository implementing IRepository</returns>
    public IRepository GetRepository(RepositoryType type = RepositoryType.AutoDetect, WorldCupGender? gender = null)
    {
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
            using var ping = new Ping();
            var reply = ping.Send("worldcup-vua.nullbit.hr", 3000);
            return reply?.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
}
