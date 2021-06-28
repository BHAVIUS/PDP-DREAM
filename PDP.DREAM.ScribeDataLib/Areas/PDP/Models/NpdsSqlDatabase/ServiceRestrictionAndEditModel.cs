using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class RestrictionAndEditModel : NexusEditModelBase
  {
    public RestrictionAndEditModel()
    {
      itemXnam = NpdsConst.ServiceRestrictionAndItemXnam;
    }

    public Guid? RestrictionAndGuidKey { get; set; } = null;
    public byte RestrictionAndIndex { get; set; } = 0;
    public byte RestrictionAndPriority { get; set; } = 0;
    public string? RestrictionName { get; set; } = string.Empty;
    public bool RestrictionIsSufficient { get; set; } = false;

  }

}