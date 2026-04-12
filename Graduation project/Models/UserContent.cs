using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class UserContent
{
    public int UserContentId { get; set; }

    public string? InteractionType { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? CompletionDate { get; set; }

    public decimal? ProgressPercent { get; set; }

    public int? Rating { get; set; }

    public string? Notes { get; set; }

    public int UserId { get; set; }

    public int ContentId { get; set; }
}
