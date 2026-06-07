using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Au1User
{
    public Guid UserId { get; set; }

    public string TeamId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string MobileTel { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string? Email { get; set; }

    public string? PhotoUrl { get; set; }

    public bool IsOnOff { get; set; }

    public int UserType { get; set; }

    public int UserCode { get; set; }

    public string? UserData { get; set; }

    public string? UserRole { get; set; }

    public DateTime? LoginDate { get; set; }

    public DateTime? LastDate { get; set; }

    public DateTime? PasswordChangeDate { get; set; }

    public bool IsNeedChangePassword { get; set; }

    public string? Notes { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? WriteInfo { get; set; }

    public virtual ICollection<Cz2Petition> Cz2Petition { get; set; } = new List<Cz2Petition>();

    public virtual ICollection<Cz2Repair> Cz2Repair { get; set; } = new List<Cz2Repair>();

    public virtual ICollection<Pb2Bulletin> Pb2Bulletin { get; set; } = new List<Pb2Bulletin>();

    public virtual ICollection<Pb2Forum> Pb2Forum { get; set; } = new List<Pb2Forum>();

    public virtual ICollection<Pb2ForumReply> Pb2ForumReply { get; set; } = new List<Pb2ForumReply>();

    public virtual ICollection<Tm2Activity> Tm2Activity { get; set; } = new List<Tm2Activity>();

    public virtual ICollection<Tm2Announcement> Tm2Announcement { get; set; } = new List<Tm2Announcement>();
}
