using DAL.Config;
using DAL.Repositories;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private AppSettings _appSettings;
    private IRepository _repo;

    public MainWindow()
    {
        InitializeComponent();
        LoadAppSettings();
    }

    #region EVENTS

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadHomeTeamsForCbAsync();
    }

    private void Settings_Click(object sender, RoutedEventArgs e)
    {
        var result = new AppSettingsWindow().ShowDialog();

        if(result == true)
        {
            LoadAppSettings();
        }
    }

    private void ButtonInfoHomeTeam_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ButtonInfoAwayTeam_Click(object sender, RoutedEventArgs e)
    {

    }

    #endregion

    #region HELPERS

    private string? GetFifaCode(string selected)
    {
        if (selected != null)
        {
            int start = selected.LastIndexOf('(');
            int end = selected.LastIndexOf(')');
            return selected.Substring(start + 1, end - start - 1);
        }
        return null;
    }

    private void LoadAppSettings()
    {
        _appSettings = RepositoryFactory.GetAppSettings();
        _repo = RepositoryFactory.GetRepository();

        Width = _appSettings.WpfWindowWidth;
        Height = _appSettings.WpfWindowHeight;

        if (Thread.CurrentThread.CurrentUICulture.Name != _appSettings.LanguageAndRegion)
        {
            var culture = new CultureInfo(_appSettings.LanguageAndRegion);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            // HandleLocalization();
        }
    }

    private async void LoadHomeTeamsForCbAsync()
    {
        try
        {
            var teams = await _repo.GetTeams();
            var sortedTeams = teams.ToList();
            sortedTeams.Sort((x, y) => string.Compare(x.Country, y.Country, StringComparison.Ordinal));

            foreach (var team in sortedTeams)
            {
                cbHomeTeam.Items.Add($"{team.Country} ({team.FifaCode})");
            }

            if (!string.IsNullOrEmpty(_appSettings.FavTeam))
            {
                int idx = cbHomeTeam.Items.IndexOf(_appSettings.FavTeam);
                if (idx >= 0)
                    cbHomeTeam.SelectedIndex = idx;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    #endregion
}