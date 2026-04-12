using Graduation_project.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CourseAssignment.xaml
    /// </summary>
    public partial class CourseAssignment : Window
    {
        GraduationProjectContext _context = new GraduationProjectContext();
        List<Course> allCourses;
        public CourseAssignment()
        {
            InitializeComponent();
            InitializeAsync();
            allCourses = _context.Courses.ToList();
            this.DataContext = allCourses;
            #region Начальный шаблон страницы
            CodeEditor.Text = @"<!DOCTYPE html>
<html>
<body>
        <h1>Header</h1>
        <p>Content goes here...</p>
        Footer
</body>
</html>";
            #endregion
            CodeEditor.TextChanged += CodeEditor_TextChanged;
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
            CourseWindow courseWindow = new CourseWindow();
            courseWindow.Show();
            this.Close();
        }
    }
}
