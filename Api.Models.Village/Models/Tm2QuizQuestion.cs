using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Tm2QuizQuestion
{
    public Guid QuestionId { get; set; }

    public Guid SubjectId { get; set; }

    public string QuestionDesc { get; set; } = null!;

    public decimal SortOrder { get; set; }

    public bool IsOnOff { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Tm2QuizSubject Subject { get; set; } = null!;

    public virtual ICollection<Tm2QuizOption> Tm2QuizOption { get; set; } = new List<Tm2QuizOption>();
}
