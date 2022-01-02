// ResrepSplitToDifferentModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel;

namespace PDP.DREAM.ScribeDataLib.Models
{
  public class ResrepSplitToDifferentModel : NexusEditModelBase
  {
    public ResrepSplitToDifferentModel() { }

    [DisplayName("Handle of Record To Split")]
    public string? RecordHandleToSplit { get; set; } = string.Empty;

  }

}