using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Pb2Forum
{
    public Guid ForumId { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Content { get; set; }

    public bool IsTop { get; set; }

    public int? TopDays { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public int ReadTimes { get; set; }

    public string? WriteInfo { get; set; }

    public virtual ICollection<Pb2ForumReply> Pb2ForumReply { get; set; } = new List<Pb2ForumReply>();

    public virtual Au1User User { get; set; } = null!;
}
