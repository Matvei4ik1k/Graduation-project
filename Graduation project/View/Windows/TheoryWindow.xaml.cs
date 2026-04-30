using Graduation_project.AppData;
using Graduation_project.Model;
using System.Windows;
using System.Windows.Controls;

namespace Graduation_project.View.Windows
{

    public partial class TheoryWindow : Window
    {
        GraduationProjectContext graduationProjectContext = new GraduationProjectContext();
        List<Book> allBooks;

        public TheoryWindow()
        {
            InitializeComponent();
            NicknameTbl.Text = UserSession.UserName;
            allBooks = graduationProjectContext.Books.ToList();
            BookList.ItemsSource = allBooks;
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allBooks == null)
                return;
            string text = SearchTb.Text.ToLower();
            var filtered = allBooks.Where(b => !string.IsNullOrEmpty(b.Name) && b.Name.ToLower().Contains(text)).ToList();
            BookList.ItemsSource = filtered;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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

        private void ProgressBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgressWindow progressWindow = new ProgressWindow();
            progressWindow.Show();
            this.Close();
        }

       

        private void ReadBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Book book)
            {
                TheoryRead theoryRead = new TheoryRead(book);
                theoryRead.Show();
                this.Close();
            }

        }
    }
}
