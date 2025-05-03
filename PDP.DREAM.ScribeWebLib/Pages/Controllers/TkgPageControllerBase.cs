// TkgPageControllerBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeWebLib.Controllers;
using PDP.DREAM.ScribeDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.ScribeWebLib.Controllers;

// Telerik Kendo Grid Scribe PageControllerBase
public abstract partial class TkgsPageControllerBase : ScribeDataRazorPageControllerBase
{
  public TkgsPageControllerBase() { }
  public TkgsPageControllerBase(QebIdentityContext userCntxt) : base(userCntxt) { }
  public TkgsPageControllerBase(ScribeDbsqlContext npdsCntxt) : base(npdsCntxt) { }
  public TkgsPageControllerBase(QebIdentityContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

} // end class

// end file