// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// QEBI User Dated Form Task Uxm
public class QebiUdftUxm : FormTaskUxmBase
{
  public DateTime? DateCreatedOn { get; set; } = null;
  public DateTime? DateUpdatedOn { get; set; } = null;
  public Guid? DateCreatedBy { get; set; } = EGS;
  public Guid? DateUpdatedBy { get; set; } = EGS;

  public void CreateUdftUxm(Guid? userguid)
  {
    DateTime utcnow = DateTime.UtcNow;
    DateCreatedBy = userguid;
    DateUpdatedBy = userguid;
    DateCreatedOn = utcnow;
    DateUpdatedOn = utcnow;
  }

  public void UpdateUdftUxm(Guid? userguid)
  {
    DateTime utcnow = DateTime.UtcNow;
    DateUpdatedBy = userguid;
    DateUpdatedOn = utcnow;
  }

} // end class

// end file