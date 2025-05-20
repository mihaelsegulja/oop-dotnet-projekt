using System.Data;
using DAL.Models;

namespace WinFormsApp;

public partial class PlayerUserControl : UserControl
{
    public StartingEleven Player { get; private set; }
    public bool IsFavorite { get; set; }
    public bool IsSelected { get; private set; }
    public event EventHandler? SelectionChanged;
    
    private Point _dragStartPoint;
    private bool _dragInitiated = false;

    public PlayerUserControl()
    {
        InitializeComponent();
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
        UpdateFavorite(isFav);
        pbFavIcon.SizeMode = PictureBoxSizeMode.Zoom;
        SetSelected(false);
        pbDragHandle.Image = Image.FromFile("Images/drag-handle.png");
        pbDragHandle.SizeMode = PictureBoxSizeMode.Zoom;
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

    private void PbDragHandle_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _dragStartPoint = e.Location;
            _dragInitiated = true;
        }
    }

    private void PbDragHandle_MouseMove(object? sender, MouseEventArgs e)
    {
        if (_dragInitiated && e.Button == MouseButtons.Left)
        {
            if (Math.Abs(e.X - _dragStartPoint.X) > SystemInformation.DragSize.Width / 2 ||
                Math.Abs(e.Y - _dragStartPoint.Y) > SystemInformation.DragSize.Height / 2)
            {
                _dragInitiated = false;
                var parent = this.Parent as FlowLayoutPanel;
                if (parent != null)
                {
                    var selected = parent.Controls.OfType<PlayerUserControl>()
                        .Where(c => c.IsSelected).ToList();
                    if (!selected.Contains(this))
                        selected = new List<PlayerUserControl> { this };
                    parent.DoDragDrop(selected, DragDropEffects.Move);
                }
            }
        }
    }

    private void PbDragHandle_MouseUp(object? sender, MouseEventArgs e)
    {
        _dragInitiated = false;
    }
}
