using System;
using System.Collections.Generic;

namespace Graduation_project.NewModels;

public partial class Course
{
    public int CourseId { get; set; }

    public string? DifficultyLevel { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Hours { get; set; }

    public int? Modules { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
