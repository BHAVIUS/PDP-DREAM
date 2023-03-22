// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// PDP dream software APPlication STATUS
public static class PdpAppStatus
{
  // TODO: create zero-parameter constructors for these properties from classes

  // PDP CodeConfig (PDPCC)
  public static PdpCodeConfig PDPCC { get; set; } // = new PdpCodeConfig();

  // PDP SiteSettings (PDPSS)
  public static PdpSiteSettings PDPSS { get; set; } // = new PdpSiteSettings();

  // NPDS ServerDefaults (NPDSSD)
  public static NpdsServerDefaults NPDSSD { get; set; } // = new NpdsServerDefaults();


  public static string GetNamespace(this object thing)
  {
    return thing.GetType().Namespace;
  }

  // for use when called from a SQL related context
  // TODO: reconcile/merge ? with QebSql error messages
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

  //public static void CatchNullObject(this object? value)
  //{
  //  if (value == null)
  //  { throw new ArgumentNullException(value?.ToString(), "Object cannot be null."); }
  //  else if ((value.GetType() == typeof(string)) && (string.IsNullOrWhiteSpace((string)value)))
  //  { throw new ArgumentNullException(value.ToString(), "String cannot be null or whitespace."); }
  //}

  // TODO: migrate references where possible to CatchNullEmptyGuid or CatchNullEmptyString 
  public static void CatchNullObject(this object? theValue, string variableName, string methodName, string className = "")
  {
    if (theValue == null)
    {
      ThrowNullEmptyException(variableName, methodName, className);
    }
  }
  public static void CatchNullEmptyGuid(this Guid? theValue, string variableName, string methodName, string className = "")
  {
    if (theValue.IsNullOrEmpty())
    {
      ThrowNullEmptyException(variableName, methodName, className);
    }
  }
  public static void CatchNullEmptyString(this string? theValue, string variableName, string methodName, string className = "")
  {
    if (string.IsNullOrEmpty(theValue))
    {
      ThrowNullEmptyException(variableName, methodName, className);
    }
  }
  public static void CatchNullWhiteString(this string? theValue, string variableName, string methodName, string className = "")
  {
    if (string.IsNullOrWhiteSpace(theValue))
    {
      ThrowNullEmptyException(variableName, methodName, className);
    }
  }
  public static void ThrowNullEmptyException(string variableName, string methodName, string className)
  {
    var errorMessage = $"Null or empty variable {variableName} in method {methodName}";
    if (!string.IsNullOrEmpty(className)) { errorMessage += $" of class {className}"; }
    throw new NullReferenceException(errorMessage);
  }

  // TODO: implement use of [CallerArgumentExpression] 
  public static void PdpDebugMessage(this object thing, string namThing = "")
  {
    Debug.WriteLine($"Name = '{namThing}', Value = '{thing}', Type = '{thing.GetType().Name}';");
  }

} // end class

// end file