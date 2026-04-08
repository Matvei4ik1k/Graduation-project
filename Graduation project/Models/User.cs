using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly? RegistrationDate { get; set; }

    public int? RoleId { get; set; }
}
