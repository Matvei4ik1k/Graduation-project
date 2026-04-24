using Graduation_project.AppData;
using Graduation_project.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Graduation_project.View.Windows
{
    public partial class TheoryRead : Window
    {
        GraduationProjectContext context = new GraduationProjectContext();

        List<Chapter> _chapter;
        Book currentBook;
        UserBook userBook;

        int indexChapter;
        int percent;
        bool isLoaded = false;

        public TheoryRead(Book book)
        {
            InitializeComponent();

            // книга
            currentBook = context.Books.First(b => b.Id == book.Id);

            // главы книги
            _chapter = context.Chapters
                .Where(c => c.BookId == book.Id)
                .ToList();

            ChaptersLB.ItemsSource = _chapter;

            // получаем прогресс текущего пользователя
            userBook = context.UserBooks
                .FirstOrDefault(x =>
                    x.BookId == book.Id &&
                    x.UserId == UserSession.UserId);

            // если нет записи — стартуем с 0
            indexChapter = userBook?.IndexChapter ?? 0;

            if (_chapter.Count > 0 && indexChapter >= _chapter.Count)
                indexChapter = 0;

            BookNameTbl.Text = book.Name;

            if (_chapter.Count > 0)
                ChaptersLB.SelectedItem = _chapter[indexChapter];

            isLoaded = true;

            UpdateUI();
        }

        // смена главы
        private void ChaptersLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isLoaded || _chapter.Count == 0)
                return;

            if (ChaptersLB.SelectedItem is Chapter selected)
            {
                ChapterContentTbl.Text = selected.ChapterContent;
            }

            indexChapter = ChaptersLB.SelectedIndex;

            UpdateUI();
            SaveProgress();
        }

        // UI обновление
        private void UpdateUI()
        {
            if (_chapter.Count == 0) return;

            percent = ((_chapter.Count == 0) ? 0 : ((indexChapter + 1) * 100 / _chapter.Count));

            ProgressTbl.Text = $"{percent}%";
            ProgressBarPb.Value = percent;
        }

        // сохранение прогресса
        private void SaveProgress()
        {
            if (_chapter.Count == 0) return;

            if (userBook != null)
            {
                userBook.IndexChapter = indexChapter;
                userBook.Percent = percent;
            }
            else
            {
                userBook = new UserBook
                {
                    UserId = UserSession.UserId,
                    BookId = currentBook.Id,
                    IndexChapter = indexChapter,
                    Percent = percent
                };

                context.UserBooks.Add(userBook);
            }

            context.SaveChanges();
        }

        // следующая глава
        private void NextChaptersBtn_Click(object sender, RoutedEventArgs e)
        {
            if (indexChapter < _chapter.Count - 1)
            {
                indexChapter++;
                ChaptersLB.SelectedItem = _chapter[indexChapter];
            }
            else
            {
                MessageBox.Show("Книга пройдена");
                new TheoryWindow().Show();
                Close();
            }
        }

        // назад
        private void BackToBooks_Click(object sender, RoutedEventArgs e)
        {
            new TheoryWindow().Show();
            Close();
        }
    }
}