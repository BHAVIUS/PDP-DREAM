// ServiceRestrictionOrEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class RestrictionOrEditModel : NexusEditModelBase
{
  public RestrictionOrEditModel()
  {
    itemXnam = NpdsConst.ServiceRestrictionOrItemXnam;
  }

  public Guid? RestrictionAndGuid { get; set; } = null;
  public Guid? RestrictionOrGuid { get; set; } = null;
  public byte RestrictionOrIndex { get; set; } = 0;
  public byte RestrictionOrPriority { get; set; } = 0;
  public string? Restriction { get; set; } = string.Empty;
  public bool IsWordPhrase { get; set; } = false;
  public bool IsConceptLabel { get; set; } = false;
}
