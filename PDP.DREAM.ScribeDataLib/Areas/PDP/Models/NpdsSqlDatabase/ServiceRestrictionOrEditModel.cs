﻿// ServiceRestrictionOrEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class RestrictionOrEditModel : NexusEditModelBase
  {
    public RestrictionOrEditModel()
    {
      itemXnam = NpdsConst.ServiceRestrictionOrItemXnam;
    }

    public Guid? RestrictionAndGuidRef { get; set; } = null;
     public Guid? RestrictionOrGuidKey { get; set; } = null;
    public byte RestrictionOrIndex { get; set; } = 0;
    public byte RestrictionOrPriority { get; set; } = 0;
    public string? Restriction { get; set; } = string.Empty;
    public bool IsWordPhrase { get; set; } = false;
    public bool IsConceptLabel { get; set; } = false;
  }

}