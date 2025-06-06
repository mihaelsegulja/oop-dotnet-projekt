using DAL.Config;
using DAL.Repositories;
using System.Windows;
using System.Windows.Media.Animation;

namespace WpfApp;

/// <summary>
/// Interaction logic for TeamDetailWindow.xaml
/// </summary>
public partial class TeamDetailWindow : Window
{
    private IAppSettingsRepository _appSettingsRepo;
    private AppSettings _appSettings;
    private IRepository _repo;

    public TeamDetailWindow(string fifaCode)
    {
        InitializeComponent();
        LoadTeamInfo(fifaCode);
    }

    private async void LoadTeamInfo(string fifaCode)
    {
        try
        {
            _appSettingsRepo = RepositoryFactory.GetAppSettingsRepository();
            _appSettings = _appSettingsRepo.LoadSettings();
            _repo = RepositoryFactory.GetRepository(_appSettings.RepositoryType);

            var teams = await _repo.GetTeams();
            var team = teams.FirstOrDefault(t => t.FifaCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase));
            if (team == null)
            {
                MessageBox.Show($"Team with FIFA code '{fifaCode}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            var matches = await _repo.GetMatchesByTeam(fifaCode);

            int played = matches.Count;
            int wins = 0, losses = 0, draws = 0, goalsFor = 0, goalsAgainst = 0;

            foreach (var match in matches)
            {
                bool isHome = match.HomeTeam.Code == fifaCode;
                int teamGoals = isHome ? (int)match.HomeTeam.Goals : (int)match.AwayTeam.Goals;
                int oppGoals = isHome ? (int)match.AwayTeam.Goals : (int)match.HomeTeam.Goals;

                goalsFor += teamGoals;
                goalsAgainst += oppGoals;

                if (teamGoals > oppGoals) wins++;
                else if (teamGoals < oppGoals) losses++;
                else draws++;
            }

            tbTeamName.Text = $"Country: {team.Country}";
            tbFifaCode.Text = $"FIFA Code: {team.FifaCode}";
            tbPlayed.Text = $"Played: {played}";
            tbWins.Text = $"Wins: {wins}";
            tbLosses.Text = $"Losses: {losses}";
            tbDraws.Text = $"Draws: {draws}";
            tbGoalsFor.Text = $"Goals For: {goalsFor}";
            tbGoalsAgainst.Text = $"Goals Against: {goalsAgainst}";
            tbGoalDiff.Text = $"Goal Difference: {goalsFor - goalsAgainst}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading team info: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Close();
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
