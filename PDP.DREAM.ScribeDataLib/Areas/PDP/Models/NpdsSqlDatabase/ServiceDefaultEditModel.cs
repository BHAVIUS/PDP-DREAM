// ServiceDefaultEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

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