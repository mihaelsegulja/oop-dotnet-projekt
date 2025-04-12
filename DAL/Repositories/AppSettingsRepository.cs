using DAL.Enums;
using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories;

internal class AppSettingsRepository
{
    private readonly string _filePath = "appsettings.json";
    private readonly AppSettings _defaultAppSettings = new()
    {
        LanguageAndRegion = "en-US",
        WorldCupGender = WorldCupGender.Men
    };

    public AppSettingsRepository(string filePath)
    {
        _filePath = filePath;

        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }

    public void SaveSettings(AppSettings appSettings)
    {
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