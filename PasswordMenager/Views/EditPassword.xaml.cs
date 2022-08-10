using PasswordMenager.Entities;
using PasswordMenager.Enums;
using PasswordMenager.Resources;
using PasswordMenager.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PasswordMenager.Views;

/// <summary>
/// Logika interakcji dla klasy EditPassword.xaml
/// </summary>
public partial class EditPassword : Page
{
    private readonly ContentWindow _contentWindow;
    private readonly int _userId;
    private readonly int _passId;
    private readonly string _publicKey;

    public EditPassword(ContentWindow contentWindow, int userId, int passId)
    {
        InitializeComponent();
        _contentWindow = contentWindow;
        _userId = userId;
        _passId = passId;
        _publicKey = Resource.PublicKey;
        PasswordsDbContext db = new PasswordsDbContext();
        var passToEdit = PasswordsService.GetById(db, passId);
        box_edit_type.ItemsSource = new List<string> 
        { "Family", "Work", "Private", "Enterteiment", "Games", "Other" };
        box_edit_login.Text = passToEdit.Login;
        box_edit_password.Text = CryptoService.Decrypt(passToEdit.Password, Resource.PrivateKey);
        box_edit_type.Text = passToEdit.Type.ToString();
        box_edit_name.Text = passToEdit.Name;
    }

    private void btn_back_Click(object sender, RoutedEventArgs e)
    {
        MainContent loginPage = new MainContent(_contentWindow, _userId);
        _contentWindow.Content = loginPage;
    }
    private void btn_edit_Click(object sender, RoutedEventArgs e)
    {
        var editedPass = new Passwords
        {
            Name = box_edit_name.Text,
            Login = box_edit_login.Text,
            Password = CryptoService.Encrypt(box_edit_password.Text, _publicKey),
            Type = (Types)PasswordsService.CheckTheType(box_edit_type.Text)
        };
        PasswordsDbContext db = new PasswordsDbContext();
        PasswordsService.Update(_passId, db, editedPass);
        MessageBox.Show("Edited!");
        MainContent loginPage = new MainContent(_contentWindow, _userId);
        _contentWindow.Content = loginPage;
    }
}