using System;
using System.Collections.Generic;

namespace Graduation_project.Model;

public partial class Course
{
    public int Id { get; set; }

    public string? DifficultyLevel { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Hours { get; set; }

    public string? Modules { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
