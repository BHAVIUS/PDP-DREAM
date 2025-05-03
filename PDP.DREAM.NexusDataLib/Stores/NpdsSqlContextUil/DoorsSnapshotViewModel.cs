// DoorsSnapshotViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models;

public class DoorsSnapshotViewModel : NexusViewModelBase
{
  public DoorsSnapshotViewModel()
  {
    itemXnam = PdpAppConst.DoorsResrepItemXnam + "Archived";
  }

  public string DoorsSnapshot { get; set; } = string.Empty;

}

