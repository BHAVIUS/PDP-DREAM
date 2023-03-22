// QebStringForm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebString
{
  public const string OrSeparator = "|";
  public const string OrSeparatorPadded = " | ";
  // ATTN: on Microsoft Windows OS, using only "Environment.NewLine" is not sufficient
  public static string[] EmptyLineSeparators = new string[] { "\r\n", "\r", "\n" };

  public static string JoinListToOrString(this IList<string>? orList)
  {
    return string.Join(OrSeparatorPadded, orList);
  }
  public static IList<string>? SplitOrStringToList(this string? orString)
  {
    return orString.Split(OrSeparator, StringSplitOptions.TrimEntries).ToList();
  }

  public static string[] NormalizeStringsToArray(this IList<string>? contents)
  {
    return contents.JoinStrings().SplitString().ToArray(); // normalize CRLF
  }
  public static string[] NormalizeStringsToArray(this IList<string[]>? page)
  {
    string[] contents = Array.Empty<string>();
    foreach (string[] part in page)
    {
      contents = contents.ArrayAppendArray(part);
    }
    return contents;
  }
  public static string[] NormalizeStringsToArray(this IDictionary<int, string[]>? book)
  {
    string[] contents = Array.Empty<string>();
    foreach (KeyValuePair<int, string[]> page in book)
    {
      var pageKey = page.Key;
      var pageValue = page.Value;
      contents = contents.ArrayAppendArray(pageValue);
    }
    return contents;
  }

  public static string[] RemoveBlankStrings(this IList<string>? contents)
  {
    // next line with RemoveAll() not working in NET 7.0 ???
    // var numRemoved = contents.ToList().RemoveAll(s => (string.IsNullOrWhiteSpace(s) == true));
    // next line with where predicate is working in NET 7.0
    // var contents1 = contents.Where(s => (string.IsNullOrWhiteSpace(s) == false));
    // next line with where predicate is working in NET 7.0
    // var contents2 = contents.Where(s => (!string.IsNullOrWhiteSpace(s))).ToArray();
    // next line with where and select is working in NET 7.0
    // var contents3 = contents.Where(s => (!string.IsNullOrWhiteSpace(s))).Select(s => s.Trim()).ToArray();
    return contents.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToArray();
  }

  public static string JoinStrings(this IList<string>? contents, string? separator = null)
  {
    string oneLine;
    if (contents == null) { throw new ArgumentNullException(nameof(contents)); }
    if (string.IsNullOrEmpty(separator)) { separator = Environment.NewLine; }
    oneLine = string.Join(separator, contents).Trim();
    return oneLine;
  }

  public static string[] SplitString(this string content, bool trimRemoveEmpty = true, string[]? separator = null)
  {
    string[] allLines;
    if (separator == null) { separator = EmptyLineSeparators; }
    if (trimRemoveEmpty) { allLines = content.Split(separator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries); }
    else { allLines = content.Split(separator, StringSplitOptions.None); }
    return allLines;
  }

  public static IList<string[]> AppendPageDeck(this IList<string[]> page, string[] deck, int index)
  {
    if (deck.Length == 0) { return page; }
    try { page[index] = deck; }
    catch { page.Add(deck); }
    return page;
  }

  public static IDictionary<int, string[]> AppendBookPage(this IDictionary<int, string[]> book, string[] page, int index)
  {
    if (page.Length == 0) { return book; }
    try { book[index] = page; }
    catch { book.Add(index, page); }
    return book;
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
  public static bool AreLinesEqual(this string[] array1, string[] array2, bool removeBlanks = true)
  {
    if (removeBlanks)
    {
      array1 = array1.RemoveBlankStrings();
      array2 = array2.RemoveBlankStrings();
    }
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
    string[] array3 = array1.Concat(array2).ToArray();
    return array3;
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

  public static string[] FormFileStreamReadAllLines(this IFormFile formFile)
  {
    string[] contents = Array.Empty<string>();
    if (formFile.Length > 0)
    {
      using StreamReader reader = new StreamReader(formFile.OpenReadStream(), Encoding.UTF8);
      contents = reader.ReadToEnd().SplitString(); // normalize CRLF
    }
    return contents;
  }

  public static IDictionary<int, string[]>? ParseFormFilesToBook(this IFormFileCollection formFiles)
  {
    IDictionary<int, string[]>? book = null; // map files to pages in book
    var fileCount = 0;
    var fileList = new List<string>();
    foreach (var file in formFiles)
    {
      fileCount += 1;
      var fileContents = Array.Empty<string>();
      var fileLength = file.Length;
      var fileName = file.FileName;
      // https://stackoverflow.com/questions/64474442/where-is-the-file-path-of-iformfile
      // var fileCDisp = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
      // var fileName = Path.GetFileName(fileCDisp.FileName.ToString().Trim('"'));
      fileList.Add($"{fileName} file with length {fileLength} bytes");
      using StreamReader reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
      fileContents = reader.ReadToEnd().SplitString(); // normalize CRLF
      book.Add(fileCount, fileContents);
    }
    return book;
  }

} // end class

// end file