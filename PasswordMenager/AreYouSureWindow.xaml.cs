using PasswordMenager.Services;
using System.Windows;

namespace PasswordMenager;

/// <summary>
/// Logika interakcji dla klasy AreYouSureWindow.xaml
/// </summary>
public partial class AreYouSureWindow : Window
{
    private readonly int _userId;
    private readonly ContentWindow _contentWindow;

    public AreYouSureWindow(int userId, ContentWindow contentWindow)
    {
        InitializeComponent();
        lbl_question.Content = "Are you sure that you want\n to delete?";
        _userId = userId;
        _contentWindow = contentWindow;
    }

    private void btn_yes_Click(object sender, RoutedEventArgs e)
    {
        var loginPage = new MainWindow();
        PasswordsDbContext db = new PasswordsDbContext();
        UserService.Delete(_userId, db);
        _contentWindow.Close();
        this.Close();
        loginPage.Show();
    }

    private void btn_no_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}