// PdpNonnullString.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

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

