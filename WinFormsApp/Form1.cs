using DAL.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var repoFactory = new RepositoryFactory();
            var repo = repoFactory.GetRepository();
        }
    }
}
