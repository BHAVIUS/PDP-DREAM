// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class PdpXml
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

}