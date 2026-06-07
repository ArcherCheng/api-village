using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class AppUserMachine
{
    public long Id { get; set; }

    public string VerifyCode { get; set; } = null!;

    public int? VerifyMinutes { get; set; }

    public DateTime? CanVerifyTime { get; set; }

    public bool IsVerified { get; set; }

    public int? ErrorTimes { get; set; }

    public string? Notes { get; set; }

    public Guid? UserId { get; set; }

    public string? MacGuid { get; set; }

    public string? IpAddress { get; set; }

    public string? TeamId { get; set; }

    public DateTime? WriteTime { get; set; }
}
