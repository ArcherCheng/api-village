using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Ma2MasterPartner: BaseEntity, IBaseEntity
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