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
    /// Логика взаимодействия для TheoryWindow.xaml
    /// </summary>
    public partial class TheoryWindow : Window
    {
        GraduationProjectContext graduationProjectContext = new GraduationProjectContext();
        List<Book> allBooks;
        public TheoryWindow()
        {
            InitializeComponent();
            allBooks = graduationProjectContext.Books.ToList();
            BookList.ItemsSource = allBooks;
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allBooks == null)
                return;

            string text = SearchTb.Text.ToLower();

            var filtered = allBooks
                .Where(b => !string.IsNullOrEmpty(b.Name) &&
                            b.Name.ToLower().Contains(text))
                .ToList();

            BookList.ItemsSource = filtered;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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

        }

        private void ProgressBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
