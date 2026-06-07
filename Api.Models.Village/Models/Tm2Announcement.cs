using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Tm2Announcement
{
    public Guid AnnounceId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public string? Category { get; set; }

    public string? Priority { get; set; }

    public bool? IsTop { get; set; }

    public int? TopDays { get; set; }

    public string? AttachmentUrl { get; set; }

    public DateTime AtDate { get; set; }

    public string Status { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public Guid? UserId { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Au1Team Team { get; set; } = null!;

    public virtual Au1User? User { get; set; }
}
