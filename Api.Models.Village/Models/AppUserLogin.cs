using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class AppUserLogin
{
    public long Id { get; set; }

    public string? LoginNname { get; set; }

    public string LoginStatus { get; set; } = null!;

    public bool? IsSuccess { get; set; }

    public string? TeamId { get; set; }

    public string? IpAddress { get; set; }

    public string? MacGuid { get; set; }

    public DateTime? WriteTime { get; set; }
}
