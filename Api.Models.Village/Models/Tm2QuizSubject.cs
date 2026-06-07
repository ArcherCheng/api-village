using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Tm2QuizSubject
{
    public Guid SubjectId { get; set; }

    public string TeamId { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public bool IsOnOff { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Au1Team Team { get; set; } = null!;

    public virtual ICollection<Tm2QuizQuestion> Tm2QuizQuestion { get; set; } = new List<Tm2QuizQuestion>();
}
