// ListExtensions.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpListExtensions
{
  public static IList<T>? AppendList<T>(this IList<T>? firstList, IList<T>? secondList)
  {
    if (secondList != null)
    {
      if (firstList == null) { firstList = secondList; }
      else { foreach (var item in secondList) { firstList.Add(item); } }
    }
    return firstList;
  }

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

}
