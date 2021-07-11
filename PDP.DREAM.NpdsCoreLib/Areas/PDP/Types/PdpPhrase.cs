// PdpPhrase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Linq;

namespace PDP.DREAM.NpdsCoreLib.Types
{
  public static class PdpPhrase
  {
    public static string[] GramArtPrepConj
      = { "A", "AN", "AND", "AT", "BY", "FOR", "FROM", "IN", "OF", "THE", "TO", "WITH" };

    public static char[] GramWordSplit = { ' ', '.', ':', '?' };

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

    public static string CleanPhrase(this string phrase)
    {
      return phrase.RemoveBraces().RemoveBrackets().RemoveAngles().RemoveParens();
    }

    public static string CreateAcronym(this string phrase, int minChars = 5, int maxChars = 9)
    {
      string acronym = "";
      if (!string.IsNullOrEmpty(phrase))
      {
        string[] phraseWords = phrase.ToUpper().Split(GramWordSplit, StringSplitOptions.RemoveEmptyEntries);
        int wordCount = phraseWords.Length;
        int charCount = 0;
        if (wordCount > 0)
        {
          for (int idx = 0; idx < wordCount; idx++)
          {
            var word = phraseWords[idx];
            if (!GramArtPrepConj.Contains(word))
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

    // TODO: convert status string to enum
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
      else if (status.Contains("partial", StringComparison.OrdinalIgnoreCase))
      { spanHtml = $"{phrase} <span class='pdpStatusPartial'> --> </span>"; }
      return spanHtml;
    }

  } // end class

} // end namespace
