using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class AppDataLog
{
    public long Id { get; set; }

    public string TableName { get; set; } = null!;

    public string? TableKey { get; set; }

    public int? WriteType { get; set; }

    public DateTime? WriteTime { get; set; }

    public string? NewData { get; set; }

    public string? OldData { get; set; }
}
