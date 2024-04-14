// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebiDalEntity
{
  public Guid OnEntityCreated(Guid appGuid)
  {
    return PdpGuid.ParseToNonNullable(appGuid, PDPSS.CiaamAppGuid);
  }

  // ATTN: properties cannot be used here without rebuilding DAL
  // or alternatively creating a separate UIL for necessary objects
  // enabling data transfer from DAL to UIL in LINQ queries

} // end class

// end file