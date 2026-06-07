using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Pb2BulletinItem
{
    public int Id { get; set; }

    public Guid BbsId { get; set; }

    public int SortOrder { get; set; }

    public string DocOrder { get; set; } = null!;

    public string Contents { get; set; } = null!;

    public string? CreateUser { get; set; }

    public string? UpdateUser { get; set; }

    public virtual Pb2Bulletin Bbs { get; set; } = null!;
}
