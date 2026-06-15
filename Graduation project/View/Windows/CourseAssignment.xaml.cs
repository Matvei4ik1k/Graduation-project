using Graduation_project.AppData;
using Graduation_project.Model;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Graduation_project.View.Windows
{
    public partial class CourseAssignment : Window
    {
        private readonly GraduationProjectContext context =
            new GraduationProjectContext();

        private List<Lesson> lessons = new();

        private Course currentCourse;

        private UserCourse? userCourse;

        private int? indexLesson;
        private int? percent;

        private bool isLoaded = false;
        private bool webViewInitialized = false;

        public CourseAssignment(Course course)
        {
            InitializeComponent();

            currentCourse =
                context.Courses
                .First(c => c.Id == course.Id);

            lessons =
                context.Lessons
                .Where(x => x.CourseId == course.Id)
                .ToList();

            userCourse =
                context.UserCourses
                .FirstOrDefault(x =>
                    x.CourseId == course.Id &&
                    x.UserId == UserSession.UserId);

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

            indexLesson = userCourse.IndexLesson;

            if (indexLesson >= lessons.Count)
                indexLesson = lessons.Count - 1;

            CourseNameTbl.Text = currentCourse.Name;

            CodeEditor.Text = @"<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'>
<style>
body{
background:white;
color:black;
padding:20px;
}
</style>
</head>
<body>
<h1>Header</h1>
<p>Content goes here...</p>
</body>
</html>";

            ShowLesson();

            CodeEditor.TextChanged += CodeEditor_TextChanged;

            Loaded += CourseAssignment_Loaded;

            isLoaded = true;
        }

        private async void CourseAssignment_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            await InitializeWebViewAsync();
        }

        private async Task InitializeWebViewAsync()
        {
            if (webViewInitialized)
                return;

            try
            {
                string folder =
                    Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "CodeCraftWebView");

                var env =
                    await CoreWebView2Environment
                    .CreateAsync(
                        null,
                        folder);

                await Preview
                    .EnsureCoreWebView2Async(env);

                Preview.CoreWebView2.NavigateToString(
                    CodeEditor.Text);

                webViewInitialized = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка WebView2");
            }
        }

        private void CodeEditor_TextChanged(
            object? sender,
            EventArgs e)
        {
            if (!webViewInitialized)
                return;

            Preview.CoreWebView2?.NavigateToString(
                CodeEditor.Text);
        }

        private void ShowLesson()
        {
            if (lessons.Count == 0)
            {
                MessageBox.Show("Нет уроков");
                return;
            }

            DataContext =
                lessons[indexLesson ?? 0];

            UpdateProgressUI();
        }

        private void UpdateProgressUI()
        {
            if (lessons.Count == 0)
                return;

            percent =
                ((indexLesson + 1) * 100)
                / lessons.Count;

            ProgressTbl.Text =
                $"{percent}%";

            ProgressBarPb.Value =
                percent ?? 0;
        }

        private void SaveProgress()
        {
            if (!isLoaded)
                return;

            percent =
                ((indexLesson + 1) * 100)
                / lessons.Count;

            userCourse!.IndexLesson =
                indexLesson;

            userCourse.Percent =
                percent;

            context.SaveChanges();
        }

        private void NextLessonBtn_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (indexLesson < lessons.Count - 1)
            {
                indexLesson++;

                ShowLesson();

                SaveProgress();
            }
            else
            {
                MessageBox.Show(
                    "Курс закончен");

                new CourseWindow()
                    .Show();

                Close();
            }
        }

        private void BackToCourses_Click(
            object sender,
            RoutedEventArgs e)
        {
            new CourseWindow()
                .Show();

            Close();
        }
    }
}