// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.NexusDataLib.Stores;

namespace PDP.DREAM.NexusWebLib.Controllers;

// Telerik Kendo Grid Nexus PageControllerBase
public abstract partial class TkgnPageControllerBase : NexusDataRazorPageControllerBase
{
  public TkgnPageControllerBase() { }
  public TkgnPageControllerBase(QebIdentityContext userCntxt) : base(userCntxt) { }
  public TkgnPageControllerBase(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }
  public TkgnPageControllerBase(QebIdentityContext userCntxt, NexusDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

} // end class

// end file