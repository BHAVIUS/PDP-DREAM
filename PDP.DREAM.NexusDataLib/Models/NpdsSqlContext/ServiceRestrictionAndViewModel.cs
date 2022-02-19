// ServiceRestrictionAndViewModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.NexusDataLib.Models
{
  public class RestrictionAndViewModel : NexusViewModelBase
  {
    public Guid? RestrictionAndGuidKey { get; set; } = null;
    public byte RestrictionAndIndex { get; set; } = 0;
    public byte RestrictionAndPriority { get; set; } = 0;
    public string? RestrictionName { get; set; } = string.Empty;
    public bool RestrictionIsSufficient { get; set; } = false;
  }

}