// ResrepMergeToSameModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel;

namespace PDP.DREAM.ScribeDataLib.Models
{
  public class ResrepMergeToSameModel : NexusEditModelBase
  {
    public ResrepMergeToSameModel() { }

    [DisplayName("Handle of Record To Retain")]
    public string? RecordHandleToRetain { get; set; } = string.Empty;

    [DisplayName("Handle of Record To Delete")]
    public string? RecordHandleToDelete { get; set; } = string.Empty;

  }

}