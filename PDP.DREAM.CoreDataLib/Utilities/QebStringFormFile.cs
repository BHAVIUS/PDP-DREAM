// PdpStringFormFile.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class PdpStringPhraseFormFile
{
  public const string OrSeparator = "|";
  public const string OrSeparatorPadded = " | ";
  public static string JoinListToOrString(this IList<string>? orList)
  {
    return string.Join(OrSeparatorPadded, orList);
  }
  public static IList<string>? SplitOrStringToList(this string? orString)
  {
    return orString.Split(OrSeparator, StringSplitOptions.TrimEntries).ToList();
  }

  public static string[] NormalizeStrings(this IEnumerable<string> content)
  {
    return content.JoinStrings().SplitString(); // normalize CRLF
  }

  public static string[] SplitString(this string content, bool removeEmptyLines = true, string[]? separator = null)
  {
    string[] allLines;
    // ATTN: on Microsoft Windows OS, using only "Environment.NewLine" is not sufficient
    // if (separator is null) { separator = new string[] { Environment.NewLine }; }
    if (separator is null) { separator = new string[] { "\r\n", "\r", "\n" }; }
    if (removeEmptyLines) { allLines = content.Split(separator, StringSplitOptions.RemoveEmptyEntries); }
    else { allLines = content.Split(separator, StringSplitOptions.None); }
    return allLines;
  }
  public static string JoinStrings(this IEnumerable<string> content, string? separator = null)
  {
    string oneLine;
    if (content is null) { throw new ArgumentNullException(nameof(content)); }
    if (string.IsNullOrEmpty(separator)) { separator = Environment.NewLine; }
    oneLine = string.Join(separator, content).Trim();
    return oneLine;
  }


  public static IDictionary<int, string[]> AppendBookPage(this IDictionary<int, string[]> book, string[] page, int index)
  {
    if (page.Length == 0) { return book; }
    book[index] = page;
    return book;
  }
  public static IList<string[]> AppendPagePart(this IList<string[]> page, string[] part, int index)
  {
    if (part.Length == 0) { return page; }
    page[index] = part;
    return page;
  }

  // this utility can be used to compare equality of two different files
  // current implementation requires filenames with full absolute paths 
  // TODO: write unit tests to confirm working as expected
  public static bool AreLinesEqual(this string fileName1, string fileName2)
  {
    var array1 = fileName1.FileReadAllLines();
    var array2 = fileName2.FileReadAllLines();
    return array1.AreLinesEqual(array2);
  }
  public static bool AreLinesEqual(this string[] array1, string[] array2)
  {
    return array1.SequenceEqual(array2);
  }

  public static bool ArrayEqualsArray(this string[] array1, string[] array2)
  {
    var areEqual = false;
    if (array1.Length != array2.Length) { return areEqual; }
    var lineIndex = 0;
    foreach (string line in array1)
    {
      if (!line.Equals(array2[lineIndex], StringComparison.Ordinal))
      {
        return areEqual;
      }
      lineIndex += 1;
    }
    areEqual = true;
    return areEqual;
  }

  public static string[] ArrayAppendArray(this string[] array1, string[] array2)
  {
    if (array1.Length == 0) { return array2; }
    if (array2.Length == 0) { return array1; }
    return array1.Concat(array2).ToArray();
  }
  public static IList<T> ListAppendList<T>(this IList<T> list1, IList<T> list2)
  {
    if (list1.Count == 0) { return list2; }
    if (list2.Count == 0) { return list1; }
    return list1.Concat(list2).ToList();
  }
  public static IList<string>? ListAppendString(this IList<string>? list1, string? line)
  {
    IList<string>? list2 = new List<string>() { line };
    return list1.ListAppendList(list2);
  }

  public static byte[]? StringListToByteArray(this IList<string>? list1)
  {
    byte[]? list2 = null;
    if (list1 != null && list1.Count > 0)
    {
      list2 = list1.SelectMany(s =>
        Encoding.UTF8.GetBytes(s + Environment.NewLine)).ToArray();
    }
    return list2;
  }

  public static string? StringEscapeHashLiteral(this string? withHash)
  {
    string? withoutHash = string.Empty;
    if (!string.IsNullOrEmpty(withHash))
    { withoutHash = withHash.Replace("#", "\\#"); }
    return withoutHash;
  }

  public static string? HtmlEscapeHashLiteral(this string? withHash)
  {
    return withHash.StringEscapeHashLiteral();
  }

  public static string? HtmlCleanByEncodeDecode(this string? dirty)
  {
    var clean = WebUtility.HtmlDecode(WebUtility.HtmlEncode(dirty));
    return clean;
  }
  public static string? UrlCleanByEncodeDecode(this string? dirty)
  {
    var clean = WebUtility.UrlDecode(WebUtility.UrlEncode(dirty));
    return clean;
  }

  public static IDictionary<int, string[]>? ParseFormFilesToBook(this IFormFileCollection formFiles)
  {
    IDictionary<int, string[]>? book = null; // map files to pages in book
    var fileCount = 0; // aka pageCount
    var fileList = new List<string>();
    foreach (var file in formFiles)
    {
      fileCount += 1;
      var fileContents = PdpAppConst.QebNESA; // aka pageContents
      var fileLength = file.Length;
      var fileName = file.FileName;
      // https://stackoverflow.com/questions/64474442/where-is-the-file-path-of-iformfile
      // var fileCDisp = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
      // var fileName = Path.GetFileName(fileCDisp.FileName.ToString().Trim('"'));
      fileList.Add($"{fileName} file with length {fileLength} bytes");
      using StreamReader reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
      fileContents = reader.ReadToEnd().SplitString(); // normalize CRLF
      book.Add(fileCount, fileContents); // aka pageCount and pageContents
    }
    return book;
  }

  // TODO: extend to additional overloads to handle filenames with relative paths
  public static string[] FileReadAllLines(this string fileName)
  {
    string[] contents = PdpAppConst.QebNESA;
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
        Console.WriteLine("IOException source: {0}", exc.Source);
      }
    }
    return fileWritten;
  }
  public static bool FileWriteAllLines(this string fileName, IDictionary<int, string[]>? book)
  {
    string[] contents = PdpAppConst.QebNESA;
    foreach (KeyValuePair<int, string[]> page in book)
    {
      var pageKey = page.Key;
      var pageValue = page.Value;
      contents.ArrayAppendArray(pageValue);
    }
    return fileName.FileWriteAllLines(contents);
  }

  public static string[] FormFileStreamReadAllLines(this IFormFile formFile)
  {
    string[] contents = PdpAppConst.QebNESA;
    if (formFile.Length > 0)
    {
      using StreamReader reader = new StreamReader(formFile.OpenReadStream(), Encoding.UTF8);
      contents = reader.ReadToEnd().SplitString(); // normalize CRLF
    }
    return contents;
  }

} // end class

// end file