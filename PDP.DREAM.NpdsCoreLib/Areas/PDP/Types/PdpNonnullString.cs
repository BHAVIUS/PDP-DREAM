// PdpNonnullString.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Types
{
  public static class PdpNonnullString
  {
    public static string ParseToNonNullable(this string? nullableValue, string defaultValue)
    {
      var nonnullValue = ((string.IsNullOrWhiteSpace(nullableValue)) ? defaultValue : nullableValue);
      return nonnullValue;
    }

    public static string ParseLeft(this string? nullableValue, int maxLength)
    {
      if (string.IsNullOrEmpty(nullableValue)) { return string.Empty; }
      return ((nullableValue.Length > maxLength) ? nullableValue.Substring(0, maxLength) : nullableValue);
    }

  }

}
