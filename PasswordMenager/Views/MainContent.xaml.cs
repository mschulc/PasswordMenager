using PasswordMenager.Entities;
using PasswordMenager.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PasswordMenager.Views;

/// <summary>
/// Logika interakcji dla klasy MainContent.xaml
/// </summary>
public partial class MainContent : Page
{ 
    private readonly ContentWindow _contentWindow;
    private readonly int _userId;
    private int _selectedId = -1;
    private int currentFont = 1;

    public MainContent(ContentWindow contentWindow, int userId)
    {
        InitializeComponent();
        combo_searchoption.ItemsSource = new string[] { "Name", "Type", "Login" };
        _contentWindow = contentWindow;
        _userId = userId;
        var db = new PasswordsDbContext();
        lbl_info.Content = "Hello " + UserService.GetById(userId, db).Login.ToString();
        var passwords = PasswordsService.GetAllPasswords(db, _userId);
        dataGrid_passwords.ItemsSource = passwords;
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Passwords data = (Passwords)dataGrid_passwords.SelectedItem;
        if (data == null)
        {
            _selectedId = -1;
        }
        else
            _selectedId = data.Id;
    }

    private void btn_search_Click(object sender, RoutedEventArgs e)
    {
        PasswordsDbContext db = new PasswordsDbContext();
        var item = combo_searchoption.SelectedItem;


        if (item != null)
        {
            if (item == "Name")
            {
                var searchName = box_search.Text;
                if (searchName.Length > 0)
                {
                    dataGrid_passwords.ItemsSource = PasswordsService
                        .GetPasswordsByName(db, _userId, searchName);
                }
                else
                {
                    dataGrid_passwords.ItemsSource = PasswordsService
                        .GetAllPasswords(db, _userId);
                }
            }
            else if (item == "Login")
            {
                var searchLogin = box_search.Text;
                if (searchLogin.Length > 0)
                {
                    dataGrid_passwords.ItemsSource = PasswordsService
                        .GetPasswordsByLogin(db, _userId, searchLogin);
                }
                else
                {
                    dataGrid_passwords.ItemsSource = PasswordsService
                        .GetAllPasswords(db, _userId);
                }
            }
            else if (item == "Type")
            {
                var searchType = box_search.Text;
                if (searchType.Length > 0)
                {
                    dataGrid_passwords.ItemsSource = PasswordsService
                        .GetPasswordsByType(db, _userId, searchType);
                }
                else
                {
                    dataGrid_passwords.ItemsSource = PasswordsService
                        .GetAllPasswords(db, _userId);
                }
            }
        }
    }

    private void btn_add_Click(object sender, RoutedEventArgs e)
    {
        AddNewPassword addNewPasswordPage = new AddNewPassword(_contentWindow, _userId);
        _contentWindow.Content = addNewPasswordPage;
    }

    private void btn_edit_Click(object sender, RoutedEventArgs e)
    {
        EditPassword editPasswordPage = new EditPassword(_contentWindow, _userId, _selectedId);
        _contentWindow.Content = editPasswordPage;
    }

    private void btn_logout_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        _contentWindow.Close();
    }

    private void btn_delete_Click(object sender, RoutedEventArgs e)
    {
        bool succes = false;
        var id = _selectedId;
        dataGrid_passwords.UnselectAllCells();
        PasswordsDbContext db = new PasswordsDbContext();
        if (id> 0)
        {
            succes = PasswordsService.Delete(db, id);
            dataGrid_passwords.ItemsSource = PasswordsService
                        .GetAllPasswords(db, _userId);
            MessageBox.Show("Deleted!");
        }
        else
            MessageBox.Show("Something gone wrong");
    }

    private void btn_settings_Click(object sender, RoutedEventArgs e)
    {
        UserSettings userSettingsPage = new UserSettings(_contentWindow, _userId);
        _contentWindow.Content = userSettingsPage;
    }

    private void przkladowy_Click(object sender, RoutedEventArgs e)
    {
        if(currentFont == 1)
        {
            PasswordColumn.FontFamily = new FontFamily("Segoe UI");
            PasswordColumn.FontSize = 12;
            currentFont = 0;
        }
        else
        {
            PasswordColumn.FontFamily = new FontFamily("Password Dots");
            PasswordColumn.FontSize = 10;
            currentFont = 1;
        }
    }
}