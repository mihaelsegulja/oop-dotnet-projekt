namespace WinFormsApp;

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
        tcMain = new TabControl();
        tpSettings = new TabPage();
        gbLangAndReg = new GroupBox();
        rbHr = new RadioButton();
        rbEn = new RadioButton();
        gbWCGender = new GroupBox();
        rbWomen = new RadioButton();
        rbMen = new RadioButton();
        btnSaveSettings = new Button();
        tpPlayers = new TabPage();
        flpFavPlayers = new FlowLayoutPanel();
        flpAllPlayers = new FlowLayoutPanel();
        lbFavPlayers = new Label();
        lbSelectFavTeam = new Label();
        btnSaveFavs = new Button();
        cbTeams = new ComboBox();
        tpPlayerStats = new TabPage();
        tpMatchStats = new TabPage();
        menuStrip1 = new MenuStrip();
        miPrint = new ToolStripMenuItem();
        miHelp = new ToolStripMenuItem();
        tcMain.SuspendLayout();
        tpSettings.SuspendLayout();
        gbLangAndReg.SuspendLayout();
        gbWCGender.SuspendLayout();
        tpPlayers.SuspendLayout();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // tcMain
        // 
        tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tcMain.Controls.Add(tpSettings);
        tcMain.Controls.Add(tpPlayers);
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
        // gbLangAndReg
        // 
        gbLangAndReg.Controls.Add(rbHr);
        gbLangAndReg.Controls.Add(rbEn);
        gbLangAndReg.Location = new Point(203, 126);
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
        gbWCGender.Location = new Point(522, 126);
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
        // tpPlayers
        // 
        tpPlayers.Controls.Add(flpFavPlayers);
        tpPlayers.Controls.Add(flpAllPlayers);
        tpPlayers.Controls.Add(lbFavPlayers);
        tpPlayers.Controls.Add(lbSelectFavTeam);
        tpPlayers.Controls.Add(btnSaveFavs);
        tpPlayers.Controls.Add(cbTeams);
        tpPlayers.Location = new Point(4, 29);
        tpPlayers.Name = "tpPlayers";
        tpPlayers.Padding = new Padding(3);
        tpPlayers.Size = new Size(1015, 590);
        tpPlayers.TabIndex = 2;
        tpPlayers.Text = "Players";
        tpPlayers.UseVisualStyleBackColor = true;
        // 
        // flpFavPlayers
        // 
        flpFavPlayers.Location = new Point(519, 132);
        flpFavPlayers.Name = "flpFavPlayers";
        flpFavPlayers.Size = new Size(347, 348);
        flpFavPlayers.TabIndex = 6;
        // 
        // flpAllPlayers
        // 
        flpAllPlayers.Location = new Point(133, 132);
        flpAllPlayers.Name = "flpAllPlayers";
        flpAllPlayers.Size = new Size(347, 348);
        flpAllPlayers.TabIndex = 5;
        // 
        // lbFavPlayers
        // 
        lbFavPlayers.AutoSize = true;
        lbFavPlayers.Location = new Point(37, 96);
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
        btnSaveFavs.Location = new Point(133, 512);
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
        tpPlayerStats.Location = new Point(4, 29);
        tpPlayerStats.Name = "tpPlayerStats";
        tpPlayerStats.Padding = new Padding(3);
        tpPlayerStats.Size = new Size(1015, 590);
        tpPlayerStats.TabIndex = 3;
        tpPlayerStats.Text = "Player Stats";
        tpPlayerStats.UseVisualStyleBackColor = true;
        // 
        // tpMatchStats
        // 
        tpMatchStats.Location = new Point(4, 29);
        tpMatchStats.Name = "tpMatchStats";
        tpMatchStats.Padding = new Padding(3);
        tpMatchStats.Size = new Size(1015, 590);
        tpMatchStats.TabIndex = 4;
        tpMatchStats.Text = "Match Stats";
        tpMatchStats.UseVisualStyleBackColor = true;
        // 
        // menuStrip1
        // 
        menuStrip1.ImageScalingSize = new Size(20, 20);
        menuStrip1.Items.AddRange(new ToolStripItem[] { miPrint, miHelp });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1027, 28);
        menuStrip1.TabIndex = 1;
        menuStrip1.Text = "msMain";
        // 
        // miPrint
        // 
        miPrint.Name = "miPrint";
        miPrint.Size = new Size(53, 24);
        miPrint.Text = "Print";
        // 
        // miHelp
        // 
        miHelp.Name = "miHelp";
        miHelp.Size = new Size(55, 24);
        miHelp.Text = "Help";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1027, 654);
        Controls.Add(tcMain);
        Controls.Add(menuStrip1);
        Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "World Cup";
        Load += Form1_Load;
        tcMain.ResumeLayout(false);
        tpSettings.ResumeLayout(false);
        gbLangAndReg.ResumeLayout(false);
        gbLangAndReg.PerformLayout();
        gbWCGender.ResumeLayout(false);
        gbWCGender.PerformLayout();
        tpPlayers.ResumeLayout(false);
        tpPlayers.PerformLayout();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
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
    private TabPage tpPlayers;
    private ComboBox cbTeams;
    private TabPage tpPlayerStats;
    private TabPage tpMatchStats;
    private MenuStrip menuStrip1;
    private Button btnSaveFavs;
    private ToolStripMenuItem miPrint;
    private ToolStripMenuItem miHelp;
    private Label lbSelectFavTeam;
    private Label lbFavPlayers;
    private FlowLayoutPanel flpFavPlayers;
    private FlowLayoutPanel flpAllPlayers;
}
