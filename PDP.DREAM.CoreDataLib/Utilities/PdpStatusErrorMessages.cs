// EncodedStringBuilder.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.Data.SqlClient;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpStatus
{
  // for use when called from a SQL related context
  public static string SqlErrorMessage(SqlException exc)
  {
    var msgOuter = exc?.Message ?? string.Empty;
    var msgInner = exc?.InnerException?.Message ?? string.Empty;
    var sqlError = $"SQL Error: ExOuter '{msgOuter}', ExInner '{msgInner}'";
    return sqlError;
  }

  // for use when called from a LINQ related context
  public static string LinqErrorMessage(Exception exc)
  {
    var msgOuter = exc?.Message ?? string.Empty;
    var msgInner = exc?.InnerException?.Message ?? string.Empty;
    var linqError = $"LINQ Error: ExOuter '{msgOuter}', ExInner '{msgInner}'";
    return linqError;
  }

} // class
