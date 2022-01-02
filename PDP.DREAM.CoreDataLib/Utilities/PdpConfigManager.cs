// PdpConfigManager.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpConfigManager
{
  public static IConfiguration? AppConfig;

  public static void Initialize(IConfiguration? config)
  {
    if (config == null)
    { throw new ArgumentNullException(nameof(config), "cannot be null in " + nameof(PdpConfigManager)); }
    AppConfig = config;
  }

  public const string WebAppSectionName = "ApplicationSettings";
  public const string DbConnSectionName = "ConnectionStrings";

  // for use with Web Application Settings
  public static string ParseAppStringSetting(Enum keynam)
  { return ParseAppStringSetting(keynam.ToString()); }

  public static string ParseAppStringSetting(string keynam)
  {
    var keyval = AppConfig?.GetSection(WebAppSectionName)[keynam];
    keyval = (keyval ?? string.Empty);
    return keyval;
  }

  public static bool ParseAppBooleanSetting(Enum keynam, bool defval = true)
  { return ParseAppBooleanSetting(keynam.ToString(), defval); }

  public static bool ParseAppBooleanSetting(string? keynam, bool defval = true)
  {
    string? keyval = null;
    if (!string.IsNullOrWhiteSpace(keynam))
    { keyval = AppConfig?.GetSection(WebAppSectionName)[keynam].ToLower(); }
    if ((keyval == null) || string.IsNullOrWhiteSpace(keyval)) { keyval = defval.ToString().ToLower(); }
    if ((keyval != "true") && (keyval != "false")) { keyval = defval.ToString().ToLower(); }
    bool value;
    bool.TryParse(keyval, out value);
    return value;
  }

  // for use with DataBase Connection Strings
  public static string ParseAppDbConnString(Enum keynam)
  { return ParseAppDbConnString(keynam.ToString()); }

  public static string ParseAppDbConnString(string keynam)
  {
    var keyval = AppConfig?.GetSection(DbConnSectionName)[keynam]; ;
    if (!string.IsNullOrWhiteSpace(keyval))
    {
      // parsing here assumes a SQL Server connection string
      var strBldr = new SqlConnectionStringBuilder(keyval);
      var isEncrypted = strBldr.Encrypt;
      keyval = strBldr.ToString();
    }
    keyval = (keyval ?? string.Empty);
    return keyval;
  }

}
