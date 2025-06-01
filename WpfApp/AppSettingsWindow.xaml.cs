using DAL.Config;
using DAL.Enums;
using DAL.Repositories;
using System.Windows;
using System.Windows.Shapes;

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
}