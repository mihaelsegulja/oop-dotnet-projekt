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

namespace WinFormsApp
{
    public partial class PlayerUserControl : UserControl
    {
        public StartingEleven Player { get; private set; }

        public PlayerUserControl()
        {
            InitializeComponent();
        }

        public void SetPlayer(StartingEleven player, Image? image = null, bool isFav = false)
        {
            Player = player;
            lbPlayerInfo.Text = $"{player.ShirtNumber}. {player.Name} \n[{player.Position}] {(player.Captain ? " (C)" : "")}";
            lbPlayerInfo.AutoSize = true;
            pbPlayerImage.Image = image ?? Image.FromFile("Images/default-player.png");
            pbPlayerImage.SizeMode = PictureBoxSizeMode.Zoom;
            if (isFav)
            {
                pbFavIcon.Image = Image.FromFile("Images/star-icon.png");
                pbFavIcon.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
