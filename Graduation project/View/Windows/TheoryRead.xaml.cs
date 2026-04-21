using Graduation_project.Models;
using System.Windows;
using System.Windows.Controls;

namespace Graduation_project.View.Windows
{
    public partial class TheoryRead : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();

        List<Chapter> _chapter;
        Book? currentBook;

        int indexChapter;
        int percent;
        bool isLoaded = false;

        public TheoryRead(Book book)
        {
            InitializeComponent();

            // заголовок книги
            BookNameTbl.Text = book.Name;

            // загрузка глав книги
            _chapter = context.Chapters.Where(g => g.BooksId == book.BooksId).ToList();

            ChaptersLB.ItemsSource = _chapter;

            // получаем книгу из БД (для сохранения прогресса)
            currentBook = context.Books
                .FirstOrDefault(b => b.BooksId == book.BooksId);

            // восстановление сохранённого прогресса
            percent = (int)(currentBook?.Progress ?? 0);
            indexChapter = book.IndexChapter ?? 0;

            // защита от выхода за границы
            if (_chapter.Count > 0 && indexChapter >= _chapter.Count)
                indexChapter = _chapter.Count - 1;

            // установка текущей главы
            if (_chapter.Count > 0)
                ChaptersLB.SelectedItem = _chapter[indexChapter];

            isLoaded = true;
        }

        // возврат к списку книг
        private void BackToBooks_Click(object sender, RoutedEventArgs e)
        {
            new TheoryWindow().Show();
            Close();
        }

        // смена выбранной главы
        private void ChaptersLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // защита от срабатывания при инициализации
            if (!isLoaded || _chapter.Count == 0) return;

            // отображение содержимого главы
            if (ChaptersLB.SelectedItem is Chapter selectedChapter)
            {
                ChapterContentTbl.Text = selectedChapter.ChapterContent;
            }

            // обновляем индекс текущей главы
            indexChapter = ChaptersLB.SelectedIndex;

            // пересчёт прогресса в процентах
            percent = ((indexChapter + 1) * 100) / _chapter.Count;

            // обновление UI
            ProgressTbl.Text = $"{percent}%";
            ProgressBarPb.Value = percent;

            // сохранение прогресса в БД
            if (currentBook != null)
            {
                currentBook.Progress = percent;
                currentBook.IndexChapter = indexChapter;

                context.SaveChanges();
            }
        }

        // переход к следующей главе
        private void NextChaptersBtn_Click(object sender, RoutedEventArgs e)
        {
            if (indexChapter < _chapter.Count - 1)
            {
                indexChapter++;
                ChaptersLB.SelectedItem = _chapter[indexChapter];
            }
            else
            {
                new TheoryWindow().Show();
                Close();
            }
        }
    }
}