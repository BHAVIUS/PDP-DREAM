// SupportingLabelEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class SupportingLabelEditModel : SupportingLabelViewModel
{
  public SupportingLabelEditModel()
  {
    itemXnam = PdpAppConst.SupportingLabelItemXnam;
  }

  //public string? SupportingLabel { get; set; } = string.Empty;

} // end class

// end file