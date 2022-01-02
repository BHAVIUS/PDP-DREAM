// PdpPredicateBuilder.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;
using System.Linq.Expressions;

namespace PDP.DREAM.CoreDataLib.Types
{
  public static class PdpPredicateBuilder
  {
    // initialize predicate as true
    public static Expression<Func<T, bool>> True<T>() { return (f) => true; }
    // initialize predicate as false
    public static Expression<Func<T, bool>> False<T>() { return (f) => false; }

    // OR predicate
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
      var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
      return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
    }

    // AND predicate
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
      var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
      return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    }
  } // class

} // namespace
