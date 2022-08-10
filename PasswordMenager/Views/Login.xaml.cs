using PasswordMenager.Entities;
using PasswordMenager.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PasswordMenager.Views;

/// <summary>
/// Logika interakcji dla klasy Login.xaml
/// </summary>
public partial class Login : Page
{
    private readonly MainWindow _mainWindow;
    private string login;
    private User potencialUser;

    public Login(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }

    private async void login_check(string login)
    {
        var db = new PasswordsDbContext();
        var ifLoginExist = await Task.Run(() => UserService.FindUserLogin(login, db));
        potencialUser = ifLoginExist;
    }

    private void password_box_TextChanged(object sender, RoutedEventArgs e)
    {
        login = login_box.Text;
        login_check(login);
    }

    private async void btn_login_Click(object sender, RoutedEventArgs e)
    {
        var log = login_box.Text.ToString();
        var pass = password_box.Password.ToString();
        var userId = -1;
        if (potencialUser != null)
        {
            var hashedPass = CryptoService.HashPassword(pass, potencialUser.Salt);

            if (potencialUser.Login == log && potencialUser.Password == hashedPass)
            {
                userId = potencialUser.Id;
            }
            else
            { userId = -1; }
        }

        if (userId >= 0)
        {
            var contentWindow = new ContentWindow(userId);
            contentWindow.Show();
            _mainWindow.Close();
        }
        else
        {
            warning_label.Content = "Wrong login or password";
        }
    }

    private void btn_createaccount_Click(object sender, RoutedEventArgs e)
    {
        CreateAccount createAccountPage = new CreateAccount(_mainWindow);
        _mainWindow.Content = createAccountPage;
    }
}
