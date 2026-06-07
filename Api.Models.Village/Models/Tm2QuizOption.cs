using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Tm2QuizOption
{
    public Guid OptionId { get; set; }

    public Guid QuestionId { get; set; }

    public string OptionDesc { get; set; } = null!;

    public decimal SortOrder { get; set; }

    public bool IsOnOff { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Tm2QuizQuestion Question { get; set; } = null!;
}
