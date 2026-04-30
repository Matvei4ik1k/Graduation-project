using Graduation_project.AppData;
using Graduation_project.Model;
using System.Windows;
using System.Windows.Controls;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CourseWindow.xaml
    /// </summary>
    public partial class CourseWindow : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();
        List<Course> allCourse;
        public CourseWindow()
        {
            InitializeComponent();
            NicknameTbl.Text = UserSession.UserName;
            allCourse = context.Courses.ToList();
            CourceList.ItemsSource = allCourse;
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

       

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allCourse == null)
                return;

            string text = SearchTb.Text.ToLower();
            var filtered = allCourse.Where(u => !string.IsNullOrWhiteSpace(u.Name) && u.Name.ToLower().Contains(text)).ToList();
            CourceList.ItemsSource = filtered;
        }

        private void CourceBtn_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.CommandParameter is Course course)
            {
                CourseAssignment courseAssignment = new CourseAssignment(course);
                courseAssignment.Show();
                this.Close();
            }
        }
    }
}
