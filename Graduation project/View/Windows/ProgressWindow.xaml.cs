using Graduation_project.AppData;
using Graduation_project.Model;
using System.Windows;

namespace Graduation_project.View.Windows
{
    public partial class ProgressWindow : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();

        List<BookProgressDto> _books = new();
        List<UserCourse> _userCourses = new();

        public ProgressWindow()
        {
            InitializeComponent();

            int userId = UserSession.UserId;

            var userBooks = context.UserBooks
                .Where(x => x.UserId == userId)
                .ToList();

            _books = userBooks.Select(ub => new BookProgressDto
            {
                Book = context.Books.First(b => b.Id == ub.BookId),
                Percent = ub.Percent ?? 0
            }).ToList();

            _userCourses = context.UserCourses
                .Where(x => x.UserId == userId)
                .ToList();

            BookList.ItemsSource = _books;

            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int totalItems = context.Books.Count() + context.Courses.Count();

            if (totalItems == 0)
            {
                ProgressTbl.Text = "Нет данных";
                ProgressPb.Value = 0;
                return;
            }

            int sum =
                _books.Sum(x => x.Percent) +
                _userCourses.Sum(x => x.Percent ?? 0);

            int totalPercent = sum / totalItems;

            ProgressTbl.Text = $"Прогресс: {totalPercent}%";
            ProgressPb.Value = totalPercent;
        }

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

        private void ReadBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement el && el.DataContext is BookProgressDto dto)
            {
                new TheoryRead(dto.Book).Show();
                Close();
            }
        }
    }
}