// ResrepMergeToSameModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.ComponentModel;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
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