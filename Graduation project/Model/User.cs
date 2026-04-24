using System;
using System.Collections.Generic;

namespace Graduation_project.Model;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly? RegistrationDate { get; set; }

    public virtual ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
