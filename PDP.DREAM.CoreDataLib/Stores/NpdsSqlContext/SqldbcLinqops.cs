// SqldbContext.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
  {
    var target = !source.Any(predicate);
    return target;
  }
}
