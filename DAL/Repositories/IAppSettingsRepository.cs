using DAL.Config;

namespace DAL.Repositories;

public interface IAppSettingsRepository
{
    /// <summary>
    /// Saves the application settings.
    /// </summary>
    /// <param name="appSettings">The application settings to save.</param>
    void SaveSettings(AppSettings appSettings);

    /// <summary>
    /// Loads the application settings.
    /// </summary>
    /// <returns>The loaded application settings.</returns>
    AppSettings LoadSettings();

    /// <summary>
    /// Deletes the application settings folder.
    /// </summary>
    void DeleteSettings();
}