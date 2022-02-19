// FairMetricViewModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models
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