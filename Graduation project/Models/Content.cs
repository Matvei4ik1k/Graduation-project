using System;
using System.Collections.Generic;

namespace Graduation_project.Models;

public partial class Content
{
    public int ContentId { get; set; }

    public string Type { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ContentUrl { get; set; }

    public int? Duration { get; set; }

    public string? Difficulty { get; set; }

    public string? Tag { get; set; }

    public string? Author { get; set; }

    public int? CategoryId { get; set; }
}
