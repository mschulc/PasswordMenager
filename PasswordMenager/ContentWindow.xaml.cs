using PasswordMenager.Views;
using System.Windows;

namespace PasswordMenager;

/// <summary>
/// Logika interakcji dla klasy ContentWindow.xaml
/// </summary>
public partial class ContentWindow : Window
{ 
    public ContentWindow(int userId)
    {
        InitializeComponent();
        MainContent contentPage = new MainContent(this, userId);
        this.Content = contentPage;
    }
}