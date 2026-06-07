using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class ViewCity
{
    public string? NationId { get; set; }

    public string? CityId { get; set; }

    public string? City { get; set; }

    public int? CityOrder { get; set; }
}
