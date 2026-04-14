using System;
using System.Collections.Generic;

namespace Graduation_project.NewModels;

public partial class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
