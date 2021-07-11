// FairMetricViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class FairMetricViewModel : NexusViewModelBase
  {
    public FairMetricViewModel()
    {
      itemXnam = NpdsConst.FairMetricItemXnam;
    }

    public short MInvalidOldClaim { get; set; }
    public short QValidOldClaim { get; set; }
    public short PInvalidNewClaim { get; set; }
    public short NValidNewClaim { get; set; }

    public float FAIR1 { get; set; }
    public float FAIR2 { get; set; }
    public float FAIR3 { get; set; }
    public float FAIR4 { get; set; }

  }

}