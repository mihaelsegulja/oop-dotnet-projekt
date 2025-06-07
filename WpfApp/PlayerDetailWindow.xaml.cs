using DAL.Models;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfApp.Models;

namespace WpfApp;

/// <summary>
/// Interaction logic for PlayerDetailWindow.xaml
/// </summary>
public partial class PlayerDetailWindow : Window
{
    private PlayerStats _playerStats;

    public PlayerDetailWindow(PlayerStats playerStats)
    {
        InitializeComponent();
        _playerStats = playerStats;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        string playerImgPath = $"Images/{_playerStats.Player.Name.ToLower().Replace(' ', '-')}.jpg";
        var imagePath = File.Exists(playerImgPath) ? playerImgPath : "Images/default-player.png";
        var uri = new Uri(imagePath, UriKind.Relative);
        imgPlayer.Source = new BitmapImage(uri);

        tbPlayerName.Text = $"{_playerStats.Player.Name}";
        tbShirtNum.Text = $"Shirt Number: {_playerStats.Player.ShirtNumber}";
        tbPosition.Text = $"Position: {_playerStats.Player.Position}";
        tbIsCaptain.Text = $"Captain: {(_playerStats.Player.Captain ? "Yes" : "No")}";
        tbGoals.Text = $"Goals: {_playerStats.Goals}";
        tbYellowCards.Text = $"Yellow Cards: {_playerStats.YellowCards}";
    }
}
