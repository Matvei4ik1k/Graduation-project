using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class Chapter
{
    public int ChaptersId { get; set; }

    public int? BooksId { get; set; }

    public int? ChapterNumber { get; set; }

    public string? ChapterTitle { get; set; }

    public string? ChapterContent { get; set; }

    public virtual Book? Books { get; set; }
}
