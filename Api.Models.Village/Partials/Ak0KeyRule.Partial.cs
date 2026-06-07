namespace Api.Models;

public partial class Ak0KeyRule : BaseEntity, IBaseEntity
{
    // public string GetId()
    // {
    //     return this.RuleId;
    // }

    public override string GetKeyType()
    {
        return "int";
    }
}
