using System;
using System.Collections.Generic;
using Api.Helpers;

#nullable disable

namespace Api.Services;

public class EmailPayrollParas
{
    public int BatchMonth { get; set; }
    public string DepIdBegin { get; set; }
    public string DepIdEnd { get; set; }
    public string EmpIdBegin { get; set; }
    public string EmpIdEnd { get; set; }
    public string BonusId { get; set; }
}
