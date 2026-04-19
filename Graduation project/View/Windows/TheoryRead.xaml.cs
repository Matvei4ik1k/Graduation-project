using Graduation_project.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для TheoryRead.xaml
    /// </summary>
    public partial class TheoryRead : Window
    {

        GraduationProjectContext context = new GraduationProjectContext();
        List<Chapter> _chapter;
        Book? currentBook = new Book();
        int indexChapter;
        int percent;
        bool isLoaded = false;
        public TheoryRead(Book book)
        {
            InitializeComponent();
            BookNameTbl.Text = book.Name;
            _chapter = context.Chapters.Where(g => g.BooksId == book.BooksId).ToList();
            ChaptersLB.ItemsSource = _chapter;
            currentBook = context.Books.FirstOrDefault(b => b.BooksId == book.BooksId);
            percent = (int)(currentBook?.Progress ?? 0);
            indexChapter = book.IndexChapter ?? 0;
            if (_chapter.Any()) ChaptersLB.SelectedItem = _chapter[indexChapter];
            isLoaded = true;
        }

        private void BackToBooks_Click(object sender, RoutedEventArgs e)
        {
            TheoryWindow theoryWindow = new TheoryWindow();
            theoryWindow.Show();
            this.Close();
        }

        private void ChaptersLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isLoaded) return;
            if(_chapter.Count == 0) return;
            if (ChaptersLB.SelectedItem is Chapter selectedChapter)
            {
                ChapterContentTbl.Text = selectedChapter.ChapterContent;
            }
            indexChapter = ChaptersLB.SelectedIndex;
            percent = ((indexChapter + 1) * 100) / _chapter.Count;
            ProgressTbl.Text = $"{percent}%";
            ProgressBarPb.Value = percent;
            if (currentBook != null)
            {
                currentBook.Progress = percent;
                currentBook.IndexChapter = indexChapter;
                context.SaveChanges();
            }
        }

        private void NextChaptersBtn_Click(object sender, RoutedEventArgs e)
        {
            if (indexChapter < _chapter.Count - 1)
            {
                indexChapter++;
                ChaptersLB.SelectedItem = _chapter[indexChapter];
            }
            else
            {
                TheoryWindow theoryWindow = new TheoryWindow();
                theoryWindow.Show();
                this.Close();
            }
        }
    }
}

