// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class PdpConfigManager
{
  protected static ConfigurationManager pdpSiteCnfgMngr;
  protected static IConfigurationRoot pdpSiteConfig;
  protected PdpConfigManager(string basedirpath)
  {
    pdpSiteCnfgMngr = new ConfigurationManager();
    IConfigurationBuilder builder = new ConfigurationBuilder();
    var testJsonFile = Path.GetFullPath("testJsonFile.json", basedirpath);
    string envir = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    switch (envir)
    {
      case "Development":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", basedirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Development.json", basedirpath), false, true);
        break;
      case "Staging":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", basedirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Staging.json", basedirpath), false, true);
        break;
      case "Production":
      default:
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", basedirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Production.json", basedirpath), false, true);
        break;
    }
    pdpSiteConfig = builder.Build();
  }
  public static ConfigurationManager PdpSiteCnfgMngr
  {
    get {
      if (pdpSiteCnfgMngr == null)
      { throw new ArgumentNullException("pdpCnfgMngr cannot be null in getter for " + nameof(PdpSiteCnfgMngr)); }
      return pdpSiteCnfgMngr;
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
  public void AddConfiguration(IConfiguration value)
  {
    if (value == null)
    { throw new ArgumentNullException("config value cannot be null in " + nameof(AddConfiguration)); }
    pdpSiteCnfgMngr.AddConfiguration(value);
  }

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

  // SqlConnection string
  private string strPipe = "";
  public string AppSqlPipeString
  {
    get {
      return strPipe;
    }
    set {
      strPipe = ParseConnectionString(value);
    }
  }

  private bool isEncrypted = false;
  public bool AppSqlPipeIsEncrypted
  {
    get { return isEncrypted; }
  }

  // SqlConnection object
  private SqlConnection? sqlPipe;
  public SqlConnection? AppSqlPipe
  {
    get { return sqlPipe; }
  }

  private string sqlStatus = "";
  public string AppSqlPipeStatus
  {
    get { return sqlStatus; }
  }

  private string sqlErrors = "";
  public string AppSqlPipeErrors
  {
    get { return sqlErrors; }
  }

  public string ParseConnectionString(string value)
  {
    var scsb = new SqlConnectionStringBuilder(value);
    isEncrypted = scsb.Encrypt;
    return scsb.ToString();
  }

  public void Connect()
  {
    if (sqlPipe == null)
    {
      sqlPipe = new SqlConnection(strPipe);
    }
    if (sqlPipe.State == ConnectionState.Closed)
    {
      try
      {
        sqlPipe.Open();
        sqlStatus = "Connection Opened.";
      }
      catch (SqlException sqlExc)
      {
        StringBuilder sbErrors = new StringBuilder("");
        foreach (SqlError sqlErr in sqlExc.Errors)
        {
          sbErrors.AppendLine(sqlErr.ToString());
          sbErrors.AppendLine(string.Format("Class: {0}; Error: {1}; Line: {2}.<br>", sqlErr.Class, sqlErr.Number, sqlErr.LineNumber));
          sbErrors.AppendLine(string.Format("Reported by {0} while connected to {1}.</p>", sqlErr.Source, sqlErr.Server));
        }
        sqlErrors = sbErrors.ToString();
        sqlStatus = "Connection Not Opened.";
      }
    }
    else if (sqlPipe.State != ConnectionState.Open)
    {
      sqlStatus = sqlPipe.State.ToString();
    }
  }

  public void Disconnect()
  {
    if (sqlPipe != null)
    {
      if (!(sqlPipe.State == ConnectionState.Closed))
      {
        sqlPipe.Close();
      }
      //TODO: answer this question re Dispose
      //				sqlCon.Dispose()
      sqlPipe = null;
    }
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