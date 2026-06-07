using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class AppUserRequest
{
    public long Id { get; set; }

    public string? UserIdName { get; set; }

    public string? ComponentId { get; set; }

    public string? ControllerId { get; set; }

    public string? ActionId { get; set; }

    public string? HttpVerb { get; set; }

    public string? HttpRoute { get; set; }

    public string? QueryString { get; set; }

    public bool? IsSuccess { get; set; }

    public Guid? UserId { get; set; }

    public string? MacGuid { get; set; }

    public string? IpAddress { get; set; }

    public string? TeamId { get; set; }

    public DateTime? WriteTime { get; set; }
}
