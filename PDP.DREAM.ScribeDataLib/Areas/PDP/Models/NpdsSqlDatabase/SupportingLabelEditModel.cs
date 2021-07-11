// SupportingLabelEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class SupportingLabelEditModel : NexusEditModelBase
  {
    public SupportingLabelEditModel()
    {
      itemXnam = NpdsConst.SupportingLabelItemXnam;
    }

    public string? SupportingLabel { get; set; }

  }

}
