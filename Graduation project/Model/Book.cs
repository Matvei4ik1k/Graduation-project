using System;
using System.Collections.Generic;

namespace Graduation_project.Model;

public partial class Book
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}
