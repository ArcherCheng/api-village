using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Tm2Activity
{
    public Guid ActivityId { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime ActivityDate { get; set; }

    public int? ActivityPns { get; set; }

    public DateTime ExpiredDate { get; set; }

    public string TeamId { get; set; } = null!;

    public Guid? UserId { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Au1Team Team { get; set; } = null!;

    public virtual Au1User? User { get; set; }
}
