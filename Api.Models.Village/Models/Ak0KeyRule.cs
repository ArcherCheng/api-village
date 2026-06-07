using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Ak0KeyRule
{
    public string RuleId { get; set; } = null!;

    public string RuleGroup { get; set; } = null!;

    public string RuleLabel { get; set; } = null!;

    public string? RuleValue { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }
}
