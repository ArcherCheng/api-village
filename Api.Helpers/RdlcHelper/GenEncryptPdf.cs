// using System;
// using System.IO;
// using Microsoft.Reporting.NETCore;
// using Spire.Pdf;
// //using iText.Kernel.Pdf;

// namespace Api.Helpers;

// public static class PdfExtensions
// {
//     // public static string GenerateRdlcPdf<TEntity>(string RdlcTemplateFilepath, TEntity dataset, int payMonth, string empId)
//     // {
//     //     if (!System.IO.File.Exists(RdlcTemplateFilepath)) {
//     //         throw new Exception($"{RdlcTemplateFilepath} File is not exists");
//     //     }

//     //     var resourcesFolder = Api.Helpers.AppSettingsHelper.ResourcesFolder();
//     //     var pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(),resourcesFolder,"Payroll",payMonth.ToString());
//     //     if (!Directory.Exists(pdfDirectory)) {
//     //         Directory.CreateDirectory(pdfDirectory);
//     //     }
//     //     var pdfFilename = $"{payMonth}-{empId}.pdf";
//     //     var pdfFilepath = Path.Combine(pdfDirectory, pdfFilename);
//     //     if (System.IO.File.Exists(pdfFilepath)) {
//     //         System.IO.File.Delete(pdfFilepath);
//     //     }

//     //     using(Stream streamReader = File.Open(RdlcTemplateFilepath,FileMode.Open,FileAccess.Read))
//     //     {
//     //         LocalReport report = new LocalReport();
//     //         report.LoadReportDefinition(streamReader); 
//     //         report.DataSources.Add(new ReportDataSource("DataSet1",dataset));
//     //         byte[] bytes = report.Render("PDF");
//     //         using Stream streamWriter = new FileStream(pdfFilepath, FileMode.Create);
//     //         streamWriter.Write(bytes, 0, bytes.Length);
//     //     }

//     //     return pdfFilepath;
//     // }

//     //[Obsolete]
//     public static string EncryptPdf(string pdfFilepath, string userPassword, string encryptName="Encrypt-")
//     {
//         if (!System.IO.File.Exists(pdfFilepath)) {
//             throw new Exception($"{pdfFilepath} File is not exists");
//         }         
//         string filename = Path.GetFileName(pdfFilepath);
//         string dir = Path.GetDirectoryName(pdfFilepath)!;
//         string outFilepath = Path.Combine(dir,encryptName+filename);
//         if (System.IO.File.Exists(outFilepath)) {
//             System.IO.File.Delete(outFilepath);
//         }
//         //// using iText
//         // using(Stream input = new FileStream(pdfFilepath, FileMode.Open, FileAccess.Read, FileShare.Read))
//         // using(Stream output = new FileStream(outFilepath,FileMode.Create,FileAccess.Write,FileShare.None))
//         // {
//         //     PdfReader pdfReader = new(input);
//         //     byte[] ownerPass = System.Text.Encoding.ASCII.GetBytes(ownerPassword);
//         //     byte[] userPass = System.Text.Encoding.ASCII.GetBytes(userPassword);
//         //     WriterProperties props = new WriterProperties().SetStandardEncryption(
//         //         userPass,
//         //         ownerPass,
//         //         EncryptionConstants.ALLOW_PRINTING,
//         //         EncryptionConstants.ENCRYPTION_AES_128 | EncryptionConstants.DO_NOT_ENCRYPT_METADATA
//         //     );
//         //     using PdfWriter writer = new PdfWriter(output, props);
//         //     using PdfDocument pdfDoc = new PdfDocument(pdfReader, writer);
//         //     //pdfDoc.Close();
//         // }
//         //// use Spire.Pdf 
//         PdfDocument doc = new PdfDocument();
//         doc.LoadFromFile(pdfFilepath);
//         doc.Security.Encrypt(userPassword);
//         doc.SaveToFile(outFilepath,FileFormat.PDF);
//         doc.Dispose();
//         return outFilepath;
//     }
// }


