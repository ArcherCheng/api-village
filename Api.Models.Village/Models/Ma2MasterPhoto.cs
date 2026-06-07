using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Ma2MasterPhoto
{
    public int Id { get; set; }

    public string TeamId { get; set; } = null!;

    public decimal OrderNo { get; set; }

    public bool IsMain { get; set; }

    public string? PublicKey { get; set; }

    public string? PhotoUrl { get; set; }

    public string? Descriptions { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Ma1Master Team { get; set; } = null!;
}
