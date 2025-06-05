using DAL.Config;
using DAL.Enums;
using DAL.Repositories;
using System.Windows;
using WpfApp.Models;

namespace WpfApp;

/// <summary>
/// Interaction logic for AppSettingsWindow.xaml
/// </summary>
public partial class AppSettingsWindow : Window
{
    private IAppSettingsRepository _appSettingsRepo;
    private AppSettings _appSettings;    

    public AppSettingsWindow()
    {
        InitializeComponent();
        _appSettingsRepo = RepositoryFactory.GetAppSettingsRepository();
        _appSettings = _appSettingsRepo.LoadSettings();
    }

    private void AppSettingsWindow_Loaded(object sender, RoutedEventArgs e)
    {
        rbEn.IsChecked = _appSettings.LanguageAndRegion == "en-US";
        rbHr.IsChecked = _appSettings.LanguageAndRegion == "hr-HR";

        rbMen.IsChecked = _appSettings.WorldCupGender == WorldCupGender.Men;
        rbWomen.IsChecked = _appSettings.WorldCupGender == WorldCupGender.Women;

        cbFullScreen.IsChecked = _appSettings.WpfIsFullscreen;

        if (_appSettings.WpfWindowWidth == Resolutions.Res800x600.Width && 
            _appSettings.WpfWindowHeight == Resolutions.Res800x600.Height)
            rbRes1.IsChecked = true;
        else if (_appSettings.WpfWindowWidth == Resolutions.Res1000x800.Width && 
            _appSettings.WpfWindowHeight == Resolutions.Res1000x800.Height)
            rbRes2.IsChecked = true;
        else if (_appSettings.WpfWindowWidth == Resolutions.Res1200x900.Width && 
            _appSettings.WpfWindowHeight == Resolutions.Res1200x900.Height)
            rbRes3.IsChecked = true;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        _appSettings.LanguageAndRegion = rbEn.IsChecked == true ? "en-US" : "hr-HR";
        _appSettings.WorldCupGender = rbMen.IsChecked == true ? WorldCupGender.Men : WorldCupGender.Women;

        _appSettings.WpfIsFullscreen = cbFullScreen.IsChecked == true;

        if (rbRes1.IsChecked == true)
        {
            _appSettings.WpfWindowWidth = Resolutions.Res800x600.Width;
            _appSettings.WpfWindowHeight = Resolutions.Res800x600.Height;
        }
        else if (rbRes2.IsChecked == true)
        {
            _appSettings.WpfWindowWidth = Resolutions.Res1000x800.Width;
            _appSettings.WpfWindowHeight = Resolutions.Res1000x800.Height;
        }
        else if (rbRes3.IsChecked == true)
        {
            _appSettings.WpfWindowWidth = Resolutions.Res1200x900.Width;
            _appSettings.WpfWindowHeight = Resolutions.Res1200x900.Height;
        }

        _appSettingsRepo.SaveSettings(_appSettings);

        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void cbFullScreen_Checked(object sender, RoutedEventArgs e)
    {
        rbRes1.IsEnabled = false;
        rbRes2.IsEnabled = false;
        rbRes3.IsEnabled = false;
    }

    private void cbFullScreen_Unchecked(object sender, RoutedEventArgs e)
    {
        rbRes1.IsEnabled = true;
        rbRes2.IsEnabled = true;
        rbRes3.IsEnabled = true;
    }
}