using DAL.Config;
using DAL.Enums;
using DAL.Repositories;
using System.Globalization;

namespace WinFormsApp;

public partial class Form1 : Form
{
    private AppSettings _appSettings;
    private IRepository _repo;

    public Form1()
    {
        InitializeComponent();

        RefreshSettings();

        this.KeyPreview = true;
        this.KeyDown += Form1_KeyDown;
        this.FormClosing += Form1_FormClosing;
    }

    #region EVENTS

    private void btnSaveSettings_Click(object sender, EventArgs e)
    {
        _appSettings.LanguageAndRegion = rbEn.Checked ? "en-US" : "hr-HR";
        _appSettings.WorldCupGender = rbMen.Checked ? WorldCupGender.Men : WorldCupGender.Women;

        Cursor = Cursors.WaitCursor;

        RepositoryFactory.SaveAppSettings(_appSettings);
        RefreshSettings();

        Cursor = Cursors.Default;
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
        if (tcMain.SelectedTab == tpFavs)
        {
            cbTeams.Items.Clear();
            cbTeams.ResetText();
            flpAllPlayers.AutoScroll = true;
            flpFavPlayers.AutoScroll = true;
            flpAllPlayers.BorderStyle = BorderStyle.FixedSingle;
            flpFavPlayers.BorderStyle = BorderStyle.FixedSingle;
            flpAllPlayers.Controls.Clear();
            flpFavPlayers.Controls.Clear();
            flpAllPlayers.AllowDrop = true;
            flpFavPlayers.AllowDrop = true;
            LoadTeamsForCbAsync();
        }
    }

    private void btnSaveFavs_Click(object sender, EventArgs e)
    {
        if (cbTeams.SelectedItem == null)
        {
            MessageBox.Show(Resource.InfoSelFavTeam, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (flpFavPlayers.Controls.Count == 0)
        {
            MessageBox.Show(Resource.InfoSelFavPlayers, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _appSettings.FavTeam = cbTeams.SelectedItem.ToString();

        RepositoryFactory.SaveAppSettings(_appSettings);
    }

    private void cbTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPlayersForFlpAsync();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        var result = MessageBox.Show(
            Resource.PromptExit,
            Resource.ConfirmExit,
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1
        );

        if (result != DialogResult.OK)
        {
            e.Cancel = true;
        }
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control)
        {
            int tabIndex = -1;
            switch (e.KeyCode)
            {
                case Keys.D1: tabIndex = 0; break;
                case Keys.D2: tabIndex = 1; break;
                case Keys.D3: tabIndex = 2; break;
                case Keys.D4: tabIndex = 3; break;
            }
            if (tabIndex >= 0 && tabIndex < tcMain.TabPages.Count)
            {
                tcMain.SelectedIndex = tabIndex;
                e.Handled = true;
            }
        }
    }

    #endregion

    #region HELPERS

    private void handleLocalization()
    {
        gbLangAndReg.Text = Resource.LangAndReg;
        rbEn.Text = Resource.English;
        rbHr.Text = Resource.Croatian;
        gbWCGender.Text = Resource.WCGender;
        rbMen.Text = Resource.Men;
        rbWomen.Text = Resource.Women;
        btnSaveSettings.Text = Resource.Save;
        tpFavs.Text = Resource.Favs;
        tpSettings.Text = Resource.Settings;
        tpMatchStats.Text = Resource.MatchStats;
        tpPlayerStats.Text = Resource.PlayerStats;
        btnSaveFavs.Text = Resource.SaveFavs;
        lbFavPlayers.Text = Resource.SelFavPlayers;
        lbSelectFavTeam.Text = Resource.SelFavTeam;
        miHelp.Text = Resource.Help;
        miPrint.Text = Resource.Print;
    }

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

    private async void LoadTeamsForCbAsync()
    {
        cbTeams.Items.Clear();
        cbTeams.Enabled = false;
        Cursor = Cursors.WaitCursor;

        try
        {
            var teams = await _repo.GetTeams();
            var sortedTeams = teams.ToList();
            sortedTeams.Sort((x, y) => string.Compare(x.Country, y.Country, StringComparison.Ordinal));

            foreach (var team in sortedTeams)
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

    private async void LoadPlayersForFlpAsync()
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
                var playerControl = new PlayerUserControl();
                Image? image = null;
                string imgPath = $"Images/{player.Name.ToLower().Replace(' ', '-')}.jpg";
                if (File.Exists(imgPath))
                    image = Image.FromFile(imgPath);
                playerControl.SetPlayer(player, image);
                flpAllPlayers.Controls.Add(playerControl);
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

    private void RefreshSettings()
    {
        _appSettings = RepositoryFactory.GetAppSettings();
        _repo = RepositoryFactory.GetRepository();

        if(Thread.CurrentThread.CurrentUICulture.Name == _appSettings.LanguageAndRegion) return;

        var culture = new CultureInfo(_appSettings.LanguageAndRegion);
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;

        handleLocalization();
    }

    #endregion
}
