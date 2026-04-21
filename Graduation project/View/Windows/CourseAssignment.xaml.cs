using Graduation_project.Models;
using System.Windows;

namespace Graduation_project.View.Windows
{
    public partial class CourseAssignment : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();

        List<Lesson> lessons = new();
        Course currentCourse;

        int indexLesson;
        int percent;
        bool isLoaded = false;

        public CourseAssignment(Course course)
        {
            InitializeComponent();

            // Загружаем курс из БД (ВАЖНО)
            currentCourse = context.Courses.First(c => c.CourseId == course.CourseId);

            // Загружаем уроки
            lessons = context.Lessons.Where(l => l.CourseId == course.CourseId).ToList();

            // Восстанавливаем прогресс
            indexLesson = currentCourse.IndexLesson ?? 0;

            if (indexLesson >= lessons.Count)
                indexLesson = lessons.Count - 1;

            // Название курса
            CourseNameTbl.Text = currentCourse.Name;

            // Стартовый HTML
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

            DataContext = lessons[indexLesson];

            UpdateProgressUI(); // обновляем UI
        }

        // Обновление UI прогресса
        private void UpdateProgressUI()
        {
            if (lessons == null || lessons.Count == 0)
                return;

            percent = ((indexLesson + 1) * 100) / lessons.Count;

            ProgressTbl.Text = $"{percent}%";
            ProgressBarPb.Value = percent;
        }

        // Сохранение прогресса в БД
        private void SaveProgress()
        {
            if (!isLoaded || lessons.Count == 0)
                return;

            percent = ((indexLesson + 1) * 100) / lessons.Count;

            currentCourse.IndexLesson = indexLesson;
            currentCourse.Progress = percent;

            context.SaveChanges();
        }

        // Следующий урок
        private void NextLessonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (indexLesson < lessons.Count - 1)
            {
                indexLesson++;
                ShowLesson();     // обновляет UI
                SaveProgress();   // сохраняет в БД
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
            CourseWindow courseWindow = new();
            courseWindow.Show();
            this.Close();
        }
    }
}