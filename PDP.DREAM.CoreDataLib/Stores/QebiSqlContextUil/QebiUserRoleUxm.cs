// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class QebiUserRoleUxm : QebiDalEntity
{
  public QebiUserRoleUxm()
  {
    if (AppGuid == EGS) { AppGuid = PDPSS.CiaamAppGuid; }
  }
  public Guid AppGuid { get; set; } = EGS;

  public Guid UserGuid { get; set; } = EGS;
  public string UserName { get; set; } = ESS;

  public Guid RoleGuid { get; set; } = EGS;
  public string RoleName { get; set; } = ESS;
  public string RoleDescription { get; set; } = ESS;
}
