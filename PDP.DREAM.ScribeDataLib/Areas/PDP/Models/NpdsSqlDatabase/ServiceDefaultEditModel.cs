using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class ServiceDefaultEditModel : NexusEditModelBase
  {
    public ServiceDefaultEditModel()
    {
      itemXnam = NpdsConst.ServiceDefaultItemXnam;
    }

    public Guid? DiristryGuid { get; set; } = null;
    public Guid? RegistryGuid { get; set; } = null;
    public Guid? DirectoryGuid { get; set; } = null;
    public Guid? RegistrarGuid { get; set; } = null;

    public string? DiristryName { get; set; } = string.Empty;
    public string? RegistryName { get; set; } = string.Empty;
    public string? DirectoryName { get; set; } = string.Empty;
    public string? RegistrarName { get; set; } = string.Empty;

  }

}