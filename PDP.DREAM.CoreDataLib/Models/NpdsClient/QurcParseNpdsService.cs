// QurcParseNpdsRoute.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserRestContext
{
  // prioritize parse first route segments from URL
  // then override only if querystring collection values present
  public void ParseNpdsService(string searchFilter, 
    string serviceTag, string serviceType, // string serviceVersion,
    string entityTag = "", string entityType = "", string entityVersion = "",
    string infosetStatus = "", string recordAccess = "")
  {
    ParseNpdsResrepFilter(searchFilter, serviceTag, serviceType);

    if (string.IsNullOrWhiteSpace(recordAccess))
    { recordAccess = NPDSSD.RecordAccessDefault.ToString(); }
    RecordAccessReqst = recordAccess;

    if (string.IsNullOrWhiteSpace(entityType))
    { entityType = NPDSSD.EntityTypeDefault.ToString(); }

    if (string.IsNullOrEmpty(entityType)) { EntityType = NpdsEntityType.AnyAndAll; }
    else { EntityTypeReqst = entityType; }

    if (string.IsNullOrEmpty(entityTag)) { EntityTag = string.Empty; }
    else { EntityTagReqst = entityTag; }

    if (string.IsNullOrEmpty(entityVersion)) { EntityVersion = string.Empty; }
    else { EntityVersionReqst = entityVersion; }

    if (string.IsNullOrEmpty(infosetStatus)) { InfosetStatus = NpdsInfosetStatus.AnyAndAll; }
    else { InfosetStatusReqst = infosetStatus; }

#if DEBUG
    DebugNpdsParams(nameof(ParseNpdsService), nameof(QebiUserRestContext));
#endif
  }

} // end class

// end file