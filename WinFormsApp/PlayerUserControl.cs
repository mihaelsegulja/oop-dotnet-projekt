using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Models;

namespace WinFormsApp;

public partial class PlayerUserControl : UserControl
{
    public StartingEleven Player { get; private set; }
    public bool IsFavorite { get; set; }
    public bool IsSelected { get; private set; }
    public event EventHandler? SelectionChanged;
    
    private Point _dragStartPoint;

    public PlayerUserControl()
    {
        InitializeComponent();
        this.Click += PlayerUserControl_Click;
        foreach (Control c in this.Controls)
            c.Click += PlayerUserControl_Click;
    }

    public void SetPlayer(StartingEleven player, Image? image = null, bool isFav = false)
    {
        Player = player;
        lbPlayerInfo.Text = $"{player.ShirtNumber}. {player.Name} \n[{player.Position}] {(player.Captain ? " (C)" : "")}";
        lbPlayerInfo.AutoSize = true;
        pbPlayerImage.Image = image ?? Image.FromFile("Images/default-player.png");
        pbPlayerImage.SizeMode = PictureBoxSizeMode.Zoom;
        IsFavorite = isFav;
        pbFavIcon.Image = isFav ? Image.FromFile("Images/star-icon.png") : null;
        pbFavIcon.SizeMode = PictureBoxSizeMode.Zoom;
        SetSelected(false);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        this.BackColor = selected ? Color.LightSkyBlue : Color.Transparent;
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateFavorite(bool isFav)
    {
        IsFavorite = isFav;
        pbFavIcon.Image = isFav ? Image.FromFile("Images/star-icon.png") : null;
    }

    private void PlayerUserControl_Click(object? sender, EventArgs e)
    {
        var parent = this.Parent as Control;
        var form = this.FindForm() as Form1;
        if (form == null) return;

        if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
        {
            SetSelected(!IsSelected);
        }
        else
        {
            form.ClearAllPlayerSelections();
            SetSelected(true);
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
            _dragStartPoint = e.Location;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (e.Button == MouseButtons.Left)
        {
            if (Math.Abs(e.X - _dragStartPoint.X) > SystemInformation.DragSize.Width / 2 ||
                Math.Abs(e.Y - _dragStartPoint.Y) > SystemInformation.DragSize.Height / 2)
            {
                var form = this.FindForm() as Form1;
                if (form == null) return;

                // Get all selected controls, or just this one if not selected
                var selected = form.GetSelectedPlayerControls();
                if (!selected.Contains(this))
                    selected = new List<PlayerUserControl> { this };

                this.DoDragDrop(selected, DragDropEffects.Move);
            }
        }
    }

}
