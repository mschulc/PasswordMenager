using PasswordMenager.Entities;
using PasswordMenager.Services;
using System.Windows;
using System.Windows.Controls;

namespace PasswordMenager.Views;

/// <summary>
/// Logika interakcji dla klasy UserSettings.xaml
/// </summary>
public partial class UserSettings : Page
{
    private readonly ContentWindow _contentWindow;
    private readonly int _userId;
    private readonly string _salt;

    public UserSettings(ContentWindow contentWindow, int userId)
    {
        InitializeComponent();
        _contentWindow = contentWindow;
        _userId = userId;
        PasswordsDbContext db = new PasswordsDbContext();
        var user = UserService.GetById(_userId, db);
        _salt = user.Salt;
        lbl_nonedit_login.Content = user.Login;
        box_edit_email.Text = user.Email;
    }

    private void btn_edit_settings_Click(object sender, RoutedEventArgs e)
    {
        var login = lbl_nonedit_login.Content.ToString();
        var emailToEdit = box_edit_email.Text;
        var passToEdit = box_edit_changePasswod.Password;
        var confirmPassToEdit = box_edit_confirmUserPassword.Password;

        if(passToEdit == null || confirmPassToEdit == null)
        {
            lbl_warrning.Content = "You have to set new password";
        }
        else if (passToEdit != confirmPassToEdit)
        {
            lbl_warrning.Content = "Passwords are diferent";
        }
        else if(passToEdit.Equals(confirmPassToEdit))
        {
            var userToEdit = new User()
            {
                Id = _userId,
                Email = emailToEdit,
                Password = CryptoService.HashPassword(passToEdit, _salt),
                Login = login,
            };
            PasswordsDbContext db = new PasswordsDbContext();
            var ifSuccess = UserService.UpdateUser(userToEdit, _userId, db);
            if(ifSuccess)
            {
                MessageBox.Show("User updated");
                MainContent loginPage = new MainContent(_contentWindow, _userId);
                _contentWindow.Content = loginPage;
            }
            else
            {
                MessageBox.Show("Something gone wrong!");
            }
        }
        else 
        {
            MessageBox.Show("Something gone wrong!");
        }
    }

    private void btn_deleteSettings_Click(object sender, RoutedEventArgs e)
    {
        var areYouSureWindow = new AreYouSureWindow(_userId, _contentWindow);
        areYouSureWindow.Show();  
    }

    private void btn_backSettings_Click(object sender, RoutedEventArgs e)
    {
        MainContent loginPage = new MainContent(_contentWindow, _userId);
        _contentWindow.Content = loginPage;
    }
}
