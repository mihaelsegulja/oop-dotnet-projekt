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
            flpAllPlayers.DragEnter += FlpPanel_DragEnter;
            flpFavPlayers.DragEnter += FlpPanel_DragEnter;
            flpAllPlayers.DragDrop += FlpAllPlayers_DragDrop;
            flpFavPlayers.DragDrop += FlpFavPlayers_DragDrop;
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
        _appSettings.FavPlayersList = flpFavPlayers.Controls
        .OfType<PlayerUserControl>()
        .Select(ctrl => ctrl.Player.Name)
        .ToList();

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
                case Keys.D5: tabIndex = 4; break;
            }
            if (tabIndex >= 0 && tabIndex < tcMain.TabPages.Count)
            {
                tcMain.SelectedIndex = tabIndex;
                e.Handled = true;
            }
        }
    }
    private void FlpPanel_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(List<PlayerUserControl>)))
            e.Effect = DragDropEffects.Move;
        else
            e.Effect = DragDropEffects.None;
    }

    private void FlpAllPlayers_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(List<PlayerUserControl>)))
        {
            var controls = (List<PlayerUserControl>)e.Data.GetData(typeof(List<PlayerUserControl>));
            foreach (var ctrl in controls)
            {
                if (flpFavPlayers.Controls.Contains(ctrl))
                {
                    flpFavPlayers.Controls.Remove(ctrl);
                    ctrl.UpdateFavorite(false);
                    flpAllPlayers.Controls.Add(ctrl);
                }
            }
        }
    }

    private void FlpFavPlayers_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(List<PlayerUserControl>)))
        {
            var controls = (List<PlayerUserControl>)e.Data.GetData(typeof(List<PlayerUserControl>));
            foreach (var ctrl in controls)
            {
                if (flpAllPlayers.Controls.Contains(ctrl))
                {
                    flpAllPlayers.Controls.Remove(ctrl);
                    ctrl.UpdateFavorite(true);
                    flpFavPlayers.Controls.Add(ctrl);
                }
            }
        }
    }


    #endregion

    #region HELPERS

    private void HandleLocalization()
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
        flpFavPlayers.Controls.Clear();
        Cursor = Cursors.WaitCursor;

        try
        {
            string fifaCode = GetSelectedFifaCode();
            var players = await _repo.GetPlayersByCountry(fifaCode);
            var sortedPlayers = players.ToList();
            sortedPlayers.Sort((x, y) => x.ShirtNumber.CompareTo(y.ShirtNumber));

            string? selectedTeam = _appSettings.FavTeam;

            foreach (var player in sortedPlayers)
            {
                var playerControl = new PlayerUserControl();
                Image? image = null;
                string imgPath = $"Images/{player.Name.ToLower().Replace(' ', '-')}.jpg";
                if (File.Exists(imgPath))
                    image = Image.FromFile(imgPath);

                if (_appSettings.FavPlayersList != null
                    && _appSettings.FavPlayersList.Contains(player.Name)
                    && cbTeams.SelectedItem?.ToString() == selectedTeam)
                {
                    playerControl.SetPlayer(player, image, true);
                    flpFavPlayers.Controls.Add(playerControl);
                }
                else
                {
                    playerControl.SetPlayer(player, image, false);
                    flpAllPlayers.Controls.Add(playerControl);
                }
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

        HandleLocalization();
    }

    public void ClearAllPlayerSelections()
    {
        foreach (var ctrl in flpAllPlayers.Controls.OfType<PlayerUserControl>())
            ctrl.SetSelected(false);
        foreach (var ctrl in flpFavPlayers.Controls.OfType<PlayerUserControl>())
            ctrl.SetSelected(false);
    }

    public List<PlayerUserControl> GetSelectedPlayerControls()
    {
        var selected = flpAllPlayers.Controls.OfType<PlayerUserControl>().Where(c => c.IsSelected).ToList();
        selected.AddRange(flpFavPlayers.Controls.OfType<PlayerUserControl>().Where(c => c.IsSelected));
        return selected;
    }

    #endregion
}
