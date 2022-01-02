// ListExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpListExtensions
{
  public static void AppendList<T>(this IList<T> firstList, IList<T> secondList)
  {
    foreach (var item in secondList)
    {
      firstList.Add(item);
    }
  }

}
