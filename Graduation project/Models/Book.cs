using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class Book
{
    public int BooksId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Progress { get; set; }
    public int? IndexChapter { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}
