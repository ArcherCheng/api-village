using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Pb2Bulletin
{
    public Guid BbsId { get; set; }

    public string Subject { get; set; } = null!;

    public DateOnly AtDate { get; set; }

    public string? DocNo { get; set; }

    public string? SpeedType { get; set; }

    public string? SecretType { get; set; }

    public string? Recipient { get; set; }

    public string? Secondary { get; set; }

    public string? PdfFileUrl { get; set; }

    public bool IsTop { get; set; }

    public int? TopDays { get; set; }

    public bool IsDelete { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreateTime { get; set; }

    public int ReadTimes { get; set; }

    public int LikeTimes { get; set; }

    public string? CreateUser { get; set; }

    public string? UpdateUser { get; set; }

    public virtual ICollection<Pb2BulletinItem> Pb2BulletinItem { get; set; } = new List<Pb2BulletinItem>();

    public virtual Au1User User { get; set; } = null!;
}
