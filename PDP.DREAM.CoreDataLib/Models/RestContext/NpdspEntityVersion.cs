// PrcEntityVersion.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // default values

  // requested values

  public string EntityVersionReqst
  {
    set
    {
      reqEntityVersion = value;
      // TODO: consider alternative regex check on version such as alphanumeric only or with . and -
      if (!string.IsNullOrEmpty(reqEntityVersion) && Regex.IsMatch(reqEntityVersion, PdpAppConst.RegexSafeGeneric))
      {
        entityVersion = reqEntityVersion;
      }
      else
      {
        entityVersion = string.Empty;
      }
    }
    get { return reqEntityVersion; }
  }
  private string reqEntityVersion = string.Empty;

  // validated values

  public string EntityVersion
  {
    set { entityVersion = value; }
    get { return entityVersion; }
  }
  private string entityVersion = string.Empty;

}

