using System;
using System.Collections.Generic;

namespace Graduation_project.Model;

public partial class UserBook
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? BookId { get; set; }

    public int? IndexChapter { get; set; }

    public int? Percent { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}
