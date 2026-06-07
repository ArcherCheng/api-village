using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Ma1Master
{
    public string TeamId { get; set; } = null!;

    public string MasterName { get; set; } = null!;

    public string? Description { get; set; }

    public string Sex { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string? BirtCity { get; set; }

    public int? ElectYear { get; set; }

    public DateOnly? ElectDate { get; set; }

    public string? MobileTel { get; set; }

    public string? OfficeTel { get; set; }

    public string? Email { get; set; }

    public string? ServiceTime { get; set; }

    public string? Address { get; set; }

    public string? PhotoUrl { get; set; }

    public string? LineId { get; set; }

    public string? Facebook { get; set; }

    public string? Threads { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual ICollection<Ma2MasterEducation> Ma2MasterEducation { get; set; } = new List<Ma2MasterEducation>();

    public virtual ICollection<Ma2MasterExperience> Ma2MasterExperience { get; set; } = new List<Ma2MasterExperience>();

    public virtual ICollection<Ma2MasterPartner> Ma2MasterPartner { get; set; } = new List<Ma2MasterPartner>();

    public virtual ICollection<Ma2MasterPhoto> Ma2MasterPhoto { get; set; } = new List<Ma2MasterPhoto>();

    public virtual ICollection<Ma2MasterPolicy> Ma2MasterPolicy { get; set; } = new List<Ma2MasterPolicy>();

    public virtual Au1Team Team { get; set; } = null!;
}
