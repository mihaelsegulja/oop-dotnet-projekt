using DAL.Config;
using DAL.Enums;
using Newtonsoft.Json;

namespace DAL.Repositories;

internal class AppSettingsRepository
{
    private readonly string _filePath = "wc_appsettings.json";
    private readonly AppSettings _defaultAppSettings = new()
    {
        LanguageAndRegion = "en-US",
        WorldCupGender = WorldCupGender.Men
    };

    public AppSettingsRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }

    public void SaveSettings(AppSettings appSettings)
    {
        appSettings ??= _defaultAppSettings;

        string json = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public AppSettings LoadSettings()
    {
        string json = File.ReadAllText(_filePath);
        AppSettings? appSettings = JsonConvert.DeserializeObject<AppSettings>(json);

        return appSettings ?? _defaultAppSettings;
    }
}