using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Cz2RepairReply: BaseEntity, IBaseEntity
{
    // public int GetId()
    // {
    //     return this.Id;
    // }

    public override string GetKeyType()
    {
        return "guid";
    }

}
