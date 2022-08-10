using PasswordMenager.Entities;
using PasswordMenager.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PasswordMenager.Views;

/// <summary>
/// Logika interakcji dla klasy CreateAccount.xaml
/// </summary>
public partial class CreateAccount : Page
{
    private readonly MainWindow _mainWindow;

    public CreateAccount(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }

    private void btn_back_Click(object sender, RoutedEventArgs e)
    {
        Login loginPage = new Login(_mainWindow);
        _mainWindow.Content = loginPage;
    }

    private void btn_create_Click(object sender, RoutedEventArgs e)
    {
        var login = box_create_login.Text;
        var email = box_create_email.Text;
        var password = passbox_create_password.Password;
        var confirmPassword = passbox_create_confirmPass.Password;
        PasswordsDbContext db = new PasswordsDbContext();

        if (password == confirmPassword)
        {
            var salt = CryptoService.GenerateSalt();
            var hashedPassword = CryptoService.HashPassword(password, salt);
            UserService.Add(new User 
            { 
                Email = email,
                Login = login,
                Salt = salt,
                Password = hashedPassword
            }, 
            db);
            MessageBox.Show("Account created!");
            Login loginPage = new Login(_mainWindow);
            _mainWindow.Content = loginPage;
        }
        else
        {
            lbl_infobox.Content = "Passwords are not the same";
        }
    }

    private async void box_create_login_TextChanged(object sender, TextChangedEventArgs e)
    {

        var db = new PasswordsDbContext();
        var login = box_create_login.Text;
        var ifLoginExist = await Task.Run(() => UserService.FindUser(login, db));

        if(!ifLoginExist)
        {
            lbl_infobox.Content = "This username already exist";
        }
        else
        {
            lbl_infobox.Content = "";
        }
    }
}
