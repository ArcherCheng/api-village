using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Cz2Repair
{
    public Guid RepairId { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Content { get; set; }

    public string? Arrdess { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? ImageUrl { get; set; }

    public string? Source { get; set; }

    public DateOnly AtDate { get; set; }

    public bool? IsTop { get; set; }

    public int? TopDays { get; set; }

    public string Status { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public Guid? UserId { get; set; }

    public string? CitizenName { get; set; }

    public string? CitizenPhone { get; set; }

    public string? CitizenLineUserId { get; set; }

    public string? AiSummary { get; set; }

    public string? WriteInfo { get; set; }

    public virtual ICollection<Cz2RepairReply> Cz2RepairReply { get; set; } = new List<Cz2RepairReply>();

    public virtual Au1Team Team { get; set; } = null!;

    public virtual Au1User? User { get; set; }
}
