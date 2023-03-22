// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class PdpConfigManager
{
  protected static string pdpCodeDirpath;
  protected static ConfigurationManager pdpCodeCnfgMngr;
  protected static IConfigurationRoot pdpSiteConfig;

  protected PdpConfigManager(bool setDefault = false)
  {
    pdpCodeCnfgMngr = new ConfigurationManager();
    if (setDefault) { Configure(Environment.CurrentDirectory); }
  }
  protected void Configure(string basedirpath)
  {
    pdpCodeDirpath = basedirpath;
    IConfigurationBuilder builder = new ConfigurationBuilder();
    string envir = Environment.GetEnvironmentVariable(MSASPNETENVVAR);
    switch (envir)
    {
      // ATTN: always check environment variable and json file on production server!!!
      // or any machine where IIS web server is running when publish to that server
      // else if environment variable not set then will default to throwing exception!!!
      case "Production":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", basedirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Production.json", basedirpath), false, true);
        break;
      case "Staging":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", basedirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Staging.json", basedirpath), false, true);
        break;
      case "Development":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", basedirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Development.json", basedirpath), false, true);
        break;
      case XunitEnvirname:
        builder.AddJsonFile(Path.GetFullPath($"{XunitEnvirname}.json", basedirpath), false, true);
        break;
      default: // should handle other cases including null if not set in current environment
        throw new UnauthorizedAccessException();
    }
    pdpSiteConfig = builder.Build();
  }
  public static ConfigurationManager PdpSiteCnfgMngr
  {
    get {
      if (pdpCodeCnfgMngr == null)
      { throw new ArgumentNullException("pdpCnfgMngr cannot be null in getter for " + nameof(PdpSiteCnfgMngr)); }
      return pdpCodeCnfgMngr;
    }
  }
  public static IConfigurationRoot PdpSiteConfig
  {
    get {
      if (pdpSiteConfig == null)
      { throw new ArgumentNullException("pdpCnfgMngr cannot be null in getter for " + nameof(PdpSiteConfig)); }
      return pdpSiteConfig;
    }
  }

  public string PdpSiteDirpath
  {
    get { return pdpCodeDirpath; }
  }

  public void AddConfiguration(IConfiguration value)
  {
    if (value == null)
    { throw new ArgumentNullException("config value cannot be null in " + nameof(AddConfiguration)); }
    pdpCodeCnfgMngr.AddConfiguration(value);
  }

  private string strPipe = "";
  public void AddConnection(string strIn, SqlPipeStringType strInTyp = SqlPipeStringType.WebConfigNameOnly)
  {
    // do not allow default null or empty string for strIn
    if (string.IsNullOrEmpty(strIn))
    {
      { throw new ArgumentNullException("config value cannot be null in " + nameof(AddConnection)); }
    }
    try
    {
      switch (strInTyp)
      {
        case SqlPipeStringType.WebConfigNameOnly:
          strPipe = ParseConnectionString(ParseAppDbConnString(strIn, ""));
          break;
        case SqlPipeStringType.AppConfigNameOnly:
          strPipe = ParseConnectionString(ParseAppStringSetting(strIn, ""));
          break;
        case SqlPipeStringType.ConnectionString:
          strPipe = ParseConnectionString(ParseAppDbConnString(strIn, ""));
          break;
      }
    }
    catch (Exception exc)
    {
      throw new Exception("PdpConnectManager error with connection string", exc);
    }
  }

  // for use with DataBase Connection Strings

  public const string DbConnSectionName = "ConnectionStrings";

  public static string ParseAppDbConnString(Enum keynam)
  { return ParseAppDbConnString(keynam.ToString(), ""); }

  public static string ParseAppDbConnString(string keynam, string defval)
  {
    var keyval = PdpSiteConfig.GetSection(DbConnSectionName)[keynam];
    if (string.IsNullOrWhiteSpace(keyval))
    { keyval = defval; }
    if (!string.IsNullOrWhiteSpace(keyval))
    { keyval = QebSql.ParseSqlDbcString(keyval); }
    return keyval;
  }

  // term "pipe" and name SqlPipe used for the SqlConnection
  public enum SqlPipeStringType : int
  {
    WebConfigNameOnly = 1,
    AppConfigNameOnly = 2,
    ConnectionString = 3
  }


  // SqlConnection object

  private bool isEncrypted = false;
  public string ParseConnectionString(string value)
  {
    var scsb = new SqlConnectionStringBuilder(value);
    isEncrypted = scsb.Encrypt;
    return scsb.ToString();
  }

  // for use with Web Application Settings
  public const string WebAppSectionName = "ApplicationSettings";

  public static string ParseAppStringSetting(Enum keynam, string defval = "")
  { return ParseAppStringSetting(keynam.ToString(), defval); }

  public static string ParseAppStringSetting(string keynam, string defval)
  {
    var keyval = PdpSiteConfig.GetSection(WebAppSectionName)[keynam];
    if (string.IsNullOrEmpty(keyval)) { keyval = defval; }
    return keyval;
  }

  public static bool ParseAppBooleanSetting(Enum keynam, bool defval = true)
  { return ParseAppBooleanSetting(keynam.ToString(), defval); }

  public static bool ParseAppBooleanSetting(string? keynam, bool defval = true)
  {
    string? keyval = null;
    if (!string.IsNullOrWhiteSpace(keynam))
    { keyval = PdpSiteConfig.GetSection(WebAppSectionName)[keynam].ToLower(); }
    if ((keyval == null) || string.IsNullOrWhiteSpace(keyval)) { keyval = defval.ToString().ToLower(); }
    if ((keyval != "true") && (keyval != "false")) { keyval = defval.ToString().ToLower(); }
    bool value;
    bool.TryParse(keyval, out value);
    return value;
  }

} // end class

// end file