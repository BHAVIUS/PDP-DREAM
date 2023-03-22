// PdpDataEntity.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class DbsqlContextEntity
{
  public Guid OnEntityCreated(Guid appGuid)
  {
    return PdpGuid.ParseToNonNullable(appGuid, PDPSS.AppSecureUiaaGuid);
  }

} // end class

// end file