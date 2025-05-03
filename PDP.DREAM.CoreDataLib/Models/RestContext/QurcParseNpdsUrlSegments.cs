// QurcParseNpdsUrlSegments.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // prioritize first the URL route segments then override only if QS values present
  public void ParseNpdsUrlSegments(string serviceType, string serviceTag,
    string entityTag = "", string entityVersion = "", string entityType = "",
    string infosetStatus = "", string recordAccess = "")
  {
    // null empty missing values reset to default values
    if (string.IsNullOrWhiteSpace(recordAccess))
    {
      recordAccess = NPDSSD.NpdsDefaultRecordAccess.ToString();
    }
    if (string.IsNullOrWhiteSpace(entityType))
    {
      entityType = NPDSSD.NpdsDefaultEntityType.ToString();
    }
    if (string.IsNullOrWhiteSpace(serviceType))
    {
      serviceType = NPDSSD.NpdsDefaultServiceType.ToString();
    }
    if (string.IsNullOrWhiteSpace(serviceTag))
    {
      switch (serviceType.ToLower())
      {
        case "nexus":
          serviceTag = NPDSSD.NpdsDefaultDiristryTag;
          break;
        case "portal":
          serviceTag = NPDSSD.NpdsDefaultRegistryTag;
          break;
        case "doors":
          serviceTag = NPDSSD.NpdsDefaultDirectoryTag;
          break;
        case "scribe":
          serviceTag = NPDSSD.NpdsDefaultRegistrarTag;
          break;
        default:
          serviceTag = NPDSSD.NpdsRootServiceTag;
          break;
      }
    }

    // TODO: recode logic of settings to eliminate any redundancies and/or cycles
    // current code requires ordered sequence when setting values
    RecordAccessReqst = recordAccess;
    // service type request should reset search filter request
    ServiceTypeReqst = serviceType;
    // service tag request should precede the service type request 
    // per old notes, but must refactor and retest so not necessary
    ServiceTagReqst = serviceTag;
    // TODO: update ServiceGuid ?!? corresponding to ServiceTag
    // service tag and service type should be set before calling ValidateSearchFilter
    // TODO: code an overload of ValidateSearchFilter that accepts service type/tag as input args
    ValidateSearchFilter();

    if (string.IsNullOrEmpty(entityType)) { EntityType = NpdsEntityType.AnyAndAll; }
    else { EntityTypeReqst = entityType; }

    if (string.IsNullOrEmpty(entityTag)) { EntityTag = string.Empty; }
    else { EntityTagReqst = entityTag; }

    if (string.IsNullOrEmpty(entityVersion)) { EntityVersion = string.Empty; }
    else { EntityVersionReqst = entityVersion; }

    if (string.IsNullOrEmpty(infosetStatus)) { InfosetStatus = NpdsInfosetStatus.AnyAndAll; }
    else { InfosetStatusReqst = infosetStatus; }

#if DEBUG
    Debug.WriteLine($"NPDS path = /{ServiceType}/{ServiceTag}/{EntityTag}");
    Debug.WriteLine(DbConnectionString);
#endif
  }

} // end class

// end file