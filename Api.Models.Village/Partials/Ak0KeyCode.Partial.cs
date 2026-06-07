using Api.Models;

namespace Api.Models;

public partial class Ak0KeyCode : BaseEntity, IBaseEntity
{
    // public int GetId()
    // {
    //     return this.Id;
    // }

    public override string GetKeyType()
    {
        return "int";
    }

}
