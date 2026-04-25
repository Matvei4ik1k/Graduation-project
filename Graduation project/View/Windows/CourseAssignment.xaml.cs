using Graduation_project.AppData;
using Graduation_project.Model;
using System.Linq;
using System.Windows;

namespace Graduation_project.View.Windows
{
    public partial class CourseAssignment : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();

        List<Lesson> lessons = new();
        Course currentCourse;

        UserCourse? userCourse;

        int? indexLesson;
        int? percent;
        bool isLoaded = false;

        public CourseAssignment(Course course)
        {
            InitializeComponent();

            // загружаем курс
            currentCourse = context.Courses.First(c => c.Id == course.Id);

            // загружаем уроки
            lessons = context.Lessons
                .Where(l => l.CourseId == course.Id)
                .ToList();

            // получаем прогресс пользователя
            userCourse = context.UserCourses
                .FirstOrDefault(x =>
                    x.CourseId == course.Id &&
                    x.UserId == UserSession.UserId);

            // если нет записи прогресса — создаём
            if (userCourse == null)
            {
                userCourse = new UserCourse
                {
                    UserId = UserSession.UserId,
                    CourseId = course.Id,
                    IndexLesson = 0,
                    Percent = 0
                };

                context.UserCourses.Add(userCourse);
                context.SaveChanges();
            }

            // восстанавливаем прогресс
            indexLesson = userCourse.IndexLesson;

            if (indexLesson >= lessons.Count)
                indexLesson = lessons.Count - 1;

            // UI
            CourseNameTbl.Text = currentCourse.Name;

            CodeEditor.Text = @"<!DOCTYPE html>
<html>
    <body>
        <h1>Header</h1>
        <p>Content goes here...</p>
    </body>
</html>";

            ShowLesson();
            InitializeAsync();

            isLoaded = true;
        }

        // Показ урока
        private void ShowLesson()
        {
            if (lessons == null || lessons.Count == 0)
            {
                MessageBox.Show("Нет уроков");
                return;
            }

            DataContext = lessons[indexLesson ?? 0];

            UpdateProgressUI();
        }

        // UI прогресса
        private void UpdateProgressUI()
        {
            if (lessons == null || lessons.Count == 0)
                return;

            percent = ((indexLesson + 1) * 100) / lessons.Count;

            ProgressTbl.Text = $"{percent}%";
            ProgressBarPb.Value = percent ?? 0;
        }

        // Сохранение прогресса
        private void SaveProgress()
        {
            if (!isLoaded || lessons.Count == 0)
                return;

            percent = ((indexLesson + 1) * 100) / lessons.Count;

                userCourse.IndexLesson = indexLesson;
                userCourse.Percent = percent;
            
            context.SaveChanges();
        }

        // Следующий урок
        private void NextLessonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (indexLesson < lessons.Count - 1)
            {
                indexLesson++;
                ShowLesson();
                SaveProgress();
            }
            else
            {
                MessageBox.Show("Курс закончен");

                CourseWindow courseWindow = new CourseWindow();
                courseWindow.Show();
                this.Close();
            }
        }

        // WebView
        private async void InitializeAsync()
        {
            await Preview.EnsureCoreWebView2Async(null);
            Preview.NavigateToString(CodeEditor.Text);
        }

        private void CodeEditor_TextChanged(object sender, EventArgs e)
        {
            if (Preview.CoreWebView2 != null)
                Preview.NavigateToString(CodeEditor.Text);
        }

        // Назад
        private void BackToCourses_Click(object sender, RoutedEventArgs e)
        {
            CourseWindow courseWindow = new CourseWindow();
            courseWindow.Show();
            this.Close();
        }
    }
}