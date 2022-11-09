// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// PDP dream software APPlication STATUS
public static class PdpAppStatus
{
  // PDP CodeConfig (PDPCC)
  public static PdpCodeConfig PDPCC { get; set; }

  // PDP SiteSettings (PDPSS)
  public static PdpSiteSettings PDPSS { get; set; }

  // NPDS ServerDefaults (NPDSSD)
  public static NpdsServerDefaults NPDSSD { get; set; }


  public static string GetNamespace(this object thing)
  {
    return thing.GetType().Namespace;
  }

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


  // CatchNull methods throw exceptions
  // arguments from calling methods are passed to parameters declared in receiving methods

  public static void CatchNullObject(this object? value)
  {
    if (value == null)
    { throw new ArgumentNullException(value?.ToString(), "Object cannot be null."); }
    else if ((value.GetType() == typeof(string)) && (string.IsNullOrWhiteSpace((string)value)))
    { throw new ArgumentNullException(value.ToString(), "String cannot be null or whitespace."); }
  }
  public static void CatchNullObject(this object? value, string name)
  {
    if (value == null) { throw new ArgumentNullException(name); }
  }
  public static void CatchNullObject(this object? theValue, string variableName, string methodName, string className = "")
  {
    if (theValue == null)
    {
      var errorMessage = $"null variable {variableName} in method {methodName}";
      if (!string.IsNullOrEmpty(className)) { errorMessage += $" of class {className}"; }
      throw new NullReferenceException(errorMessage);
    }
  }
  public static void CatchNullEmptyString(this string? value, string name = "")
  {
    value.CatchNullObject(name);
    if (value.Length == 0)
    {
      throw new ArgumentNullException(name, "String cannot be null or empty.");
    }
  }
  public static void CatchNullWhiteString(this string? value, string name = "")
  {
    value.CatchNullObject(name);
    if (string.IsNullOrWhiteSpace(value))
    {
      if ((string.IsNullOrWhiteSpace(name)) && (value != null)) { name = value.ToString(); }
      throw new ArgumentNullException(name, "String cannot be null or whitespace.");
    }
  }

} // end class

// end file