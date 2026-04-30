using Graduation_project.AppData;
using System.Windows;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NicknameTbl.Text = UserSession.UserName;
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

        private void ProgressBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgressWindow progressWindow = new ProgressWindow();
            progressWindow.Show();
            this.Close();
        }

     

        private void StartLearning_Click(object sender, RoutedEventArgs e)
        {
            TheoryWindow theoryWindow = new TheoryWindow();
            theoryWindow.Show();
            this.Close();
        }

        private void AboutTheCourses_Click(object sender, RoutedEventArgs e)
        {
            CourseWindow courseWindow = new CourseWindow();
            courseWindow.Show();
            this.Close();
        }
    }
}
