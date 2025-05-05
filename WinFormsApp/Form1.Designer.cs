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
        lbWCGender = new Label();
        lbLangAndReg = new Label();
        btnSaveSettings = new Button();
        tpPlayers = new TabPage();
        tcMain.SuspendLayout();
        tpSettings.SuspendLayout();
        SuspendLayout();
        // 
        // tcMain
        // 
        tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tcMain.Controls.Add(tpSettings);
        tcMain.Controls.Add(tpPlayers);
        tcMain.Location = new Point(2, 1);
        tcMain.Name = "tcMain";
        tcMain.SelectedIndex = 0;
        tcMain.Size = new Size(910, 606);
        tcMain.TabIndex = 0;
        // 
        // tpSettings
        // 
        tpSettings.Controls.Add(lbWCGender);
        tpSettings.Controls.Add(lbLangAndReg);
        tpSettings.Controls.Add(btnSaveSettings);
        tpSettings.Location = new Point(4, 29);
        tpSettings.Name = "tpSettings";
        tpSettings.Padding = new Padding(3);
        tpSettings.Size = new Size(902, 573);
        tpSettings.TabIndex = 0;
        tpSettings.Text = "Settings";
        tpSettings.UseVisualStyleBackColor = true;
        // 
        // lbWCGender
        // 
        lbWCGender.AutoSize = true;
        lbWCGender.Location = new Point(39, 123);
        lbWCGender.Name = "lbWCGender";
        lbWCGender.Size = new Size(93, 20);
        lbWCGender.TabIndex = 2;
        lbWCGender.Text = "lbWCGender";
        // 
        // lbLangAndReg
        // 
        lbLangAndReg.AutoSize = true;
        lbLangAndReg.Location = new Point(39, 30);
        lbLangAndReg.Name = "lbLangAndReg";
        lbLangAndReg.Size = new Size(107, 20);
        lbLangAndReg.TabIndex = 1;
        lbLangAndReg.Text = "lbLangAndReg";
        // 
        // btnSaveSettings
        // 
        btnSaveSettings.Location = new Point(383, 509);
        btnSaveSettings.Name = "btnSaveSettings";
        btnSaveSettings.Size = new Size(102, 39);
        btnSaveSettings.TabIndex = 0;
        btnSaveSettings.Text = "Save";
        btnSaveSettings.UseVisualStyleBackColor = true;
        // 
        // tpPlayers
        // 
        tpPlayers.Location = new Point(4, 29);
        tpPlayers.Name = "tpPlayers";
        tpPlayers.Padding = new Padding(3);
        tpPlayers.Size = new Size(902, 573);
        tpPlayers.TabIndex = 1;
        tpPlayers.Text = "Players";
        tpPlayers.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(913, 607);
        Controls.Add(tcMain);
        Name = "Form1";
        Text = "World Cup";
        tcMain.ResumeLayout(false);
        tpSettings.ResumeLayout(false);
        tpSettings.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TabControl tcMain;
    private TabPage tpSettings;
    private TabPage tpPlayers;
    private Label lbLangAndReg;
    private Button btnSaveSettings;
    private Label lbWCGender;
}
