using PasswordMenager.Services;
using PasswordMenager.Views;
using System.Windows;

namespace PasswordMenager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PasswordsDbContext dbContext = new PasswordsDbContext();
            dbContext.Create();
            Login loginPage = new Login(this);
            this.Content = loginPage;
        }
    }
}