// NexusResrepEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Models;

public class NexusResrepEditModel : NexusViewModelBase
{
  public NexusResrepEditModel()
  {
    itemXnam = PdpAppConst.NexusResrepItemXnam;
  }

  // Display and UIHint necessary for Telerik Grid EditForm and PopUp modes but not for InLine mode

  public bool AgentIsCreator { get { return (CreatedByAgentGuid == AgentGuid); } }
  public bool AgentIsUpdater { get { return (UpdatedByAgentGuid == AgentGuid); } }
  public bool AgentIsManager { get { return (ManagedByAgentGuid == AgentGuid); } }

  public string? RequestOrRelease
  {
    get { return ((AgentIsManager) ? "Release" : "Request"); }
  }
  public string? ReqRelButtonHtml
  {
    get { return $"<a href='#' id='{RRRecordGuid}' class='k-button' onclick='OnRequestReleaseRecord(this)'>{RequestOrRelease}</a>"; }
  }
  public string? AgentRequestHtml
  {
    get { return ((AgentIsManager) ? RequestOrRelease : ReqRelButtonHtml); }
  }
  public string? AuthorReleaseHtml
  {
    get { return ((AgentIsManager) ? ReqRelButtonHtml : RequestOrRelease); }
  }


} // end class

// end file