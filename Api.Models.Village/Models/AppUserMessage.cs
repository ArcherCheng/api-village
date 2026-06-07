using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class AppUserMessage
{
    public long Id { get; set; }

    public string SendNo { get; set; } = null!;

    public int SendType { get; set; }

    public bool IsSuccess { get; set; }

    public DateTime SendDate { get; set; }

    public string? SendSubject { get; set; }

    public string? SendMessage { get; set; }

    public string? ErrorMessage { get; set; }
}
