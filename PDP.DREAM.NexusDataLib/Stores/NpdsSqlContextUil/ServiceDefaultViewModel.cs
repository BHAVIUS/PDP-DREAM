﻿// ServiceDefaultViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public class ServiceDefaultViewModel : CoreResrepModelBase
{
  public ServiceDefaultViewModel()
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
