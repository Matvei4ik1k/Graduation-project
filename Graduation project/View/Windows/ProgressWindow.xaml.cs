using Graduation_project.Models;
using System.Windows;
using System.Windows.Controls;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();
        List<Book> _books = new();
        public ProgressWindow()
        {
            InitializeComponent();
            _books = context.Books.ToList();
            BookList.ItemsSource = _books;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void TheoryBtn_Click(object sender, RoutedEventArgs e)
        {
            TheoryWindow theoryWindow = new TheoryWindow();
            theoryWindow.Show();
            this.Close();
        }

        private void CourceBtn_Click(object sender, RoutedEventArgs e)
        {
            CourseWindow courseWindow = new CourseWindow();
            courseWindow.Show();
            this.Close();
        }

        private void SandboxBtn_Click(object sender, RoutedEventArgs e)
        {
            SandboxWindow sandboxWindow = new SandboxWindow();
            sandboxWindow.Show();
            this.Close();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

        private void ReadBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Book selectBook)
            {
                TheoryRead theoryRead = new TheoryRead(selectBook);
                theoryRead.Show();
                this.Close();
            }
            else MessageBox.Show("Ошибка", "Ошибка");
        }

    }
}
