using DAL.Repositories;

namespace WinFormsApp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        var repoFactory = new RepositoryFactory();
        var appSettings = repoFactory.GetAppSettings();

    }
}
