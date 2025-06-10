using DAL.Config;
using DAL.Enums;
using DAL.Models;
using DAL.Repositories;
using Localization;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Models;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IAppSettingsRepository _appSettingsRepo;
    private AppSettings _appSettings;
    private IRepository _repo;

    public MainWindow()
    {
        LoadLocalization();
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
            LoadHomeTeamsForCbAsync();
        }
    }

    private void ButtonInfoHomeTeam_Click(object sender, RoutedEventArgs e)
    {
        var selected = cbHomeTeam.SelectedItem as string;
        var fifaCode = GetFifaCode(selected);
        if (string.IsNullOrEmpty(fifaCode)) return;

        var detailWindow = new TeamDetailWindow(fifaCode)
        {
            Owner = this,
            Opacity = 0
        };
        detailWindow.ShowDialog();
    }

    private void ButtonInfoAwayTeam_Click(object sender, RoutedEventArgs e)
    {
        var selected = cbAwayTeam.SelectedItem as string;
        var fifaCode = GetFifaCode(selected);
        if (string.IsNullOrEmpty(fifaCode)) return;

        var detailWindow = new TeamDetailWindow(fifaCode)
        {
            Owner = this,
            Opacity = 0
        };
        detailWindow.ShowDialog();
    }

    private void cbHomeTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LoadAwayTeamsForCbAsync();
    }

    private void cbAwayTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DisplayMatchResult();
    }

    private MouseButtonEventHandler OpenPlayerInfo(PlayerStats playerStats)
    {
        return (sender, e) =>
        {
            new PlayerDetailWindow(playerStats).Show();
        };
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        var result = MessageBox.Show(
            Resource.PromptExit,
            Resource.ConfirmExit,
            MessageBoxButton.YesNo, 
            MessageBoxImage.Question
            );

        if (result == MessageBoxResult.No)
            e.Cancel = true;
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
        _appSettingsRepo = RepositoryFactory.GetAppSettingsRepository();
        _appSettings = _appSettingsRepo.LoadSettings();
        _repo = RepositoryFactory.GetRepository(_appSettings.RepositoryType);

        WindowState = _appSettings.WpfIsFullscreen ? WindowState.Maximized : WindowState.Normal;

        Width = _appSettings.WpfWindowWidth;
        Height = _appSettings.WpfWindowHeight;

        if (Thread.CurrentThread.CurrentUICulture.Name != _appSettings.LanguageAndRegion)
        {
            var culture = new CultureInfo(_appSettings.LanguageAndRegion);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }

    private void LoadLocalization()
    {
        _appSettingsRepo = RepositoryFactory.GetAppSettingsRepository();
        _appSettings = _appSettingsRepo.LoadSettings();

        if (Thread.CurrentThread.CurrentUICulture.Name != _appSettings.LanguageAndRegion)
        {
            var culture = new CultureInfo(_appSettings.LanguageAndRegion);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }

    private async void LoadHomeTeamsForCbAsync()
    {
        cbHomeTeam.Items.Clear();

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

    private async void LoadAwayTeamsForCbAsync()
    {
        cbAwayTeam.Items.Clear();

        var selected = cbHomeTeam.SelectedItem as string;
        var homeFifaCode = GetFifaCode(selected);
        if (string.IsNullOrEmpty(homeFifaCode))
            return;

        try
        {
            var matches = await _repo.GetMatchesByTeam(homeFifaCode);

            var opponentTeams = new HashSet<string>();
            foreach (var match in matches)
            {
                if (match.HomeTeam.Code == homeFifaCode)
                    opponentTeams.Add($"{match.AwayTeamCountry} ({match.AwayTeam.Code})");
                else if (match.AwayTeam.Code == homeFifaCode)
                    opponentTeams.Add($"{match.HomeTeamCountry} ({match.HomeTeam.Code})");
            }

            foreach (var team in opponentTeams.OrderBy(x => x))
            {
                cbAwayTeam.Items.Add(team);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading away teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void DisplayMatchResult()
    {
        tbMatchResult.Text = "X : X";

        var homeSelected = cbHomeTeam.SelectedItem as string;
        var awaySelected = cbAwayTeam.SelectedItem as string;
        var homeFifaCode = GetFifaCode(homeSelected);
        var awayFifaCode = GetFifaCode(awaySelected);

        if (string.IsNullOrEmpty(homeFifaCode) || string.IsNullOrEmpty(awayFifaCode))
            return;

        try
        {
            var matches = await _repo.GetMatchesByTeam(homeFifaCode);

            var match = matches.FirstOrDefault(m =>
                (m.HomeTeam.Code == homeFifaCode && m.AwayTeam.Code == awayFifaCode) ||
                (m.HomeTeam.Code == awayFifaCode && m.AwayTeam.Code == homeFifaCode));

            if (match != null)
            {
                int homeGoals = (int)match.HomeTeam.Goals;
                int awayGoals = (int)match.AwayTeam.Goals;

                var normalOrder = new[] { Position.Goalie, Position.Defender, Position.Midfield, Position.Forward };
                var reverseOrder = normalOrder.Reverse().ToArray();

                if (match.HomeTeam.Code == homeFifaCode)
                {
                    tbMatchResult.Text = $"{homeGoals} : {awayGoals}";
                    DisplayPlayersOnPitch(HomeTeamGrid, match.HomeTeamStatistics.StartingEleven, normalOrder, match);
                    DisplayPlayersOnPitch(AwayTeamGrid, match.AwayTeamStatistics.StartingEleven, reverseOrder, match);
                }
                else
                {
                    tbMatchResult.Text = $"{awayGoals} : {homeGoals}";
                    DisplayPlayersOnPitch(AwayTeamGrid, match.HomeTeamStatistics.StartingEleven, reverseOrder, match);
                    DisplayPlayersOnPitch(HomeTeamGrid, match.AwayTeamStatistics.StartingEleven, normalOrder, match);
                }
            }
            else
            {
                tbMatchResult.Text = "X : X";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading match result: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void DisplayPlayersOnPitch(Grid grid, List<StartingEleven> players, Position[] positions, DAL.Models.Match match)
    {
        grid.Children.Clear();
        grid.RowDefinitions.Clear();
        grid.ColumnDefinitions.Clear();

        for (int i = 0; i < positions.Length; i++)
            grid.ColumnDefinitions.Add(new ColumnDefinition());

        grid.RowDefinitions.Add(new RowDefinition());

        for (int col = 0; col < positions.Length; col++)
        {
            var group = players.Where(p => p.Position == positions[col]).ToList();

            var stack = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            foreach (var player in group)
            {
                var matchSurname = Regex.Match(player.Name, @"([A-Z][A-Z\- ]+)$");
                string displayName = matchSurname.Success ? matchSurname.Groups[1].Value : player.Name;
                var control = new PlayerUserControl(displayName, (int)player.ShirtNumber);

                var (goals, yellowCards) = GetPlayerStatsForMatch(match, player.Name);

                var playerStats = new PlayerStats
                {
                    Player = player,
                    Goals = goals,
                    YellowCards = yellowCards
                };
                control.MouseUp += OpenPlayerInfo(playerStats);
                stack.Children.Add(control);
            }

            Grid.SetColumn(stack, col);
            Grid.SetRow(stack, 0);
            grid.Children.Add(stack);
        }
    }

    private (int Goals, int YellowCards) GetPlayerStatsForMatch(DAL.Models.Match match, string playerName)
    {
        int goals = 0;
        int yellowCards = 0;

        foreach (var ev in match.HomeTeamEvents.Concat(match.AwayTeamEvents))
        {
            if (ev.Player == playerName)
            {
                if (ev.TypeOfEvent == TypeOfEvent.Goal || ev.TypeOfEvent == TypeOfEvent.GoalPenalty)
                    goals++;
                if (ev.TypeOfEvent == TypeOfEvent.YellowCard)
                    yellowCards++;
            }
        }

        return (goals, yellowCards);
    }

    #endregion
}
