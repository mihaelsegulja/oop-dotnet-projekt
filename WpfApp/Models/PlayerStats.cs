using DAL.Models;
using System.Windows.Controls;

namespace WpfApp.Models;

public class PlayerStats
{
    public StartingEleven Player { get; set; }
    public int Goals { get; set; }
    public int YellowCards { get; set; }
}
