﻿// DescriptionEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class DescriptionEditModel : DescriptionViewModel
{
  public DescriptionEditModel()
  {
    itemXnam = PdpAppConst.DescriptionItemXnam;
  }

  //public string? Description { get; set; } = string.Empty;

  //public string? Description128
  //{
  //  get { return Description.ToPartialPhrase(128); }
  //}

} // end class

// end file