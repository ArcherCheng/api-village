using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Pb2ForumReply
{
    public Guid ReplyId { get; set; }

    public Guid ParentId { get; set; }

    public Guid ForumId { get; set; }

    public string? Contents { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDelete { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Pb2Forum Forum { get; set; } = null!;

    public virtual Au1User User { get; set; } = null!;
}
