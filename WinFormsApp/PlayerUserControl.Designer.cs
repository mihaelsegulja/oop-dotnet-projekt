namespace WinFormsApp;

partial class PlayerUserControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        pbPlayerImage = new PictureBox();
        pbFavIcon = new PictureBox();
        lbPlayerInfo = new Label();
        pbDragHandle = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)pbPlayerImage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pbFavIcon).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pbDragHandle).BeginInit();
        SuspendLayout();
        // 
        // pbPlayerImage
        // 
        pbPlayerImage.Location = new Point(3, 3);
        pbPlayerImage.Name = "pbPlayerImage";
        pbPlayerImage.Size = new Size(100, 100);
        pbPlayerImage.TabIndex = 0;
        pbPlayerImage.TabStop = false;
        // 
        // pbFavIcon
        // 
        pbFavIcon.Location = new Point(349, 17);
        pbFavIcon.Name = "pbFavIcon";
        pbFavIcon.Size = new Size(76, 70);
        pbFavIcon.TabIndex = 1;
        pbFavIcon.TabStop = false;
        // 
        // lbPlayerInfo
        // 
        lbPlayerInfo.AutoSize = true;
        lbPlayerInfo.Location = new Point(109, 17);
        lbPlayerInfo.Name = "lbPlayerInfo";
        lbPlayerInfo.Size = new Size(97, 20);
        lbPlayerInfo.TabIndex = 2;
        lbPlayerInfo.Text = "lbPlayerInfo";
        // 
        // pbDragHandle
        // 
        pbDragHandle.Location = new Point(109, 59);
        pbDragHandle.Name = "pbDragHandle";
        pbDragHandle.Size = new Size(234, 49);
        pbDragHandle.TabIndex = 3;
        pbDragHandle.TabStop = false;
        pbDragHandle.MouseDown += PbDragHandle_MouseDown;
        pbDragHandle.MouseMove += PbDragHandle_MouseMove;
        pbDragHandle.MouseUp += PbDragHandle_MouseUp;
        // 
        // PlayerUserControl
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BorderStyle = BorderStyle.Fixed3D;
        Controls.Add(pbDragHandle);
        Controls.Add(pbFavIcon);
        Controls.Add(lbPlayerInfo);
        Controls.Add(pbPlayerImage);
        Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
        Name = "PlayerUserControl";
        Size = new Size(428, 106);
        Click += PlayerUserControl_Click;
        ((System.ComponentModel.ISupportInitialize)pbPlayerImage).EndInit();
        ((System.ComponentModel.ISupportInitialize)pbFavIcon).EndInit();
        ((System.ComponentModel.ISupportInitialize)pbDragHandle).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pbPlayerImage;
    private PictureBox pbFavIcon;
    private Label lbPlayerInfo;
    private PictureBox pbDragHandle;
}
