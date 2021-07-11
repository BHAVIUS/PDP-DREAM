// ServiceRestrictionAndViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
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