using System;

namespace Api.Services;

    
/// <summary>
/// 批次檔案轉入參數檔
/// </summary>
public class ImportCsvParas
{
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
    public int BatchMonth { get; set; }
    public DateTime BatchDate { get; set; }
    public DateTime BatchBeginDate { get; set; }
    public DateTime BatchEndDate { get; set; }
    public required string BatchKeyField { get; set; }  //例如獎金代號 01
    public bool IsCheckEmpName { get; set;} = true;
    public bool IsDeleteAllTemp { get; set;} = true;       
    public required string EncodeingType { get; set;} = "UTF8";
    public required string FileType { get; set;} = "CSV";        
}
 