using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Au1Team : BaseEntity, IBaseEntity
{
    // public string GetId()
    // {
    //     return this.TeamId;
    // }

    public override string GetKeyType()
    {
        return "string";
    }
}
