﻿// Original C# source from http://www.albahari.com/nutshell/predicatebuilder.aspx

using System;
using System.Linq;
using System.Linq.Expressions;

namespace PDP.DREAM.NpdsCoreLib.Types
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
  }
}