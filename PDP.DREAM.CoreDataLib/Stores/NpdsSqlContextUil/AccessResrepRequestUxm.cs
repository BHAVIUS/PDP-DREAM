// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class AccessResrepRequestUxm : ResrepRootModelBase
{
  public AccessResrepRequestUxm() { InitUxm(); }
  public AccessResrepRequestUxm(NpdsRecordAccess nra) : base(nra) { InitUxm(); }

  // TODO: consider consolidate with AccessServiceRequest
  protected void InitUxm()
  {
    itemXnam = "AccessResrepRequest";
  }

  public string? RoleAccessRequested { get; set; } = ESS;
  public Guid? RequestedForAgentGuid { get; set; } = null;
  public string? RequestedForAgentAlias { get; set; } = ESS;
  public Guid? AllowedByAgentGuid { get; set; } = null;
  public string? AllowedByAgentAlias { get; set; } = ESS;
  public Guid? DeniedByAgentGuid { get; set; } = null;
  public string? DeniedByAgentAlias { get; set; } = ESS;

  public bool AccessAsAuthor { get; set; }
  public bool AccessAsReviewer { get; set; }
  public bool AccessAsEditor { get; set; }
  public bool AccessForService { get; set; }
  public bool AccessIsAllowed { get; set; }
  public bool AccessIsDenied { get; set; }

}
