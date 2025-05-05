using DAL.Repositories;

namespace WinFormsApp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        LoadDataAsync(); //demo
    }

    private async void LoadDataAsync()
    {
        var repoFactory = new RepositoryFactory();
        var repo = repoFactory.GetRepository();

        // Await the result of GetTeams()
        var teams = await repo.GetTeams();

        // Display the count in the label
        lbLangAndReg.Text = teams.ToList().Count.ToString();
    }
}
