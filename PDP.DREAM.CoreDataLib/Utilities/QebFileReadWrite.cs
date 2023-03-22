// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebFile
{
  public static string ReadFileByLine(string fileName, out int lineCount)
  {
    lineCount = 0;
    string line;
    var sb = new StringBuilder();
    using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
    {

      while ((line = reader.ReadLine()) != null)
      {
        if (!string.IsNullOrWhiteSpace(line))
        { sb.AppendLine(line); lineCount += 1; }
      }
    }
    return sb.ToString();
  }

  public static void ReadFileAndCountLines(string fileName)
  {
    string htmlpage;
    int numLines;
    htmlpage = ReadFileByLine(fileName, out numLines);
    Console.WriteLine(htmlpage);
    Console.WriteLine("Line Count = {0}", numLines);
    Console.WriteLine("Hit Enter key to continue.");
    Console.ReadLine();
  }


  public static string ReadStreamAsString(MemoryStream ms)
  {
    ms.Position = 0; // resets stream to beginning
    StreamReader sr = new StreamReader(ms);
    string msContent = sr.ReadToEnd();
    return msContent;
  }

  public static byte[] StringToByteArray(string strIn)
  {
    // System.Text.UTF8Encoding
    var encoder = new UTF8Encoding();
    byte[] bytOut = null;
    bytOut = encoder.GetBytes(strIn);
    return bytOut;
  }

  public static void ConsoleContinue(bool needPause = false, bool needExtraLine = false)
  {
    Console.WriteLine();
    if (needExtraLine)
    {
      Console.WriteLine();
    }
    if (needPause)
    {
      Console.WriteLine("Hit Enter key to continue.");
      Console.ReadLine();
    }
  }

  // TODO: extend to additional overloads to handle filenames with relative paths
  public static string[] FileReadAllLines(this string fileName)
  {
    string[] contents = Array.Empty<string>();
    if (File.Exists(fileName)) // requires full absolute path
    {
      contents = File.ReadAllLines(fileName, Encoding.UTF8);
    }
    return contents;
  }
  public static bool FileWriteAllLines(this string fileName, string[] contents)
  {
    var fileWritten = false;
    if (!string.IsNullOrEmpty(fileName) && !File.Exists(fileName))
    {
      try
      {
        File.WriteAllLines(fileName, contents, Encoding.UTF8);
        fileWritten = true;
      }
      catch (IOException exc)
      {
        Debug.WriteLine("IOException source: {0}", exc.Source);
      }
    }
    return fileWritten;
  }
  public static bool FileWriteAllLines(this string fileName, IList<string[]>? page)
  {
    var fileWritten = false;
    string[] contents = Array.Empty<string>();
    foreach (string[] part in page)
    {
      contents = contents.ArrayAppendArray(part);
    }
    fileWritten = fileName.FileWriteAllLines(contents);
    return fileWritten;
  }
  public static bool FileWriteAllLines(this string fileName, IDictionary<int, string[]>? book)
  {
    string[] contents = Array.Empty<string>();
    foreach (KeyValuePair<int, string[]> page in book)
    {
      var pageKey = page.Key;
      var pageValue = page.Value;
      contents = contents.ArrayAppendArray(pageValue);
    }
    return fileName.FileWriteAllLines(contents);
  }

} // end class

// end file