// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class PdpConfigManager
{
  protected string pdpCodeDirpath; // compare PdpCodePrgroot in PdpCodeConfig
  protected static ConfigurationManager pdpCodeCnfgMngr;
  protected static IConfigurationRoot pdpSiteConfig;

  protected PdpConfigManager(bool resetDefault = false)
  {
    pdpCodeCnfgMngr = new ConfigurationManager();
    if (resetDefault) { pdpCodeDirpath = Environment.CurrentDirectory; }
    else { pdpCodeDirpath = PDPCC.PdpCodePrgroot; }
#if DEBUG
    pdpCodeCnfgMngr.CatchNullObject(nameof(pdpCodeCnfgMngr), nameof(PdpConfigManager));
    pdpCodeDirpath.CatchNullEmptyString(nameof(pdpCodeDirpath), nameof(PdpConfigManager));
#endif
  }
  protected void Configure()
  {
    IConfigurationBuilder builder = new ConfigurationBuilder();
    string envir = Environment.GetEnvironmentVariable(MSASPNETENVVAR);
    switch (envir)
    {
      // ATTN: always check environment variable and json file on production server!!!
      // or any machine where IIS web server is running when publish to that server
      // else if environment variable not set then will default to throwing exception!!!
      case "Production":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", pdpCodeDirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Production.json", pdpCodeDirpath), false, true);
        break;
      case "Staging":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", pdpCodeDirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Staging.json", pdpCodeDirpath), true, true);
        break;
      case "Development":
        builder.AddJsonFile(Path.GetFullPath("appsettings.json", pdpCodeDirpath), true, true);
        builder.AddJsonFile(Path.GetFullPath("appsettings.Development.json", pdpCodeDirpath), true, true);
        break;
      case XunitEnvirname:
        builder.AddJsonFile(Path.GetFullPath($"{XunitEnvirname}.json", pdpCodeDirpath), true, true);
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

  public string PdpCodeDirpath
  {
    get { return pdpCodeDirpath; }
  }

  // for use with DataBase Connection Strings

  public const string DbConnSectionName = "ConnectionStrings";

  public static string ParseAppDbConnString(Enum keynam, string defval = ESS)
  { return ParseAppDbConnString(keynam.ToString(), defval); }

  public static string ParseAppDbConnString(string keynam, string defval = ESS)
  {
    var keyval = PdpSiteConfig.GetSection(DbConnSectionName)[keynam];
    if (string.IsNullOrWhiteSpace(keyval))
    { keyval = defval; }
    if (!string.IsNullOrWhiteSpace(keyval))
    { keyval = QebSqlLinq.ParseSqlDbcString(keyval); }
    return keyval;
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

  public static string ParseAppStringSetting(string keynam, string defval = ESS)
  {
    var keyval = PdpSiteConfig.GetSection(WebAppSectionName)[keynam];
    if (string.IsNullOrEmpty(keyval)) { keyval = defval; }
    return keyval;
  }

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