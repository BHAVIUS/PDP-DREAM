// QurcParseSelectFilterForPage.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // TODO: revert to a void and remove the pageTitle parameter ?
  public string ParseNpdsSelectFilterForPage(string serviceType, string serviceTag,
   string entityType, string recordAccess = "", string pageTitle = "")
  {
    if (string.IsNullOrEmpty(recordAccess))
    { recordAccess = RecordAccess.ToString(); }

    ParseNpdsUrlSegments(serviceType, serviceTag,
     "", "", entityType, "", recordAccess);

    if (string.IsNullOrEmpty(pageTitle)) { pageTitle = "resreps from"; }
    return $"{pageTitle} {ServiceTitle}";
  }

} // end class

// end file