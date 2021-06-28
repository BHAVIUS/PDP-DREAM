using System;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class ConfigManager
  {

    private static IConfiguration? webAppConfig;

    public static void Initialize(IConfiguration? config)
    {
      if (config == null)
      { throw new ArgumentNullException(nameof(config), "config cannot be null in ConfigManager"); }
      webAppConfig = config;
    }

    public const string WebAppSectionName = "ApplicationSettings";
    public const string DBConnSectionName = "ConnectionStrings";

    // for use with Web Application Settings
    public static string ParseAppStringSetting(Enum keynam)
    { return ParseAppStringSetting(keynam.ToString()); }

    public static string ParseAppStringSetting(string keynam)
    {
      var keyval = webAppConfig?.GetSection(WebAppSectionName)[keynam];
      keyval = (keyval ?? string.Empty);
      return keyval;
    }

    public static bool ParseAppBooleanSetting(Enum keynam, bool defval = true)
    { return ParseAppBooleanSetting(keynam.ToString(), defval); }

    public static bool ParseAppBooleanSetting(string? keynam, bool defval = true)
    {
      string? keyval = null;
      if (!string.IsNullOrWhiteSpace(keynam))
      { keyval = webAppConfig?.GetSection(WebAppSectionName)[keynam].ToLower(); }
      if ((keyval == null) || string.IsNullOrWhiteSpace(keyval)) { keyval = defval.ToString().ToLower(); }
      if ((keyval != "true") && (keyval != "false")) { keyval = defval.ToString().ToLower(); }
      bool value;
      bool.TryParse(keyval, out value);
      return value;
    }

    // for use with DataBase Connection Strings
    public static string ParseAppDBConnString(Enum keynam)
    { return ParseAppDBConnString(keynam.ToString()); }

    public static string ParseAppDBConnString(string keynam)
    {
      var keyval = webAppConfig?.GetSection(DBConnSectionName)[keynam]; ;
      if (!string.IsNullOrWhiteSpace(keyval))
      {
        // TODO: parsing here assumes a SQL Server connection string
        keyval = (new SqlConnectionStringBuilder(keyval)).ToString();
      }
      keyval = (keyval ?? string.Empty);
      return keyval;
    }

  }

}
