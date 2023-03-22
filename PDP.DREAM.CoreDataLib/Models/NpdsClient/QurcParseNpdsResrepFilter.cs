// QurcParseNpdsResrepFilter.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserRestContext
{
  public void ParseNpdsResrepFilter(string searchFilter, 
    string serviceTag, string entityType = "")
  {
    // NPDS SearchFilter
    if (string.IsNullOrWhiteSpace(searchFilter))
    { searchFilter = NPDSSD.SearchFilterDefault.ToString(); }
    SearchFilterReqst = searchFilter;

    // NPDS ServiceTag
    if (string.IsNullOrWhiteSpace(serviceTag))
    { serviceTag = NPDSSD.DiristryTagDefault; }
    ServiceTagReqst = serviceTag;

    // NPDS EntityType
    if (string.IsNullOrWhiteSpace(entityType))
    { entityType = NPDSSD.EntityTypeDefault.ToString(); }
    EntityTypeReqst = entityType;

#if DEBUG
    DebugNpdsParams(nameof(ParseNpdsResrepFilter), nameof(QebiUserRestContext));
#endif
  }

} // end class

// end file