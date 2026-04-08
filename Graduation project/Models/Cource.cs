using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class Cource
{
    public int CourceId { get; set; }

    public string? DifficultyLevel { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Hours { get; set; }

    public int? Modules { get; set; }
}
