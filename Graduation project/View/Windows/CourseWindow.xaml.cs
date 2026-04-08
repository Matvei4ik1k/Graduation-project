using Graduation_project.Models;
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
    /// Логика взаимодействия для CourseWindow.xaml
    /// </summary>
    public partial class CourseWindow : Window
    {
      GraduationProjectContext context = new GraduationProjectContext();
        List<Cource> cources;
        public CourseWindow()
        {
            InitializeComponent();
            cources = context.Cources.ToList();
            CourceList.ItemsSource = cources;
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

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }
    }
}
