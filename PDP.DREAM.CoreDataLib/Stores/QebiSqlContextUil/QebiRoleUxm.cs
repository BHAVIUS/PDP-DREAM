// QebiRoleUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class QebiRoleUxm : DbsqlContextEntity
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

