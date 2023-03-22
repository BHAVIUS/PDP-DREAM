// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebFile
{

  // for alternative approaches on getting downloading pdf file in aspnetcore
  // https://stackoverflow.com/questions/40486431/return-pdf-to-the-browser-using-asp-net-core
  public static PhysicalFileResult? GetPdfFile(string dirNam, string filNam)
  {
    PhysicalFileResult? pdfFile;
    try
    {
      string path = Path.Combine("~/docs/" + dirNam + "/");
      string fileName = filNam + ".pdf";
      string fileSpec = path + fileName;
      string mimeType = "application/pdf";
      pdfFile = new PhysicalFileResult(fileSpec, mimeType);
    }
    catch
    {
      pdfFile = null;
    }
    return pdfFile;
  }

  public static VirtualFileResult? GetPdf(string filename)
  {
    string fileSpec = "/docs/" + filename + ".pdf";
    return GetPdfVirtFile(fileSpec);
  }

  // TODO: dev/retest for Net 6
  public static async Task<IActionResult?> GetPdf2(string id)
  {
    FileContentResult? pdfFile;
    string fileName = id + ".pdf";
    string fileSpec = "/docs/" + fileName;
    string mimeType = "application/pdf";
    try
    {
      using (var client = new System.Net.Http.HttpClient())
      {
        byte[] data = await client.GetByteArrayAsync(fileSpec);
        pdfFile = new FileContentResult(data, mimeType)
        {
          FileDownloadName = id
        };
      }
    }
    catch { pdfFile = null; }
    return pdfFile;
  }

  public static VirtualFileResult? GetPdfVirtFile(string fileSpec)
  {
    VirtualFileResult? pdfFile;
    try
    {
      string mimeType = "application/pdf";
      pdfFile = new VirtualFileResult(fileSpec, mimeType);
    }
    catch
    {
      pdfFile = null;
    }
    return pdfFile;
  }

} // end class

// end file