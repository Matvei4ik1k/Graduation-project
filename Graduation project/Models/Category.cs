using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
