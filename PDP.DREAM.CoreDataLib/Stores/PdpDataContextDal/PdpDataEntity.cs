// PdpDataEntity.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class PdpDataEntity
{
  public Guid OnEntityCreated(Guid appGuid)
  {
    return PdpGuid.ParseToNonNullable(appGuid, PDPSS.AppSecureUiaaGuid);
  }

} // end class

// end file