using System.Collections.Generic;
using Microsoft.Reporting.NETCore;

namespace Api.Helpers;

public static class GenRdlcReportExtensions
{
    
    public static byte[] GenerateRdlcTemplate<T>(string templateFilepath,IEnumerable<T> dataSource, List<ReportParameter> reportParameters,string renderFormat)
    {
        using var stream = System.IO.File.Open(templateFilepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        LocalReport report = new LocalReport();
        report.LoadReportDefinition(stream);
        report.DataSources.Add(new ReportDataSource("DataSet1", dataSource));
        report.SetParameters(reportParameters);
        byte[] bytes = report.Render(renderFormat);
        return bytes;
        //return File(bytes,System.Net.Mime.MediaTypeNames.Application.Octet,outputType.outputFileName); 
    } 

    public static (string outputFileName, string renderFormat) GetOutputReportNameAndType(string reportName, string reportType = "XLSX" )
    {
        string outputFileName;
        string renderFormat;

        switch (reportType.ToUpper())
        {
            case "EXCEL":
            case "XLSX":
            case "EXCELOPENXML":
                outputFileName = reportName+"-"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".xlsx";
                renderFormat = "EXCELOPENXML";
                break;
            case "XLS":
                outputFileName = reportName+"-"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";
                renderFormat = "EXCEL";
                break;
            case "HTML":
            case "HTML4":
            case "HTML5":
                outputFileName =reportName+"-"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".html";
                renderFormat = "HTML5";
                break;
            case "DOC":
            case "WORD":
                outputFileName = reportName+"-"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".doc";
                renderFormat = "WORD";
                break;
            case "WORDOPENXML":
            case "DOCX":
                outputFileName = reportName+"-"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".docx";
                renderFormat = "WORDOPENXML";
                break;
            case "JPG":
            case "JEPG":
            case "PNG":
            case "IMAGE":
                outputFileName = reportName+"-"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".png";
                renderFormat = "IMAGE";
                break;
            case "PDF":
            default:
                outputFileName = reportName+"-"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".pdf";
                renderFormat = "PDF";
                break;
        }
        return (outputFileName,renderFormat);
    }    

}