using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class AppTempSql
{
    public int Id { get; set; }

    public string? SqlDesc { get; set; }

    public string SqlExpress { get; set; } = null!;
}
