using System;
using System.Collections.Generic;

namespace Api.Services;

public partial class DtoAu1Team
{
    public string TeamId { get; set; } = null!;

    public string MasterName { get; set; } = null!;

    public DateOnly? BeginDate { get; set; }

    public string MobileTel { get; set; } = null!;

    public string? Email { get; set; }

    public int MonthAmt { get; set; }

    public int YearAmt { get; set; }

    public string? Notes { get; set; }

    public bool IsOnOff { get; set; }

    public string? WriteInfo { get; set; }

}
