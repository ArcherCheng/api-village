using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class AppUserStar
{
    public long Id { get; set; }

    public string SourceTable { get; set; } = null!;

    public Guid SourceId { get; set; }

    public int LikeStar { get; set; }

    public Guid? UserId { get; set; }

    public string? MacGuid { get; set; }

    public string? IpAddress { get; set; }

    public string? TeamId { get; set; }

    public DateTime? WriteTime { get; set; }
}
