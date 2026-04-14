using Graduation_project.NewModels;
using System.Windows;
using System.Windows.Controls;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для TheoryRead.xaml
    /// </summary>
    public partial class TheoryRead : Window
    {
        
        GraduationProjectContext context = new GraduationProjectContext();
        List<Chapter> _chapter = new();
        public TheoryRead(Book book)
        {
            InitializeComponent();
            BookNameTbl.Text = book.Name;
            _chapter = context.Chapters.Where(g=>g.BooksId == book.BooksId).ToList(); 
            ChaptersLB.ItemsSource = _chapter;
            if (_chapter.Any())
            {
                ChaptersLB.SelectedItem = _chapter?.First();
            }
        }

        private void BackToBooks_Click(object sender, RoutedEventArgs e)
        {
            TheoryWindow theoryWindow = new TheoryWindow();
            theoryWindow.Show();
            this.Close();
        }

        private void ChaptersLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChaptersLB.SelectedItem is Chapter selectedChapter)
            {
                ChapterContentTbl.Text = selectedChapter.ChapterContent;
            }

            
        }
    }
}

