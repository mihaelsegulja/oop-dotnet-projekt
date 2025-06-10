using DAL.Config;
using DAL.Repositories;
using System.Windows;

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

            var teams = await _repo.GetTeamResults();
            var team = teams.FirstOrDefault(t => t.FifaCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase));
            if (team == null)
            {
                MessageBox.Show($"Team with FIFA code '{fifaCode}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            tbTeamName.Text = $"Country: {team.Country}";
            tbFifaCode.Text = $"FIFA Code: {team.FifaCode}";
            tbPlayed.Text = $"Played: {team.GamesPlayed}";
            tbWins.Text = $"Wins: {team.Wins}";
            tbLosses.Text = $"Losses: {team.Losses}";
            tbDraws.Text = $"Draws: {team.Draws}";
            tbGoalsFor.Text = $"Goals For: {team.GoalsFor}";
            tbGoalsAgainst.Text = $"Goals Against: {team.GoalsAgainst}";
            tbGoalDiff.Text = $"Goal Difference: {team.GoalDifferential}";
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
