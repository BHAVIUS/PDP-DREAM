﻿using System;

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