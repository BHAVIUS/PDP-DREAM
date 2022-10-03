// QebiUserRoleModels.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Models;

public class QebiRoleUxm : PdpDataEntity
{
  public QebiRoleUxm()
  {
    if (AppGuid == Guid.Empty) { AppGuid = PDPSS.AppSecureUiaaGuid; }
  }
  public Guid AppGuid { get; set; } = Guid.Empty;
  public Guid RoleGuid { get; set; } = Guid.Empty;
  public string RoleName { get; set; } = string.Empty;
  public string RoleDescription { get; set; } = string.Empty;
}

public class QebiUserRoleUxm : PdpDataEntity
{
  public QebiUserRoleUxm()
  {
    if (AppGuid == Guid.Empty) { AppGuid = PDPSS.AppSecureUiaaGuid; }
  }
  public Guid AppGuid { get; set; } = Guid.Empty;
  public Guid UserGuid { get; set; } = Guid.Empty;
  public string UserName { get; set; } = string.Empty;
  public Guid RoleGuid { get; set; } = Guid.Empty;
  public string RoleName { get; set; } = string.Empty;
  public string RoleDescription { get; set; } = string.Empty;
}
