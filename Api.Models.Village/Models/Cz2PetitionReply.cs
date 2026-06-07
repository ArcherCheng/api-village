using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Cz2PetitionReply
{
    public Guid ReplyId { get; set; }

    public Guid PetitionId { get; set; }

    public int ReplyType { get; set; }

    public string Content { get; set; } = null!;

    public Guid? UserId { get; set; }

    public string? WriteInfo { get; set; }

    public virtual Cz2Petition Petition { get; set; } = null!;
}
