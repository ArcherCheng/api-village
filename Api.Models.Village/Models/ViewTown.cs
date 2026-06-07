using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class ViewTown
{
    public string? NationId { get; set; }

    public string? CityId { get; set; }

    public string? City { get; set; }

    public string? TownId { get; set; }

    public string? Town { get; set; }

    public int? TownOrder { get; set; }
}
