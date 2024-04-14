// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class AccessServiceRequestUxm : ResrepRootModelBase
{
  public AccessServiceRequestUxm() { InitUxm(); }
  public AccessServiceRequestUxm(NpdsRecordAccess nra) : base(nra) { InitUxm(); }

  // TODO: consider consolidate with AccessResrepRequest
  protected void InitUxm()
  {
    itemXnam = "AccessServiceRequest";
  }

  public Guid? AccessRequestedForAgentGuid { get; set; } = null;
  public string? AccessRequestedForAgentAlias { get; set; } = ESS;
  public Guid? AccessAllowedByAgentGuid { get; set; } = null;
  public string? AccessAllowedByAgentAlias { get; set; } = ESS;
  public Guid? AccessDeniedByAgentGuid { get; set; } = null;
  public string? AccessDeniedByAgentAlias { get; set; } = ESS;

  public bool AccessIsAllowed { get; set; }
  public bool AccessIsDenied { get; set; }

}
