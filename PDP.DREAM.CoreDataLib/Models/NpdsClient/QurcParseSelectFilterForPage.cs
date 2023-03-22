// QurcParseSelectFilterForPage.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserRestContext
{
  public string ParseNpdsSelectFilterForPage(string searchFilter,
    string serviceTag, string serviceType, string entityType,
    string recordAccess = "", string pageTitle = "")
  {
    if (string.IsNullOrEmpty(recordAccess))
    { recordAccess = RecordAccess.ToString(); }

    ParseNpdsService(searchFilter,
      serviceTag, serviceType, // serviceVersion ?
      "", entityType, "", recordAccess);

    if (string.IsNullOrEmpty(pageTitle)) 
    { pageTitle = "resreps from"; }
    pageTitle = $"{pageTitle} {ServiceTitle}";

    return pageTitle;
  }

} // end class

// end file