using System;
using System.Collections.Generic;

namespace Graduation_project.NewModels;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }
}
