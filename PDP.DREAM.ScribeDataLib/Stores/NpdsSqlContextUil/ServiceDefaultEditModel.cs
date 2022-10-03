// ServiceDefaultEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class ServiceDefaultEditModel : NexusViewModelBase
{
  public ServiceDefaultEditModel()
  {
    itemXnam = PdpAppConst.ServiceDefaultItemXnam;
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
