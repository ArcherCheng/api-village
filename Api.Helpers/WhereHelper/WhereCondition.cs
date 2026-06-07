namespace Api.Helpers;

#nullable disable
public class WhereCondition
{
    public string Field { get; set; }
    public string Type { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
    public string AndOr { get; set; }
    public int BracketOr { get; set; } 
}
