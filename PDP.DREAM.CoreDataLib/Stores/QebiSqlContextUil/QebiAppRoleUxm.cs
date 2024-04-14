// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class QebiAppRoleUxm : QebiDalEntity
{
  public QebiAppRoleUxm()
  {
    if (AppGuid == EGS) { AppGuid = PDPSS.CiaamAppGuid; }
  }
  public Guid AppGuid { get; set; } = EGS;
  public Guid RoleGuid { get; set; } = EGS;
  public byte RoleCode { get; set; } = 0;
  public string RoleName { get; set; } = ESS;
  public string RoleDescription { get; set; } = ESS;
  public string AppRoleComment { get; set; } = ESS;

} // end class

// end file