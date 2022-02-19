// PdpFormManager.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpFileStringForm
{
  public static string[] SplitString(string content, string? separator = null, bool removeEmptyLines = false)
  {
    string[] allLines;
    if (string.IsNullOrEmpty(separator)) { separator = Environment.NewLine; }
    if (removeEmptyLines) { allLines = content.Split(separator, StringSplitOptions.RemoveEmptyEntries); }
    else { allLines = content.Split(separator, StringSplitOptions.None); }
    return allLines;
  }

  public static string JoinStrings(IEnumerable<string> content, string? separator = null)
  {
    string oneLine;
    if (string.IsNullOrEmpty(separator)) { separator = Environment.NewLine; }
    oneLine = string.Join(separator, content);
    return oneLine;
  }

  public static string ParseFormFilesToFirstFile(IFormFileCollection formFiles)
  {
    var firstFile = string.Empty;
    var fileCount = 0;
    var fileList = new List<string>();
    var fileContents = new List<string>();
    foreach (var file in formFiles)
    {
      fileCount += 1;
      var fileLength = file.Length;
      var fileCDisp = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
      var fileName = Path.GetFileName(fileCDisp.FileName.ToString().Trim('"'));
      fileList.Add($"{fileName} file with length {fileLength} bytes");
      // https://stackoverflow.com/questions/57460048/when-does-ide0063-dispose
      using StreamReader reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
      var fileText = reader.ReadToEnd().Replace("\r", " ").Replace("\n", " ");
      if (fileCount == 1) { firstFile = fileText; }
      fileContents.Add(fileText);
    }
    return firstFile;
  }

}
