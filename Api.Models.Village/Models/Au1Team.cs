using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Au1Team
{
    public string TeamId { get; set; } = null!;

    public string? NationId { get; set; }

    public string? CityId { get; set; }

    public string? City { get; set; }

    public string? CityCode { get; set; }

    public string? CityShort { get; set; }

    public string? TownId { get; set; }

    public string? Town { get; set; }

    public string? PostalCode { get; set; }

    public string? VillageId { get; set; }

    public string? Village { get; set; }

    public int? CityOrder { get; set; }

    public int? TownOrder { get; set; }

    public int? VillageOrder { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual ICollection<Cz2Petition> Cz2Petition { get; set; } = new List<Cz2Petition>();

    public virtual ICollection<Cz2Repair> Cz2Repair { get; set; } = new List<Cz2Repair>();

    public virtual Ma1Master? Ma1Master { get; set; }

    public virtual ICollection<Tm2Activity> Tm2Activity { get; set; } = new List<Tm2Activity>();

    public virtual ICollection<Tm2Announcement> Tm2Announcement { get; set; } = new List<Tm2Announcement>();

    public virtual ICollection<Tm2QuizSubject> Tm2QuizSubject { get; set; } = new List<Tm2QuizSubject>();
}
