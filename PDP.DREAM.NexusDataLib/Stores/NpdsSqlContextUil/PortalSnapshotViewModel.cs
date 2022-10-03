// PortalSnapshotViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models;

public class PortalSnapshotViewModel : NexusViewModelBase
{
  public PortalSnapshotViewModel()
  {
    itemXnam = PdpAppConst.PortalResrepItemXnam + "Archived";
  }

  public string PortalSnapshot { get; set; } = string.Empty;

}

