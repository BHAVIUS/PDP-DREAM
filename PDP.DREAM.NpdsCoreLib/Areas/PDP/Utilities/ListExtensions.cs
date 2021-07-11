// ListExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Collections.Generic;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class ListExtensions
  {
    public static void AppendList<T>(this IList<T> firstList, IList<T> secondList)
    {
      foreach (var item in secondList)
      {
        firstList.Add(item);
      }
    }

  }

}