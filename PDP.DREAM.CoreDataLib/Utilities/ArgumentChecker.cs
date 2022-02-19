// ArgumentChecker.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Utilities;

// TODO: reconcile with other guard utilities and eliminate redundancy
public static class ArgumentChecker
{
  public static void CatchNull(object? argValue)
  {
    if (argValue == null)
    { throw new ArgumentNullException(argValue?.ToString(), "Object argument cannot be null."); }
    else if ((argValue.GetType() == typeof(string)) && (string.IsNullOrWhiteSpace((string)argValue)))
    { throw new ArgumentNullException(argValue.ToString(), "String argument cannot be null or whitespace."); }
  }

  public static void CatchNullOrWhite(string argValue, string argName = "")
  {
    if ((argValue == null) || (string.IsNullOrWhiteSpace(argValue)))
    {
      if ((string.IsNullOrWhiteSpace(argName)) && (argValue != null)) { argName = argValue.ToString(); }
      throw new ArgumentNullException(argName, "String argument cannot be null or whitespace.");
    }
  }

  public static void ArgNotNull(object value, string argumentName)
  {
    if (value == null)
    {
      throw new ArgumentNullException(argumentName);
    }
  }

  public static void ArgNotNullOrEmptyString(string value, string argumentName)
  {
    ArgNotNull(value, argumentName);

    if (value.Length == 0)
    {
      throw new ArgumentNullException(argumentName);
    }
  }

}
