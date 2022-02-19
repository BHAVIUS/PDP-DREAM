// NexusSnapshotViewModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models;

public class NexusSnapshotViewModel : NexusViewModelBase
{
  public NexusSnapshotViewModel()
  {
    itemXnam = NpdsConst.NexusResrepItemXnam + "Archived";
  }

  public string NexusSnapshot { get; set; } = string.Empty;

}

