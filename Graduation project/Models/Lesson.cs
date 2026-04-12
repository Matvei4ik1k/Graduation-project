using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class Lesson
{
    public int LessonsId { get; set; }

    public int? CourseId { get; set; }

    public int? TaskNumber { get; set; }

    public string? LessonTopic { get; set; }

    public string? TaskDescription { get; set; }

    public string? Instructions { get; set; }

    public virtual Course? Course { get; set; }
}
