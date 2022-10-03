// ResrepSplitToDifferentModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class ResrepSplitToDifferentModel : NexusViewModelBase
{
  public ResrepSplitToDifferentModel() { }

  [DisplayName("Handle of Record To Split")]
  public string? RecordHandleToSplit { get; set; } = string.Empty;

}

