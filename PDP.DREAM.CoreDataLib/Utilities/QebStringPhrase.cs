// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebString
{
  public static string[] PdpGramArtPrepConj
    = { "A", "AN", "AND", "AT", "BY", "FOR", "FROM", "IN", "OF", "THE", "TO", "WITH" };

  public static char[] PdpGramWordSplit = { ' ', '.', '_', '-', ':', '?' };

  public static string DateTimeSortString(this DateTime? ndt)
  {
    var dt = ndt ?? DateTime.UtcNow;
    var ss = dt.ToString(PdpDateTimeSortFormat);
    return ss;
  }
  public static string DateTimeNowSortString()
  {
    var dtnSort = DateTime.UtcNow.ToString(PdpDateTimeSortFormat);
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
  public static string RemoveQuotes(this string phrase)
  {
    return phrase.Replace("\"", "");
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

  public static string CreateUniqueAcronym(this string phrase)
  {
    return (phrase.CleanPhrase().CreateAcronym() + DateTimeNowSortString());
  }
  public static string CleanPhrase(this string phrase)
  {
    return phrase.RemoveQuotes().RemoveBraces().RemoveBrackets().RemoveAngles().RemoveParens();
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

  // OSFNMC = Operating System File Name Max Chars
  public const int OSFNMC = 256;

  // OSFPMC = Operating System File Path Max Chars
  public const int OSFPMC = 512;

  // JSAM = JavaScript Alert Maxchars 
  public const int JSAM = 9999;

  public static string TruncateToMaxChars(this string? allText, int maxChars)
  {
    var partText = ESS;
    if (!string.IsNullOrEmpty(allText))
    {
      partText = ((allText.Length > maxChars) ?
       allText.Substring(0, maxChars) : allText);
    }
    return partText;
  }
  public static string? TruncateToJsam(this string? strLong)
  {
    string? strShort = strLong.TruncateToMaxChars(JSAM);
    return strShort;
  }

  // TODO: convert status string to enum
  //   for invalid|valid|unknown|pending|partial|complete|truncated
  // TODO: convert css class strings to enum for pdpStatus* series 
  // TODO: recode all the keys in ToColorSpan() with string constants
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
    return spanHtml;
  }
  // TODO: recode all the keys in ToColorSpan() with string constants

  // TODO: deprecate all use of the TruncateToHtml* methods ???
  // instead simply use the ShowInfosubset buttons ???
  // or just use a single version TruncateToHtmlFixlen
  // renamed as TruncateToHtml

  // TODO: rebuild/retest use of this method to avoid use <img /> tag
  // <img> tag with title not necessary if using ShowInfosubset button
  // intended for FIXed LENgth properties
  // (EntityName, EntityNature, SupportingTag)

  // TODO: reconcile/deconflict these constants 
  //  with similar related declarations elsewhere in code
  public const int MCGenShort = 128; // max chars generic short
  public const int MCGenLong = 512; // max chars generic long
  public static string TruncateForTkgr(this string? allText, int maxChars)
  {
    var hhHtml = ESS;
    if (!string.IsNullOrEmpty(allText))
    {
      var lenPhrase = allText.Length;
      if (lenPhrase > maxChars)
      {
        var partText = allText.Substring(0, maxChars);
        // TODO: consider use of both title for json alert,
        // also fulltext in popup window with JavaScript method call
        hhHtml = $"{partText}<img src='/RAB3v1.ico' title='{allText}' />";
      }
      else { hhHtml = allText; }
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