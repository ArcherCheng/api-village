using System.Collections.Generic;
using Api.Helpers;

namespace Api.Services;

public class BaseParas
{
    public Pagination Pagination { get; set; } = new Pagination();
    public ReportSignParas ReportSignParas { get; set; } = new ReportSignParas();
    public List<WhereCondition> WhereConditionList { get; set; } =[];

    public static string GetWhereFieldBeginValue(List<WhereCondition> WhereConditionList, string fieldName)
    {
        foreach (var item in WhereConditionList)
        {
            if(item.Field.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(item.Value)) {
                return item.Value;
            }
        }
        return "";
    }

    public static string GetWhereFieldEndValue(List<WhereCondition> WhereConditionList, string fieldName)
    {
        string value = "";
        foreach (var item in WhereConditionList)
        {
            if(item.Field.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(item.Value)) {
                value = item.Value;
            }
        }
        return value;
    }        
}