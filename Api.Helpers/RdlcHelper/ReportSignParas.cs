namespace Api.Helpers;

public class ReportSignParas
{
    // 改由 _BaseService.GetReportParameters(),由使用者 claims 產生
    // public string ReportUser { get; set; } //= "Archer";
    // public string? ReportCompany { get; set; } //= "Newsoft Company";
    public string ReportType { get; set; } = "XLSX";
    public string ReportTitle { get; set; } = "基本資料明細表";
    public string ReportNotes { get; set; } = "報表說明:";
    public string ReportSign1 { get; set; } = "製表:";
    public string ReportSign2 { get; set; } = "主管:";
    public string ReportSign3 { get; set; } = "經理:";
    public string ReportSign4 { get; set; } = "總經理:";
    public string ReportSign5 { get; set; } = "董事長:";
    public bool IsExecutionTime { get; set; } = true; 
    //public bool IsPageBreak { get; set; } //= "董事長/總經理:";
}

