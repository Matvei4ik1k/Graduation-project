using System;
using System.Collections.Generic;

namespace Graduation_project.Model;

public partial class UserCourse
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CourseId { get; set; }

    public int? IndexLesson { get; set; }

    public int? Percent { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
