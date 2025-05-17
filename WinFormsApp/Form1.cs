using DAL.Config;
using DAL.Enums;
using DAL.Repositories;
using System.Windows.Forms;

namespace WinFormsApp;

public partial class Form1 : Form
{
    private readonly AppSettings _appSettings;
    private readonly IRepository _repo;

    public Form1()
    {
        InitializeComponent();

        _appSettings = RepositoryFactory.GetAppSettings();
        _repo = RepositoryFactory.GetRepository();
    }

    #region EVENTS

    private void btnSaveSettings_Click(object sender, EventArgs e)
    {
        _appSettings.LanguageAndRegion = rbEn.Checked ? "en-US" : "hr-HR";
        _appSettings.WorldCupGender = rbMen.Checked ? WorldCupGender.Men : WorldCupGender.Women;

        RepositoryFactory.SaveAppSettings(_appSettings);

        tcMain.SelectedTab = tpPlayers;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        rbEn.Checked = _appSettings.LanguageAndRegion == "en-US";
        rbHr.Checked = _appSettings.LanguageAndRegion == "hr-HR";

        rbMen.Checked = _appSettings.WorldCupGender == WorldCupGender.Men;
        rbWomen.Checked = _appSettings.WorldCupGender == WorldCupGender.Women;
    }

    private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tcMain.SelectedTab == tpPlayers)
        {
            cbTeams.Items.Clear();
            cbTeams.ResetText();
            flpAllPlayers.AutoScroll = true;
            flpFavPlayers.AutoScroll = true;
            flpAllPlayers.BorderStyle = BorderStyle.FixedSingle;
            flpFavPlayers.BorderStyle = BorderStyle.FixedSingle;
            flpAllPlayers.Controls.Clear();
            flpFavPlayers.Controls.Clear();
            LoadTeamsAsync();
        }
    }

    private void btnSaveFavs_Click(object sender, EventArgs e)
    {
        _appSettings.FavTeam = cbTeams.SelectedItem?.ToString();

        RepositoryFactory.SaveAppSettings(_appSettings);
    }

    private void cbTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPlayersAsync();
    }

    #endregion

    #region HELPERS

    private string? GetSelectedFifaCode()
    {
        if (cbTeams.SelectedItem is string selected)
        {
            int start = selected.LastIndexOf('(');
            int end = selected.LastIndexOf(')');
            return selected.Substring(start + 1, end - start - 1);
        }
        return null;
    }

    private async void LoadTeamsAsync()
    {
        cbTeams.Items.Clear();
        cbTeams.Enabled = false;
        Cursor = Cursors.WaitCursor;

        try
        {
            var teams = await _repo.GetTeams();

            foreach (var team in teams)
            {
                cbTeams.Items.Add($"{team.Country} ({team.FifaCode})");
            }

            if (!string.IsNullOrEmpty(_appSettings.FavTeam))
            {
                int idx = cbTeams.Items.IndexOf(_appSettings.FavTeam);
                if (idx >= 0)
                    cbTeams.SelectedIndex = idx;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            cbTeams.Enabled = true;
            Cursor = Cursors.Default;
        }
    }

    private async void LoadPlayersAsync()
    {
        flpAllPlayers.Controls.Clear();
        Cursor = Cursors.WaitCursor;

        try
        {
            string fifaCode = GetSelectedFifaCode();
            var players = await _repo.GetPlayersByCountry(fifaCode);
            var sortedPlayers = players.ToList();
            sortedPlayers.Sort((x, y) => x.ShirtNumber.CompareTo(y.ShirtNumber));

            foreach (var player in sortedPlayers)
            {
                var panel = new Panel
                {
                    Width = flpAllPlayers.Width,
                    Height = 60,
                    Margin = new Padding(5)
                };

                var playerImage = new PictureBox
                {
                    Width = 40,
                    Height = 40,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = Image.FromFile("Images/default-player.png")
                };

                var lbl = new Label
                {
                    AutoSize = true,
                    Location = new Point(50, 10),
                    Text = $"{player.ShirtNumber}. {player.Name} \n[{player.Position}]" + (player.Captain ? " (C)" : "")
                };

                panel.Controls.Add(playerImage);
                panel.Controls.Add(lbl);

                flpAllPlayers.Controls.Add(panel);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading players: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    #endregion
}
