// ResrepSplitToDifferentModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.ComponentModel;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class ResrepSplitToDifferentModel : NexusEditModelBase
  {
    public ResrepSplitToDifferentModel() { }

    [DisplayName("Handle of Record To Split")]
    public string? RecordHandleToSplit { get; set; } = string.Empty;

  }

}