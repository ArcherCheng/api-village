using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Ma2MasterExperience
{
    public int Id { get; set; }

    public string TeamId { get; set; } = null!;

    public decimal? OrderNo { get; set; }

    public string? OrderTitle { get; set; }

    public string Descriptions { get; set; } = null!;

    public string? Notes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Ma1Master Team { get; set; } = null!;
}
