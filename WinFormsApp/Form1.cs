using DAL.Config;
using DAL.Enums;
using DAL.Repositories;
using Localization;
using System.Drawing.Printing;
using System.Globalization;
using WinFormsApp.Models;

namespace WinFormsApp;

public partial class Form1 : Form
{
    private IAppSettingsRepository _appSettingsRepo;
    private AppSettings _appSettings;
    private IRepository _repo;

    private PrintDocument printDocument = new PrintDocument();
    private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
    private DataGridView? printSourceDgv = null;
    private string printTitle = "";

    public Form1()
    {
        InitializeComponent();
        LoadAppSettings();
        printDocument.PrintPage += PrintDocument_PrintPage;
    }

    #region EVENTS

    private void btnSaveSettings_Click(object sender, EventArgs e)
    {
        _appSettings.LanguageAndRegion = rbEn.Checked ? "en-US" : "hr-HR";
        _appSettings.WorldCupGender = rbMen.Checked ? WorldCupGender.Men : WorldCupGender.Women;

        if (rbAutodetect.Checked)
            _appSettings.RepositoryType = RepositoryType.AutoDetect;
        else if (rbLocalFiles.Checked)
            _appSettings.RepositoryType = RepositoryType.FileSystem;
        else if (rbWebApi.Checked)
            _appSettings.RepositoryType = RepositoryType.WebApi;

        Cursor = Cursors.WaitCursor;
        pbMain.Visible = true;

        _appSettingsRepo.SaveSettings(_appSettings);
        LoadAppSettings();

        Cursor = Cursors.Default;
        pbMain.Visible = false;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        rbEn.Checked = _appSettings.LanguageAndRegion == "en-US";
        rbHr.Checked = _appSettings.LanguageAndRegion == "hr-HR";

        rbMen.Checked = _appSettings.WorldCupGender == WorldCupGender.Men;
        rbWomen.Checked = _appSettings.WorldCupGender == WorldCupGender.Women;

        rbAutodetect.Checked = _appSettings.RepositoryType == RepositoryType.AutoDetect;
        rbLocalFiles.Checked = _appSettings.RepositoryType == RepositoryType.FileSystem;
        rbWebApi.Checked = _appSettings.RepositoryType == RepositoryType.WebApi;
    }

    private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (tcMain.SelectedTab)
        {
            case TabPage tp when tp == tpFavs:
                miPrint.Enabled = false;
                LoadTeamsForCbAsync();
                break;
            case TabPage tp when tp == tpMatchStats:
                miPrint.Enabled = true;
                LoadMatchAttendanceForDgvAsync();
                break;
            case TabPage tp when tp == tpPlayerStats:
                miPrint.Enabled = true;
                LoadPlayerStatsForDgvAsync();
                break;
            case TabPage tp when tp == tpSettings:
                miPrint.Enabled = false;
                break;
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

        Cursor = Cursors.WaitCursor;
        pbMain.Visible = true;

        _appSettings.FavTeam = cbTeams.SelectedItem.ToString();
        _appSettings.FavPlayersList = flpFavPlayers.Controls
            .OfType<PlayerUserControl>()
            .Select(ctrl => ctrl.Player.Name)
            .ToList();

        _appSettingsRepo.SaveSettings(_appSettings);

        Cursor = Cursors.Default;
        pbMain.Visible = false;
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
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1);

        if (result == DialogResult.Yes)
            _appSettingsRepo.SaveSettings(_appSettings);
        else
            e.Cancel = true;
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
                case Keys.Q:
                    Close();
                    e.Handled = true;
                    break;
                case Keys.P:
                    if (miPrint.Enabled)
                        miPrint.PerformClick();
                    e.Handled = true;
                    break;
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
            int availableSlots = 3 - flpFavPlayers.Controls.Count;
            var toAdd = controls.Where(ctrl => flpAllPlayers.Controls.Contains(ctrl)).Take(availableSlots).ToList();

            if (toAdd.Count < controls.Count)
            {
                MessageBox.Show(Resource.InfoLimitReached3FavPlayers, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

    private void miPrint_Click(object sender, EventArgs e)
    {
        if (tcMain.SelectedTab == tpPlayerStats)
        {
            printSourceDgv = dgvPlayerStats;
            printTitle = Resource.PlayerStats;
        }
        else if (tcMain.SelectedTab == tpMatchStats)
        {
            printSourceDgv = dgvMatchStats;
            printTitle = Resource.MatchStats;
        }
        else
        {
            printSourceDgv = null;
            return;
        }

        printPreviewDialog.Document = printDocument;
        printPreviewDialog.ShowDialog();
    }

    private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
    {
        if (printSourceDgv == null) return;

        Font headerFont = new Font("Comic Sans MS", 12, FontStyle.Bold);
        Font cellFont = new Font("Comic Sans MS", 10);
        int x = e.MarginBounds.Left;
        int y = e.MarginBounds.Top;
        int rowHeight = cellFont.Height + 8;
        int imageSize = 40;

        e.Graphics.DrawString(printTitle, headerFont, Brushes.Black, x, y);
        y += rowHeight + 10;

        int colX = x;
        foreach (DataGridViewColumn col in printSourceDgv.Columns)
        {
            if (!col.Visible) continue;
            e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black, colX, y);
            colX += (col is DataGridViewImageColumn) ? imageSize + 10 : 125;
        }
        y += rowHeight;

        foreach (DataGridViewRow row in printSourceDgv.Rows)
        {
            if (row.IsNewRow) continue;
            colX = x;
            foreach (DataGridViewColumn col in printSourceDgv.Columns)
            {
                if (!col.Visible) continue;

                if (col is DataGridViewImageColumn)
                {
                    var cellValue = row.Cells[col.Index].Value;
                    if (cellValue is Image img)
                    {
                        e.Graphics.DrawImage(img, colX, y, imageSize, imageSize);
                    }
                    colX += imageSize + 10;
                }
                else
                {
                    var value = row.Cells[col.Index].FormattedValue?.ToString() ?? "";
                    e.Graphics.DrawString(value, cellFont, Brushes.Black, colX, y + (imageSize - rowHeight) / 2);
                    colX += 125;
                }
            }
            y += Math.Max(rowHeight, imageSize);
        }
    }

    private void miControls_Click(object sender, EventArgs e)
    {
        string message =
            "Keyboard Shortcuts:\n\n" +
            "Ctrl + 1 ... 4     - Switch between tabs\n" +
            "Ctrl + Q           - Quit\n" +
            "Ctrl + P           - Print (when available)\n" +
            "Ctrl + Mouse Click - Multi select";
        MessageBox.Show(message, Resource.Controls, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion

    #region HELPERS

    private void HandleLocalization()
    {
        this.Text = Resource.WorldCup;
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
        miControls.Text = Resource.Controls;
        miPrint.Text = Resource.Print;
        gbRepoType.Text = Resource.RepoType;
        rbAutodetect.Text = Resource.Autodetect;
        rbLocalFiles.Text = Resource.LocalFiles;
        rbWebApi.Text = Resource.WebAPI;
    }

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

    private async void LoadTeamsForCbAsync()
    {
        cbTeams.Items.Clear();
        cbTeams.Enabled = false;
        cbTeams.ResetText();
        flpAllPlayers.Controls.Clear();
        flpFavPlayers.Controls.Clear();
        Cursor = Cursors.WaitCursor;
        pbMain.Visible = true;

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
            pbMain.Visible = false;
        }
    }

    private async void LoadPlayersForFlpAsync()
    {
        flpAllPlayers.Controls.Clear();
        flpFavPlayers.Controls.Clear();
        Cursor = Cursors.WaitCursor;
        pbMain.Visible = true;

        try
        {
            string fifaCode = GetFifaCode(cbTeams.SelectedItem.ToString());
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
            pbMain.Visible = false;
        }
    }

    private void LoadAppSettings()
    {
        _appSettingsRepo = RepositoryFactory.GetAppSettingsRepository();
        _appSettings = _appSettingsRepo.LoadSettings();
        _repo = RepositoryFactory.GetRepository(_appSettings.RepositoryType);

        if(Thread.CurrentThread.CurrentUICulture.Name != _appSettings.LanguageAndRegion)
        {
            var culture = new CultureInfo(_appSettings.LanguageAndRegion);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            HandleLocalization();
        }

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

    private async Task<List<PlayerStats>> GetPlayerStatsAsync(string fifaCode)
    {
        var teamPlayers = await _repo.GetPlayersByCountry(fifaCode);
        var playerNames = new HashSet<string>(teamPlayers.Select(p => p.Name));

        var matches = await _repo.GetMatchesByTeam(fifaCode);
        var playerStatsDict = new Dictionary<string, PlayerStats>();

        foreach (var match in matches)
        {
            bool isHome = match.HomeTeam.Code == fifaCode;
            bool isAway = match.AwayTeam.Code == fifaCode;

            if (!isHome && !isAway)
                continue;

            var events = isHome ? match.HomeTeamEvents : match.AwayTeamEvents;

            foreach (var ev in events)
            {
                if (string.IsNullOrEmpty(ev.Player) || !playerNames.Contains(ev.Player)) 
                    continue;

                if (!playerStatsDict.TryGetValue(ev.Player, out var stats))
                {
                    string imgPath = $"Images/{ev.Player.ToLower().Replace(' ', '-')}.jpg";
                    var img = File.Exists(imgPath) ? Image.FromFile(imgPath) : Image.FromFile("Images/default-player.png");

                    stats = new PlayerStats
                    {
                        PlayerName = ev.Player,
                        PlayerImage = img,
                        Goals = 0,
                        YellowCards = 0
                    };
                    playerStatsDict[ev.Player] = stats;
                }

                if (ev.TypeOfEvent == TypeOfEvent.Goal || ev.TypeOfEvent == TypeOfEvent.GoalPenalty)
                    stats.Goals++;
                if (ev.TypeOfEvent == TypeOfEvent.YellowCard)
                    stats.YellowCards++;
            }
        }

        return playerStatsDict.Values
            .OrderByDescending(s => s.Goals)
            .ThenByDescending(s => s.YellowCards)
            .ToList();
    }

    private async void LoadPlayerStatsForDgvAsync()
    {
        dgvPlayerStats.Rows.Clear();
        dgvPlayerStats.Columns.Clear();

        dgvPlayerStats.Columns.Add(new DataGridViewImageColumn 
        { 
            Name = "PlayerImage",
            HeaderText = Resource.Image,
            ImageLayout = DataGridViewImageCellLayout.Zoom
        });
        dgvPlayerStats.Columns.Add("Name", Resource.PlayerName);
        dgvPlayerStats.Columns.Add("Goals", Resource.Goals);
        dgvPlayerStats.Columns.Add("YellowCards", Resource.YellowCards);

        dgvPlayerStats.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dgvPlayerStats.Columns["Goals"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        dgvPlayerStats.Columns["YellowCards"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

        Cursor = Cursors.WaitCursor;
        pbMain.Visible = true;

        try
        {
            string fifaCode = GetFifaCode(_appSettings.FavTeam);
            if (string.IsNullOrEmpty(fifaCode)) return;

            var stats = await GetPlayerStatsAsync(fifaCode);

            foreach (var s in stats)
                dgvPlayerStats.Rows.Add(s.PlayerImage, s.PlayerName, s.Goals, s.YellowCards);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading player stats: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
            pbMain.Visible = false;
        }
        
        dgvPlayerStats.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
    }

    private async Task<List<MatchAttendanceInfo>> GetMatchAttendanceAsync(string fifaCode)
    {
        var matches = await _repo.GetMatchesByTeam(fifaCode);
        return matches
            .OrderByDescending(m => m.Attendance)
            .Select(m => new MatchAttendanceInfo
            {
                DateTime = m.Datetime,
                HomeTeam = m.HomeTeamCountry,
                AwayTeam = m.AwayTeamCountry,
                Venue = m.Venue,
                Attendance = m.Attendance
            })
            .ToList();
    }

    private async void LoadMatchAttendanceForDgvAsync()
    {
        dgvMatchStats.Rows.Clear();
        dgvMatchStats.Columns.Clear();

        dgvMatchStats.Columns.Add("DateTime", Resource.DateTime);
        dgvMatchStats.Columns.Add("HomeTeam", Resource.HomeTeam);
        dgvMatchStats.Columns.Add("AwayTeam", Resource.AwayTeam);
        dgvMatchStats.Columns.Add("Venue", Resource.Venue);
        dgvMatchStats.Columns.Add("Attendance", Resource.Attendance);

        Cursor = Cursors.WaitCursor;
        pbMain.Visible = true;

        try
        {
            string fifaCode = GetFifaCode(_appSettings.FavTeam);
            if (string.IsNullOrEmpty(fifaCode)) return;

            var matches = await GetMatchAttendanceAsync(fifaCode);

            foreach (var m in matches)
            {
                dgvMatchStats.Rows.Add(
                    m.DateTime.ToString("g"),
                    m.HomeTeam,
                    m.AwayTeam,
                    m.Venue,
                    m.Attendance
                );
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading match attendance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
            pbMain.Visible = false;
        }

        dgvMatchStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMatchStats.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
    }

    #endregion
}
