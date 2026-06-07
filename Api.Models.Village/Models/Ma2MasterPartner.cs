using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Ma2MasterPartner
{
    public Guid PartnerId { get; set; }

    public string TeamId { get; set; } = null!;

    public decimal OrderNo { get; set; }

    public string Title { get; set; } = null!;

    public string PartnerName { get; set; } = null!;

    public string? Description { get; set; }

    public string Sex { get; set; } = null!;

    public string? MobileTel { get; set; }

    public string? PhotoUrl { get; set; }

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Ma1Master Team { get; set; } = null!;
}
