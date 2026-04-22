using Graduation_project.Models;
using System.Windows;
using System.Windows.Controls;

namespace Graduation_project.View.Windows
{
    public partial class ProgressWindow : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();

        List<Book> _books = new();
        List<Course> _courses = new();

        public ProgressWindow()
        {
            InitializeComponent();

            // загрузка данных
            _books = context.Books.ToList();
            _courses = context.Courses.ToList();

            BookList.ItemsSource = _books;

            // расчёт и отображение прогресса
            UpdateProgress();
        }

        // расчет общего прогресса
        private void UpdateProgress()
        {
            int totalItems = _books.Count + _courses.Count;

            if (totalItems == 0)
            {
                ProgressTbl.Text = "Нет данных для отображения прогресса";
                ProgressPb.Value = 0;
                return;
            }

            int sumProgress =
                _books.Sum(b => b.Progress ?? 0) +
                _courses.Sum(c => c.Progress ?? 0);

            int totalPercent = sumProgress / totalItems;

            // обновление UI
            ProgressTbl.Text = $"Вы продвинулись на {totalPercent}% в изучении веб-разработки";
            ProgressPb.Value = totalPercent;
        }

        // навигация
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void TheoryBtn_Click(object sender, RoutedEventArgs e)
        {
            new TheoryWindow().Show();
            Close();
        }

        private void CourceBtn_Click(object sender, RoutedEventArgs e)
        {
            new CourseWindow().Show();
            Close();
        }

        private void SandboxBtn_Click(object sender, RoutedEventArgs e)
        {
            new SandboxWindow().Show();
            Close();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().Show();
            Close();
        }

        // открыть книгу
        private void ReadBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Book selectBook)
            {
                new TheoryRead(selectBook).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}