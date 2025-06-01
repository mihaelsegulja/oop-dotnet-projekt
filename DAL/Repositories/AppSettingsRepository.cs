using DAL.Config;
using Newtonsoft.Json;

namespace DAL.Repositories;

internal class AppSettingsRepository : IAppSettingsRepository
{
    private readonly string _filePath;
    private readonly AppSettings _defaultAppSettings = new();

    public AppSettingsRepository()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appData, "msegulja-oop-dotnet", "WorldCupApp");
        Directory.CreateDirectory(appFolder);
        _filePath = Path.Combine(appFolder, "wc_appsettings.json");
        if (!File.Exists(_filePath))
            File.Create(_filePath).Close();
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

    public void DeleteSettings()
    {
        if (File.Exists(_filePath))
            File.Delete(_filePath);
    }
}