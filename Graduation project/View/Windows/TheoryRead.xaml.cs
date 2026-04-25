using Graduation_project.AppData;
using Graduation_project.Model;
using System;
using System.Collections.Generic;
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
        UserBook? userBook;

        int indexChapter;
        int percent;
        bool isLoaded = false;

        public TheoryRead(Book book)
        {
            InitializeComponent();

            // получаем книгу из БД
            currentBook = context.Books.First(b => b.Id == book.Id);

            // загружаем главы
            _chapter = context.Chapters
                .Where(c => c.BookId == currentBook.Id)
                .ToList();

            ChaptersLB.ItemsSource = _chapter;

            // получаем прогресс
            userBook = context.UserBooks
                .FirstOrDefault(x =>
                    x.BookId == currentBook.Id &&
                    x.UserId == UserSession.UserId);

            // индекс
            indexChapter = userBook?.IndexChapter ?? 0;

            // защита от выхода за границы
            if (_chapter.Count > 0)
            {
                indexChapter = Math.Max(0, Math.Min(indexChapter, _chapter.Count - 1));
            }
            else
            {
                indexChapter = 0;
            }

            BookNameTbl.Text = currentBook.Name;

            isLoaded = true;

            // выбираем главу по индексу
            if (_chapter.Count > 0)
                ChaptersLB.SelectedIndex = indexChapter;

            UpdateUI();
        }

        // смена главы
        private void ChaptersLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isLoaded || _chapter.Count == 0)
                return;

            indexChapter = ChaptersLB.SelectedIndex;

            if (indexChapter >= 0 && indexChapter < _chapter.Count)
            {
                ChapterContentTbl.Text = _chapter[indexChapter].ChapterContent;
            }

            UpdateUI();
            SaveProgress();
        }

        // обновление UI
        private void UpdateUI()
        {
            if (_chapter.Count == 0) return;

            percent = (indexChapter + 1) * 100 / _chapter.Count;

            ProgressTbl.Text = $"{percent}%";
            ProgressBarPb.Value = percent;
        }

        // сохранение
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

                ChaptersLB.SelectedIndex = indexChapter;

                UpdateUI();
                SaveProgress();
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