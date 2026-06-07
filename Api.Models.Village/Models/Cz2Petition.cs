using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Cz2Petition
{
    public Guid PetitionId { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Content { get; set; }

    public bool? IsTop { get; set; }

    public int? TopDays { get; set; }

    public string Status { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public DateOnly CreateDate { get; set; }

    public DateOnly UpadteDate { get; set; }

    public string TeamId { get; set; } = null!;

    public Guid? UserId { get; set; }

    public string? CitizenName { get; set; }

    public string? CitizenPhone { get; set; }

    public string? CitizenLineUserId { get; set; }

    public string? WriteInfo { get; set; }

    public virtual ICollection<Cz2PetitionReply> Cz2PetitionReply { get; set; } = new List<Cz2PetitionReply>();

    public virtual Au1Team Team { get; set; } = null!;

    public virtual Au1User? User { get; set; }
}
