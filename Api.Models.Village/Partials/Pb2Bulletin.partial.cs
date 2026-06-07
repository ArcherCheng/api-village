using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Pb2Bulletin : BaseEntity, IBaseEntity
{
    // public int GetId()
    // {
    //     return this.Id;
    // }

    public override string GetKeyType()
    {
        return "guid";
    }
    public string? WriteInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}