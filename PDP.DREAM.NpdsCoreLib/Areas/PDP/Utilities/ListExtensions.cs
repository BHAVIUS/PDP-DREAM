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