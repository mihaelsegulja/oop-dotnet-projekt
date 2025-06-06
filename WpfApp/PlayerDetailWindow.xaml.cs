using DAL.Models;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp;

/// <summary>
/// Interaction logic for PlayerDetailWindow.xaml
/// </summary>
public partial class PlayerDetailWindow : Window
{
    private StartingEleven _player;

    public PlayerDetailWindow(StartingEleven player)
    {
        InitializeComponent();
        _player = player;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        tbPlayerName.Text = $"{_player.Name}";
        tbShirtNum.Text = $"Shirt Number: {_player.ShirtNumber}";
        tbPosition.Text = $"Position: {_player.Position}";
        tbIsCaptain.Text = $"Captain: {(_player.Captain ? "Yes" : "No")}";

        string playerImgPath = $"Images/{_player.Name.ToLower().Replace(' ', '-')}.jpg";
        var imagePath = Path.Exists(playerImgPath) ? playerImgPath : "Images/default-player.png";
        var uri = new Uri(imagePath, UriKind.Relative);
        imgPlayer.Source = new BitmapImage(uri);
    }
}
