// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  public string? QebPassWord { get; set; } = string.Empty;
  public string? QebUserName { get; set; } = string.Empty;
  public string? QebUserNameDisplayed { get; set; } = string.Empty;


  // IDENTIFICATION/AUTHENTICATION

  // string-ids are "Key"s while guid-ids are "Guid"s
  public string? QebUserKey
  {
    get { return qebUsrKey; }
    set { qebUsrKey = value; QebUserGuid = PdpGuid.ParseToNonNullable(qebUsrKey); }
  }
  private string? qebUsrKey = string.Empty;

  public string? QebAgentKey
  {
    get { return qebAgtKey; }
    set { qebAgtKey = value; QebAgentGuid = PdpGuid.ParseToNonNullable(qebAgtKey); }
  }
  private string? qebAgtKey = string.Empty;

  public string? QebAgentInfosetKey
  {
    get { return qebAifsKey; }
    set { qebAifsKey = value; QebAgentInfosetGuid = PdpGuid.ParseToNonNullable(qebAifsKey); }
  }
  private string? qebAifsKey = string.Empty;

  public string? QebSessionKey
  {
    get { return qebSssnKey; }
    set { qebSssnKey = value; QebSessionGuid = PdpGuid.ParseToNonNullable(qebSssnKey); }
  }
  private string? qebSssnKey = string.Empty;

  public Guid QebApplicationGuid { get; } = PDPSS.AppSecureUiaaGuid;

  // user agent session guids for service APIs

  public Guid QebUserGuid { get; set; }

  public Guid QebAgentGuid { get; set; }

  public Guid QebAgentInfosetGuid { get; set; }

  public Guid QebSessionGuid { get; set; }

  public bool QebSessionValueIsRequired { get; set; } = false;

} // end class

// end file