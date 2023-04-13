// QebStringPhrase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebString
{
  public static string[] PdpGramArtPrepConj
    = { "A", "AN", "AND", "AT", "BY", "FOR", "FROM", "IN", "OF", "THE", "TO", "WITH" };

  public static char[] PdpGramWordSplit = { ' ', '.', ':', '?' };

  public static string DateTimeNowSortString()
  {
    // var dtnSort = DateTime.Now.ToString("s").RemoveHyphen().RemoveColon();
    var dtnSort = DateTime.Now.ToString(PdpDateTimeNowSortFormat);
    return dtnSort;
  }
  public static string AssureInitialSlash(this string content)
  {
    var slashed = content;
    if ((content.Length > 0) && (content[0] != '/'))
    { slashed = content.Insert(0, "/"); }
    return slashed;
  }
  public static string AssureInitialUnderscore(this string content)
  {
    var uscored = content;
    if ((content.Length > 0) && (content[0] != '_'))
    { uscored = content.Insert(0, "_"); }
    return uscored;
  }

  public static string RemoveColon(this string phrase)
  {
    return phrase.Replace(":", "");
  }
  public static string RemoveHyphen(this string phrase)
  {
    return phrase.Replace("-", "");
  }
  public static string RemoveBraces(this string phrase)
  {
    return phrase.Replace("{", "").Replace("}", "");
  }
  public static string RemoveBrackets(this string phrase)
  {
    return phrase.Replace("[", "").Replace("]", "");
  }
  public static string RemoveAngles(this string phrase)
  {
    return phrase.Replace("<", "").Replace(">", "");
  }
  public static string RemoveParens(this string phrase)
  {
    return phrase.Replace("(", "").Replace(")", "");
  }
  public static string RemoveBracesAndTrim(this string content)
  {
    return content.Replace("{", "").Replace("}", "").Trim();
  }
  public static string RemovePunctuation(this string content)
  {
    var puncfree = content.Trim();
    puncfree = new string(puncfree.Where(c => !char.IsPunctuation(c)).ToArray());
    return puncfree;
  }
  public static string PuncFreeSubstr(this string content, int length = 16)
  {
    var puncfree = content.RemovePunctuation();
    if (puncfree.Length > length) { puncfree = puncfree.Substring(0, length); }
    return puncfree;
  }


  public static string CleanPhrase(this string phrase)
  {
    return phrase.RemoveBraces().RemoveBrackets().RemoveAngles().RemoveParens();
  }
  public static string CreateAcronym(this string phrase, int minChars = 5, int maxChars = 9)
  {
    string acronym = "";
    if (!string.IsNullOrEmpty(phrase))
    {
      string[] phraseWords = phrase.ToUpper().Split(PdpGramWordSplit, StringSplitOptions.RemoveEmptyEntries);
      int wordCount = phraseWords.Length;
      int charCount = 0;
      if (wordCount > 0)
      {
        for (int idx = 0; idx < wordCount; idx++)
        {
          var word = phraseWords[idx];
          if (!PdpGramArtPrepConj.Contains(word))
          {
            acronym += word[0];
            charCount += 1;
          }
          if (charCount >= maxChars) { break; }
        }
      }
      if (charCount < minChars)
      {
        var numChars = (short)(minChars - charCount);
        acronym += PdpRandom.RandUpperString(numChars);
      }
    }
    return acronym;
  }

  // TODO: convert status string to enum with invalid|valid|unknown|pending|partial|complete|truncated
  // TODO: convert css class strings to enum for pdpStatus* series 
  public static string ToColorSpan(this string? phrase, string status = "")
  {
    string spanHtml = "";
    if (string.IsNullOrWhiteSpace(phrase)) { phrase = "Invalid"; }
    if (string.IsNullOrWhiteSpace(status)) { status = phrase; }
    if (status.Contains("invalid", StringComparison.OrdinalIgnoreCase))
    { spanHtml = $"<span class='pdpStatusInvalid'>{phrase}</span>"; }
    else if (status.Contains("valid", StringComparison.OrdinalIgnoreCase))
    { spanHtml = $"<span class='pdpStatusValid'>{phrase}</span>"; }
    else if (status.Contains("unknown", StringComparison.OrdinalIgnoreCase))
    { spanHtml = $"<span class='pdpStatusUnknown'>{phrase}</span>"; }
    else if (status.Contains("pending", StringComparison.OrdinalIgnoreCase))
    { spanHtml = $"<span class='pdpStatusPending'>{phrase}</span>"; }
    else if (status.Contains("pdpHover", StringComparison.OrdinalIgnoreCase))
    { spanHtml = $"{phrase} <span class='pdpHover'> --> </span>"; }
    else if (status.Contains("truncated", StringComparison.OrdinalIgnoreCase))
    { spanHtml = $"{phrase} <span class='pdpStatusTruncated'> --> </span>"; }
    // TODO: deprecate use of pdpStatusPartial and pdpStatusTruncated ???
    else if (status.Contains("partial", StringComparison.OrdinalIgnoreCase))
    { spanHtml = $"{phrase} <span class='pdpStatusPartial'> --> </span>"; }
    return spanHtml;
  }
  // TODO: recode all the keys above with string constants
  //  and use alternative key to replace "partial" with "truncated"
  public static string ToTruncatedPhrase(this string? fullPhrase, int maxChars)
  {
    var partPhrase = string.Empty;
    if (fullPhrase != null)
    {
      partPhrase = ((fullPhrase.Length > maxChars) ?
       fullPhrase.Substring(0, maxChars).ToColorSpan("truncated") : fullPhrase);
    }
    return partPhrase;
  }
  public static string ToHoverHideHtml(this string? fullText, int maxChars)
  {
    var hhHtml = string.Empty;
    if (!string.IsNullOrEmpty(fullText))
    {
      var lenPhrase = fullText.Length;
      if (lenPhrase > maxChars)
      {
        //var part1 = fullText.Substring(0, maxChars);
        //var part2 = fullText.Substring(maxChars, (lenPhrase - maxChars));
        // hhHtml = $"{part1}<span class='pdpHover'> --> </span><span class='pdpHide'>{part2}</span>";
        // hhHtml = $"{part1}<img src='/RAB3v1.ico' style='cursor:pointer;' title='{part2}'";
        hhHtml = $"{fullText.Substring(0, maxChars)}<img src='/RAB3v1.ico' style='cursor:pointer;' title='{fullText}'";
      }
      else { hhHtml = fullText; }
    }
    return hhHtml;
  }

  public static string ToDigitNumberString(this string? str, int digits)
  {
    var num = 0;
    try { num = int.Parse(str); }
    catch { num = 1; }
    var ds = $"D{digits.ToString()}";
    var dnumstr = num.ToString(ds);
    return dnumstr;
  }


} // end class

// end file