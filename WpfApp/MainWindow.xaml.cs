using System.Windows;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Settings_Click(object sender, RoutedEventArgs e)
    {
        new AppSettingsWindow().ShowDialog();
    }

    private void ButtonInfoHomeTeam_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ButtonInfoAwayTeam_Click(object sender, RoutedEventArgs e)
    {

    }
}