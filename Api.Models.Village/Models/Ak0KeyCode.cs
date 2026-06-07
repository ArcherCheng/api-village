using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Ak0KeyCode
{
    public int Id { get; set; }

    public string CodeGroup { get; set; } = null!;

    public string CodeLabel { get; set; } = null!;

    public string CodeValue { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsOnOff { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }
}
