﻿// DistributionEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class DistributionEditModel : DistributionViewModel
{
  public DistributionEditModel()
  {
    itemXnam = PdpAppConst.DistributionItemXnam;
  }

  //public string? Distribution { get; set; } = string.Empty;

  //public string? Distribution128
  //{
  //  get { return Distribution.ToPartialPhrase(128); }
  //}

} // end class

// end file