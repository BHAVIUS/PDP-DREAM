// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract partial class WraceRazorViewControllerBase : Controller
{
  // Web REST API Controller Environment (NPDSCW) for user config settings and web api requests
  // QEB User Data Context (QUDC) for user identification, authentication, authorization
  // PDP Core Data Context (PCDC) for Core data repositories of data/metadata records
  // PDP Nexus Data Context (PNDC) for Nexus data repositories of data/metadata records
  // PDP Scribe Data Context (PCDC) for Scribe data repositories of data/metadata records
  // PDP ACMS Data Context (PADC) for ACMS data repositories of data/metadata records

  protected void DebugWraceRazorView(string rzrHandlerName = ESS, string rzrCntrllrName = ESS)
  {
    CatchNullWrace(rzrHandlerName, rzrCntrllrName);
    NPDSCW.DebugWraceData(rzrHandlerName, rzrCntrllrName);
    NPDSCW.DebugClientAccess(rzrHandlerName, rzrCntrllrName);
    NPDSCW.DebugNpdsSelectFilter(rzrHandlerName, rzrCntrllrName);
    PSRM.DebugRazorPageStrings(rzrHandlerName, rzrCntrllrName);
  }

  protected void CatchNullWrace(string methodName = ESS, string className = ESS)
  {
    NPDSCW.CatchNullObject(WraceKey, methodName, className);
  }

  protected void WraceAddErrors(string error)
  {
    ModelState.AddModelError(ESS, error);
  }
  protected void WraceAddErrors(string[] errors)
  {
    foreach (var error in errors)
    {
      ModelState.AddModelError(ESS, error);
    }
  }

  protected void WraceAddQebiErrors(IEnumerable<QebIdentityError> errorList)
  {
    foreach (var error in errorList)
    {
      ModelState.AddModelError("PdpIdentity", error.Message);
    }
  }

} // end class

// end file