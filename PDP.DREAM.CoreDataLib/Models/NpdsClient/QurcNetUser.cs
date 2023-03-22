// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserRestContext
{
  // IDENTIFICATION/AUTHENTICATION

  // string-ids are "Key"s while guid-ids are "Guid"s
  // string-ids in QURC, guid-ids in NpdsClient

  private string? qebUsrKey = string.Empty;
  public string? ClientUserKey
  {
    get { return qebUsrKey; }
    set { qebUsrKey = value; ClientUserGuid = PdpGuid.ParseToNonNullable(qebUsrKey); }
  }

  private string? qebAgtKey = string.Empty;
  public string? ClientAgentKey
  {
    get { return qebAgtKey; }
    set { qebAgtKey = value; ClientAgentGuid = PdpGuid.ParseToNonNullable(qebAgtKey); }
  }

  private string? qebAifsKey = string.Empty;
  public string? ClientAgentInfosetKey
  {
    get { return qebAifsKey; }
    set { qebAifsKey = value; ClientAgentInfosetGuid = PdpGuid.ParseToNonNullable(qebAifsKey); }
  }

  private string? qebSssnKey = string.Empty;
  public string? ClientSessionKey
  {
    get { return qebSssnKey; }
    set { qebSssnKey = value; ClientSessionGuid = PdpGuid.ParseToNonNullable(qebSssnKey); }
  }

} // end class

// end file