﻿namespace WinFormsApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        tcMain = new TabControl();
        tpSettings = new TabPage();
        gbRepoType = new GroupBox();
        rbLocalFiles = new RadioButton();
        rbWebApi = new RadioButton();
        gbLangAndReg = new GroupBox();
        rbHr = new RadioButton();
        rbEn = new RadioButton();
        gbWCGender = new GroupBox();
        rbWomen = new RadioButton();
        rbMen = new RadioButton();
        btnSaveSettings = new Button();
        tpFavs = new TabPage();
        flpFavPlayers = new FlowLayoutPanel();
        flpAllPlayers = new FlowLayoutPanel();
        lbFavPlayers = new Label();
        lbSelectFavTeam = new Label();
        btnSaveFavs = new Button();
        cbTeams = new ComboBox();
        tpPlayerStats = new TabPage();
        dgvPlayerStats = new DataGridView();
        PlayerImage = new DataGridViewImageColumn();
        PlayerName = new DataGridViewTextBoxColumn();
        Goals = new DataGridViewTextBoxColumn();
        YellowCards = new DataGridViewTextBoxColumn();
        tpMatchStats = new TabPage();
        dgvMatchStats = new DataGridView();
        Date = new DataGridViewTextBoxColumn();
        HomeTeam = new DataGridViewTextBoxColumn();
        AwayTeam = new DataGridViewTextBoxColumn();
        Venue = new DataGridViewTextBoxColumn();
        Attendance = new DataGridViewTextBoxColumn();
        msMain = new MenuStrip();
        miPrint = new ToolStripMenuItem();
        miControls = new ToolStripMenuItem();
        pbMain = new ProgressBar();
        rbAutodetect = new RadioButton();
        tcMain.SuspendLayout();
        tpSettings.SuspendLayout();
        gbRepoType.SuspendLayout();
        gbLangAndReg.SuspendLayout();
        gbWCGender.SuspendLayout();
        tpFavs.SuspendLayout();
        tpPlayerStats.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPlayerStats).BeginInit();
        tpMatchStats.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvMatchStats).BeginInit();
        msMain.SuspendLayout();
        SuspendLayout();
        // 
        // tcMain
        // 
        tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tcMain.Controls.Add(tpSettings);
        tcMain.Controls.Add(tpFavs);
        tcMain.Controls.Add(tpPlayerStats);
        tcMain.Controls.Add(tpMatchStats);
        tcMain.Location = new Point(3, 31);
        tcMain.Name = "tcMain";
        tcMain.SelectedIndex = 0;
        tcMain.Size = new Size(1023, 623);
        tcMain.TabIndex = 0;
        tcMain.SelectedIndexChanged += tcMain_SelectedIndexChanged;
        // 
        // tpSettings
        // 
        tpSettings.BackColor = Color.WhiteSmoke;
        tpSettings.Controls.Add(gbRepoType);
        tpSettings.Controls.Add(gbLangAndReg);
        tpSettings.Controls.Add(gbWCGender);
        tpSettings.Controls.Add(btnSaveSettings);
        tpSettings.Location = new Point(4, 29);
        tpSettings.Name = "tpSettings";
        tpSettings.Padding = new Padding(3);
        tpSettings.Size = new Size(1015, 590);
        tpSettings.TabIndex = 0;
        tpSettings.Text = "Settings";
        // 
        // gbRepoType
        // 
        gbRepoType.Controls.Add(rbAutodetect);
        gbRepoType.Controls.Add(rbLocalFiles);
        gbRepoType.Controls.Add(rbWebApi);
        gbRepoType.Location = new Point(655, 126);
        gbRepoType.Name = "gbRepoType";
        gbRepoType.Size = new Size(242, 144);
        gbRepoType.TabIndex = 5;
        gbRepoType.TabStop = false;
        gbRepoType.Text = "Repo type";
        // 
        // rbLocalFiles
        // 
        rbLocalFiles.AutoSize = true;
        rbLocalFiles.Location = new Point(6, 70);
        rbLocalFiles.Name = "rbLocalFiles";
        rbLocalFiles.Size = new Size(102, 24);
        rbLocalFiles.TabIndex = 3;
        rbLocalFiles.TabStop = true;
        rbLocalFiles.Text = "Local Files";
        rbLocalFiles.UseVisualStyleBackColor = true;
        // 
        // rbWebApi
        // 
        rbWebApi.AutoSize = true;
        rbWebApi.Location = new Point(6, 40);
        rbWebApi.Name = "rbWebApi";
        rbWebApi.Size = new Size(93, 24);
        rbWebApi.TabIndex = 2;
        rbWebApi.TabStop = true;
        rbWebApi.Text = "Web API";
        rbWebApi.UseVisualStyleBackColor = true;
        // 
        // gbLangAndReg
        // 
        gbLangAndReg.Controls.Add(rbHr);
        gbLangAndReg.Controls.Add(rbEn);
        gbLangAndReg.Location = new Point(118, 126);
        gbLangAndReg.Name = "gbLangAndReg";
        gbLangAndReg.Size = new Size(243, 113);
        gbLangAndReg.TabIndex = 3;
        gbLangAndReg.TabStop = false;
        gbLangAndReg.Text = "Language and region";
        // 
        // rbHr
        // 
        rbHr.AutoSize = true;
        rbHr.Location = new Point(6, 70);
        rbHr.Name = "rbHr";
        rbHr.Size = new Size(89, 24);
        rbHr.TabIndex = 1;
        rbHr.TabStop = true;
        rbHr.Text = "Croatian";
        rbHr.UseVisualStyleBackColor = true;
        // 
        // rbEn
        // 
        rbEn.AutoSize = true;
        rbEn.Location = new Point(6, 40);
        rbEn.Name = "rbEn";
        rbEn.Size = new Size(78, 24);
        rbEn.TabIndex = 0;
        rbEn.TabStop = true;
        rbEn.Text = "English";
        rbEn.UseVisualStyleBackColor = true;
        // 
        // gbWCGender
        // 
        gbWCGender.Controls.Add(rbWomen);
        gbWCGender.Controls.Add(rbMen);
        gbWCGender.Location = new Point(386, 126);
        gbWCGender.Name = "gbWCGender";
        gbWCGender.Size = new Size(242, 113);
        gbWCGender.TabIndex = 4;
        gbWCGender.TabStop = false;
        gbWCGender.Text = "World Cup gender";
        // 
        // rbWomen
        // 
        rbWomen.AutoSize = true;
        rbWomen.Location = new Point(6, 70);
        rbWomen.Name = "rbWomen";
        rbWomen.Size = new Size(80, 24);
        rbWomen.TabIndex = 3;
        rbWomen.TabStop = true;
        rbWomen.Text = "Women";
        rbWomen.UseVisualStyleBackColor = true;
        // 
        // rbMen
        // 
        rbMen.AutoSize = true;
        rbMen.Location = new Point(6, 40);
        rbMen.Name = "rbMen";
        rbMen.Size = new Size(59, 24);
        rbMen.TabIndex = 2;
        rbMen.TabStop = true;
        rbMen.Text = "Men";
        rbMen.UseVisualStyleBackColor = true;
        // 
        // btnSaveSettings
        // 
        btnSaveSettings.Location = new Point(367, 388);
        btnSaveSettings.Name = "btnSaveSettings";
        btnSaveSettings.Size = new Size(220, 49);
        btnSaveSettings.TabIndex = 0;
        btnSaveSettings.Text = "Save";
        btnSaveSettings.UseVisualStyleBackColor = true;
        btnSaveSettings.Click += btnSaveSettings_Click;
        // 
        // tpFavs
        // 
        tpFavs.Controls.Add(flpFavPlayers);
        tpFavs.Controls.Add(flpAllPlayers);
        tpFavs.Controls.Add(lbFavPlayers);
        tpFavs.Controls.Add(lbSelectFavTeam);
        tpFavs.Controls.Add(btnSaveFavs);
        tpFavs.Controls.Add(cbTeams);
        tpFavs.Location = new Point(4, 29);
        tpFavs.Name = "tpFavs";
        tpFavs.Padding = new Padding(3);
        tpFavs.Size = new Size(1015, 590);
        tpFavs.TabIndex = 2;
        tpFavs.Text = "Favorites";
        tpFavs.UseVisualStyleBackColor = true;
        // 
        // flpFavPlayers
        // 
        flpFavPlayers.AllowDrop = true;
        flpFavPlayers.AutoScroll = true;
        flpFavPlayers.BorderStyle = BorderStyle.Fixed3D;
        flpFavPlayers.Location = new Point(511, 126);
        flpFavPlayers.Name = "flpFavPlayers";
        flpFavPlayers.Size = new Size(482, 348);
        flpFavPlayers.TabIndex = 6;
        flpFavPlayers.DragDrop += FlpFavPlayers_DragDrop;
        flpFavPlayers.DragEnter += FlpPanel_DragEnter;
        // 
        // flpAllPlayers
        // 
        flpAllPlayers.AllowDrop = true;
        flpAllPlayers.AutoScroll = true;
        flpAllPlayers.BorderStyle = BorderStyle.Fixed3D;
        flpAllPlayers.Location = new Point(23, 126);
        flpAllPlayers.Name = "flpAllPlayers";
        flpAllPlayers.Size = new Size(482, 348);
        flpAllPlayers.TabIndex = 5;
        flpAllPlayers.DragDrop += FlpAllPlayers_DragDrop;
        flpAllPlayers.DragEnter += FlpPanel_DragEnter;
        // 
        // lbFavPlayers
        // 
        lbFavPlayers.AutoSize = true;
        lbFavPlayers.Location = new Point(37, 83);
        lbFavPlayers.Name = "lbFavPlayers";
        lbFavPlayers.Size = new Size(173, 20);
        lbFavPlayers.TabIndex = 4;
        lbFavPlayers.Text = "Select favorite players:";
        // 
        // lbSelectFavTeam
        // 
        lbSelectFavTeam.AutoSize = true;
        lbSelectFavTeam.Location = new Point(37, 27);
        lbSelectFavTeam.Name = "lbSelectFavTeam";
        lbSelectFavTeam.Size = new Size(157, 20);
        lbSelectFavTeam.TabIndex = 3;
        lbSelectFavTeam.Text = "Select favorite team:";
        // 
        // btnSaveFavs
        // 
        btnSaveFavs.Location = new Point(129, 505);
        btnSaveFavs.Name = "btnSaveFavs";
        btnSaveFavs.Size = new Size(733, 48);
        btnSaveFavs.TabIndex = 2;
        btnSaveFavs.Text = "Save favorites";
        btnSaveFavs.UseVisualStyleBackColor = true;
        btnSaveFavs.Click += btnSaveFavs_Click;
        // 
        // cbTeams
        // 
        cbTeams.FormattingEnabled = true;
        cbTeams.Location = new Point(200, 24);
        cbTeams.Name = "cbTeams";
        cbTeams.Size = new Size(280, 28);
        cbTeams.TabIndex = 0;
        cbTeams.SelectedIndexChanged += cbTeams_SelectedIndexChanged;
        // 
        // tpPlayerStats
        // 
        tpPlayerStats.Controls.Add(dgvPlayerStats);
        tpPlayerStats.Location = new Point(4, 29);
        tpPlayerStats.Name = "tpPlayerStats";
        tpPlayerStats.Padding = new Padding(3);
        tpPlayerStats.Size = new Size(1015, 590);
        tpPlayerStats.TabIndex = 3;
        tpPlayerStats.Text = "Player Stats";
        tpPlayerStats.UseVisualStyleBackColor = true;
        // 
        // dgvPlayerStats
        // 
        dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
        dgvPlayerStats.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.BackColor = SystemColors.Control;
        dataGridViewCellStyle2.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
        dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvPlayerStats.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvPlayerStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPlayerStats.Columns.AddRange(new DataGridViewColumn[] { PlayerImage, PlayerName, Goals, YellowCards });
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.BackColor = SystemColors.Window;
        dataGridViewCellStyle3.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvPlayerStats.DefaultCellStyle = dataGridViewCellStyle3;
        dgvPlayerStats.Location = new Point(0, 0);
        dgvPlayerStats.Name = "dgvPlayerStats";
        dgvPlayerStats.RowHeadersWidth = 51;
        dgvPlayerStats.RowTemplate.Height = 65;
        dgvPlayerStats.Size = new Size(1015, 590);
        dgvPlayerStats.TabIndex = 0;
        // 
        // PlayerImage
        // 
        PlayerImage.HeaderText = "Image";
        PlayerImage.MinimumWidth = 6;
        PlayerImage.Name = "PlayerImage";
        PlayerImage.Width = 125;
        // 
        // PlayerName
        // 
        PlayerName.HeaderText = "Name";
        PlayerName.MinimumWidth = 6;
        PlayerName.Name = "PlayerName";
        PlayerName.Width = 125;
        // 
        // Goals
        // 
        Goals.HeaderText = "Goals";
        Goals.MinimumWidth = 6;
        Goals.Name = "Goals";
        Goals.Width = 125;
        // 
        // YellowCards
        // 
        YellowCards.HeaderText = "Yellow Cards";
        YellowCards.MinimumWidth = 6;
        YellowCards.Name = "YellowCards";
        YellowCards.Width = 125;
        // 
        // tpMatchStats
        // 
        tpMatchStats.Controls.Add(dgvMatchStats);
        tpMatchStats.Location = new Point(4, 29);
        tpMatchStats.Name = "tpMatchStats";
        tpMatchStats.Padding = new Padding(3);
        tpMatchStats.Size = new Size(1015, 590);
        tpMatchStats.TabIndex = 4;
        tpMatchStats.Text = "Match Stats";
        tpMatchStats.UseVisualStyleBackColor = true;
        // 
        // dgvMatchStats
        // 
        dataGridViewCellStyle4.BackColor = Color.WhiteSmoke;
        dgvMatchStats.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle5.BackColor = SystemColors.Control;
        dataGridViewCellStyle5.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
        dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
        dgvMatchStats.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
        dgvMatchStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvMatchStats.Columns.AddRange(new DataGridViewColumn[] { Date, HomeTeam, AwayTeam, Venue, Attendance });
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle6.BackColor = SystemColors.Window;
        dataGridViewCellStyle6.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
        dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
        dgvMatchStats.DefaultCellStyle = dataGridViewCellStyle6;
        dgvMatchStats.Location = new Point(0, 0);
        dgvMatchStats.Name = "dgvMatchStats";
        dgvMatchStats.RowHeadersWidth = 51;
        dgvMatchStats.Size = new Size(1015, 590);
        dgvMatchStats.TabIndex = 0;
        // 
        // Date
        // 
        Date.HeaderText = "Date";
        Date.MinimumWidth = 6;
        Date.Name = "Date";
        Date.Width = 125;
        // 
        // HomeTeam
        // 
        HomeTeam.HeaderText = "Home Team";
        HomeTeam.MinimumWidth = 6;
        HomeTeam.Name = "HomeTeam";
        HomeTeam.Width = 125;
        // 
        // AwayTeam
        // 
        AwayTeam.HeaderText = "Away Team";
        AwayTeam.MinimumWidth = 6;
        AwayTeam.Name = "AwayTeam";
        AwayTeam.Width = 125;
        // 
        // Venue
        // 
        Venue.HeaderText = "Venue";
        Venue.MinimumWidth = 6;
        Venue.Name = "Venue";
        Venue.Width = 125;
        // 
        // Attendance
        // 
        Attendance.HeaderText = "Attendance";
        Attendance.MinimumWidth = 6;
        Attendance.Name = "Attendance";
        Attendance.Width = 125;
        // 
        // msMain
        // 
        msMain.ImageScalingSize = new Size(20, 20);
        msMain.Items.AddRange(new ToolStripItem[] { miPrint, miControls });
        msMain.Location = new Point(0, 0);
        msMain.Name = "msMain";
        msMain.Size = new Size(1027, 28);
        msMain.TabIndex = 1;
        // 
        // miPrint
        // 
        miPrint.Name = "miPrint";
        miPrint.Size = new Size(53, 24);
        miPrint.Text = "Print";
        miPrint.Click += miPrint_Click;
        // 
        // miControls
        // 
        miControls.Name = "miControls";
        miControls.Size = new Size(78, 24);
        miControls.Text = "Controls";
        miControls.Click += miControls_Click;
        // 
        // pbMain
        // 
        pbMain.Dock = DockStyle.Bottom;
        pbMain.Location = new Point(0, 625);
        pbMain.Name = "pbMain";
        pbMain.Size = new Size(1027, 29);
        pbMain.Style = ProgressBarStyle.Marquee;
        pbMain.TabIndex = 5;
        pbMain.Visible = false;
        // 
        // rbAutodetect
        // 
        rbAutodetect.AutoSize = true;
        rbAutodetect.Location = new Point(6, 100);
        rbAutodetect.Name = "rbAutodetect";
        rbAutodetect.Size = new Size(111, 24);
        rbAutodetect.TabIndex = 4;
        rbAutodetect.TabStop = true;
        rbAutodetect.Text = "Autodetect";
        rbAutodetect.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1027, 654);
        Controls.Add(pbMain);
        Controls.Add(tcMain);
        Controls.Add(msMain);
        Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
        KeyPreview = true;
        MainMenuStrip = msMain;
        Name = "Form1";
        Text = "World Cup";
        FormClosing += Form1_FormClosing;
        Load += Form1_Load;
        KeyDown += Form1_KeyDown;
        tcMain.ResumeLayout(false);
        tpSettings.ResumeLayout(false);
        gbRepoType.ResumeLayout(false);
        gbRepoType.PerformLayout();
        gbLangAndReg.ResumeLayout(false);
        gbLangAndReg.PerformLayout();
        gbWCGender.ResumeLayout(false);
        gbWCGender.PerformLayout();
        tpFavs.ResumeLayout(false);
        tpFavs.PerformLayout();
        tpPlayerStats.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvPlayerStats).EndInit();
        tpMatchStats.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvMatchStats).EndInit();
        msMain.ResumeLayout(false);
        msMain.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TabControl tcMain;
    private TabPage tpSettings;
    private Button btnSaveSettings;
    private GroupBox gbLangAndReg;
    private GroupBox gbWCGender;
    private RadioButton rbHr;
    private RadioButton rbEn;
    private RadioButton rbWomen;
    private RadioButton rbMen;
    private TabPage tpFavs;
    private ComboBox cbTeams;
    private TabPage tpPlayerStats;
    private TabPage tpMatchStats;
    private MenuStrip msMain;
    private Button btnSaveFavs;
    private ToolStripMenuItem miPrint;
    private Label lbSelectFavTeam;
    private Label lbFavPlayers;
    private FlowLayoutPanel flpFavPlayers;
    private FlowLayoutPanel flpAllPlayers;
    private DataGridView dgvPlayerStats;
    private DataGridViewImageColumn PlayerImage;
    private DataGridViewTextBoxColumn PlayerName;
    private DataGridViewTextBoxColumn Goals;
    private DataGridViewTextBoxColumn YellowCards;
    private ToolStripMenuItem miControls;
    private DataGridView dgvMatchStats;
    private DataGridViewTextBoxColumn Date;
    private DataGridViewTextBoxColumn HomeTeam;
    private DataGridViewTextBoxColumn AwayTeam;
    private DataGridViewTextBoxColumn Venue;
    private DataGridViewTextBoxColumn Attendance;
    private ProgressBar pbMain;
    private GroupBox gbRepoType;
    private RadioButton rbLocalFiles;
    private RadioButton rbWebApi;
    private RadioButton rbAutodetect;
}
