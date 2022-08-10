using PasswordMenager.Entities;
using PasswordMenager.Enums;
using PasswordMenager.Resources;
using PasswordMenager.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PasswordMenager.Views;

/// <summary>
/// Logika interakcji dla klasy AddNewPassword.xaml
/// </summary>
public partial class AddNewPassword : Page
{
    private readonly ContentWindow _contentWindow;
    private readonly int _userId;

    public AddNewPassword(ContentWindow contentWindow, int userId)
    {
        InitializeComponent();
        _contentWindow = contentWindow;
        _userId = userId;
        box_new_type.ItemsSource = new List<string> {"Family", "Work", "Private", "Enterteiment", "Games", "Other" };
    }

    private void btn_back_Click(object sender, RoutedEventArgs e)
    {
        MainContent loginPage = new MainContent(_contentWindow, _userId);
        _contentWindow.Content = loginPage;
    }

    private void btn_add_Click(object sender, RoutedEventArgs e)
    {
        var login = box_new_login.Text;
        var password = CryptoService.Encrypt(box_new_password.Text, Resource.PublicKey);
        var name = box_new_name.Text;
        var type = box_new_type.Text;

        PasswordsDbContext db = new PasswordsDbContext();

        Passwords newPass = new Passwords
        {
            Name = name,
            Password = password,
            Login = login,
            UserId = _userId,
            Type = (Types)PasswordsService.CheckTheType(type)
        };
        PasswordsService.AddPssword(newPass, db);
        MainContent loginPage = new MainContent(_contentWindow, _userId);
        _contentWindow.Content = loginPage;
    }
}