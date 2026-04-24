using System;
using System.Collections.Generic;

namespace Graduation_project.Model;

public partial class Chapter
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? ChapterNumber { get; set; }

    public string? ChapterTitle { get; set; }

    public string? ChapterContent { get; set; }

    public virtual Book? Book { get; set; }
}
