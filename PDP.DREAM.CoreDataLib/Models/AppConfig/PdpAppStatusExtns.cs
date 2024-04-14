// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: this static partial class file contains extension methods

public static partial class PdpAppStatus
{
  public static string GetNamespace(this object thing)
  {
    return thing.GetType().Namespace;
  }

  // ATTN: CatchNull* methods throw exceptions

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

  // TODO: implement use of [CallerArgumentExpression] 
  public static void PdpDebugMessage(this object thing, string namThing = "")
  {
    Debug.WriteLine($"Name = '{namThing}', Value = '{thing}', Type = '{thing.GetType().Name}';");
  }

} // end class

// end file