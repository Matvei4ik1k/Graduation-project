using Graduation_project.Models;
using System.Windows;

namespace Graduation_project.View.Windows
{
    public partial class CourseAssignment : Window
    {
        GraduationProjectContext _context = new GraduationProjectContext();
        List<Lesson> lessons = new();
        int currentLessonIndex = 0;
        Course currentCurse;

        public CourseAssignment(Course course)
        {
            InitializeComponent();
            currentCurse = course;
            lessons = _context.Lessons.Where(l => l.CourseId == course.CourseId).ToList();
            #region Начальный шаблон страницы
            CodeEditor.Text = @"<!DOCTYPE html>
<html>
    <body>
        <h1>Header</h1>
        <p>Content goes here...</p>
    </body>
</html>";
            #endregion
            ShowLesson();
            InitializeAsync();
            CourseNameTbl.Text = course.Name;
        }

        private void ShowLesson()
        {
            if (lessons == null || lessons.Count == 0)
            {
                MessageBox.Show("Нет уроков");
                return;
            }
            var lesson = lessons[currentLessonIndex];
            var lessonViewModel = new
            {
                LessonTopicText = lesson.LessonTopic,
                TaskText = lesson.TaskDescription,
                InstructionText = lesson.Instructions
            };
            DataContext = lessonViewModel;
        }
        private void NextLessonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentLessonIndex < lessons.Count - 1)
            {
                currentLessonIndex++;
                ShowLesson();
            }
            else
            {
                MessageBox.Show("Курс закончен");
                CourseWindow courseWindow = new();
                courseWindow.Show();
                this.Close();
            }
        }
        private async void InitializeAsync()
        {
            await Preview.EnsureCoreWebView2Async(null);
            Preview.NavigateToString(CodeEditor.Text);
        }
        private void CodeEditor_TextChanged(object? sender, EventArgs e)
        {
            if (Preview.CoreWebView2 != null)
            {
                Preview.NavigateToString(CodeEditor.Text);
            }
        }
        private void BackToCourses_Click(object sender, RoutedEventArgs e)
        {
            CourseWindow courseWindow = new();
            courseWindow.Show();
            this.Close();
        }
    }
}
