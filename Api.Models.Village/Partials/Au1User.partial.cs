namespace Api.Models;

public partial class Au1User : BaseEntity, IBaseEntity
{
    public override string GetKeyType()
    {
        return "guid";
    }
}
