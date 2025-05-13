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
        gbWCGender = new GroupBox();
        btnSaveSettings = new Button();
        tpPlayers = new TabPage();
        tpTeams = new TabPage();
        radioButton1 = new RadioButton();
        radioButton2 = new RadioButton();
        radioButton3 = new RadioButton();
        radioButton4 = new RadioButton();
        tcMain.SuspendLayout();
        tpSettings.SuspendLayout();
        gbLangAndReg.SuspendLayout();
        gbWCGender.SuspendLayout();
        SuspendLayout();
        // 
        // tcMain
        // 
        tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tcMain.Controls.Add(tpSettings);
        tcMain.Controls.Add(tpPlayers);
        tcMain.Controls.Add(tpTeams);
        tcMain.Location = new Point(2, 1);
        tcMain.Name = "tcMain";
        tcMain.SelectedIndex = 0;
        tcMain.Size = new Size(1024, 653);
        tcMain.TabIndex = 0;
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
        tpSettings.Size = new Size(1016, 620);
        tpSettings.TabIndex = 0;
        tpSettings.Text = "Settings";
        // 
        // gbLangAndReg
        // 
        gbLangAndReg.Controls.Add(radioButton2);
        gbLangAndReg.Controls.Add(radioButton1);
        gbLangAndReg.Location = new Point(44, 53);
        gbLangAndReg.Name = "gbLangAndReg";
        gbLangAndReg.Size = new Size(243, 113);
        gbLangAndReg.TabIndex = 3;
        gbLangAndReg.TabStop = false;
        gbLangAndReg.Text = "Language and region";
        // 
        // gbWCGender
        // 
        gbWCGender.Controls.Add(radioButton4);
        gbWCGender.Controls.Add(radioButton3);
        gbWCGender.Location = new Point(384, 53);
        gbWCGender.Name = "gbWCGender";
        gbWCGender.Size = new Size(242, 113);
        gbWCGender.TabIndex = 4;
        gbWCGender.TabStop = false;
        gbWCGender.Text = "World Cup gender";
        // 
        // btnSaveSettings
        // 
        btnSaveSettings.Location = new Point(433, 536);
        btnSaveSettings.Name = "btnSaveSettings";
        btnSaveSettings.Size = new Size(115, 39);
        btnSaveSettings.TabIndex = 0;
        btnSaveSettings.Text = "Save";
        btnSaveSettings.UseVisualStyleBackColor = true;
        // 
        // tpPlayers
        // 
        tpPlayers.BackColor = Color.WhiteSmoke;
        tpPlayers.Location = new Point(4, 29);
        tpPlayers.Name = "tpPlayers";
        tpPlayers.Padding = new Padding(3);
        tpPlayers.Size = new Size(1016, 620);
        tpPlayers.TabIndex = 1;
        tpPlayers.Text = "Players";
        // 
        // tpTeams
        // 
        tpTeams.BackColor = Color.WhiteSmoke;
        tpTeams.Location = new Point(4, 29);
        tpTeams.Name = "tpTeams";
        tpTeams.Padding = new Padding(3);
        tpTeams.Size = new Size(1016, 620);
        tpTeams.TabIndex = 2;
        tpTeams.Text = "Teams";
        // 
        // radioButton1
        // 
        radioButton1.AutoSize = true;
        radioButton1.Location = new Point(6, 40);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new Size(120, 24);
        radioButton1.TabIndex = 0;
        radioButton1.TabStop = true;
        radioButton1.Text = "radioButton1";
        radioButton1.UseVisualStyleBackColor = true;
        // 
        // radioButton2
        // 
        radioButton2.AutoSize = true;
        radioButton2.Location = new Point(6, 70);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new Size(122, 24);
        radioButton2.TabIndex = 1;
        radioButton2.TabStop = true;
        radioButton2.Text = "radioButton2";
        radioButton2.UseVisualStyleBackColor = true;
        // 
        // radioButton3
        // 
        radioButton3.AutoSize = true;
        radioButton3.Location = new Point(6, 40);
        radioButton3.Name = "radioButton3";
        radioButton3.Size = new Size(122, 24);
        radioButton3.TabIndex = 2;
        radioButton3.TabStop = true;
        radioButton3.Text = "radioButton3";
        radioButton3.UseVisualStyleBackColor = true;
        // 
        // radioButton4
        // 
        radioButton4.AutoSize = true;
        radioButton4.Location = new Point(6, 70);
        radioButton4.Name = "radioButton4";
        radioButton4.Size = new Size(122, 24);
        radioButton4.TabIndex = 3;
        radioButton4.TabStop = true;
        radioButton4.Text = "radioButton4";
        radioButton4.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1027, 654);
        Controls.Add(tcMain);
        Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
        Name = "Form1";
        Text = "World Cup";
        tcMain.ResumeLayout(false);
        tpSettings.ResumeLayout(false);
        gbLangAndReg.ResumeLayout(false);
        gbLangAndReg.PerformLayout();
        gbWCGender.ResumeLayout(false);
        gbWCGender.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TabControl tcMain;
    private TabPage tpSettings;
    private TabPage tpPlayers;
    private Button btnSaveSettings;
    private TabPage tpTeams;
    private GroupBox gbLangAndReg;
    private GroupBox gbWCGender;
    private RadioButton radioButton2;
    private RadioButton radioButton1;
    private RadioButton radioButton4;
    private RadioButton radioButton3;
}
