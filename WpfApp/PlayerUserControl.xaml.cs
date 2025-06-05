using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp;

/// <summary>
/// Interaction logic for PlayerUserControl.xaml
/// </summary>
public partial class PlayerUserControl : UserControl
{
    public PlayerUserControl(string name, int number)
    {
        InitializeComponent();
        tbName.Text = name;
        tbNumber.Text = number.ToString();
    }
}
