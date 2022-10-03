// PdpPredicateBuilder.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PDP.DREAM.CoreDataLib.Types;

public static class PdpPredicateBuilder
{
  // TRUE
  public static Expression<Func<T, bool>> True<T>() { return (f) => true; }

  // FALSE
  public static Expression<Func<T, bool>> False<T>() { return (f) => false; }

  // OR
  public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
  {
    var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
    return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
  }

  // AND
  public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
  {
    var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
    return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
  }

  // NONE
  public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
  {
    var target = !source.Any(predicate);
    return target;
  }

}
