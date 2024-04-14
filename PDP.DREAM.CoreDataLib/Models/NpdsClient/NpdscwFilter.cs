// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace : INpdscwClient
{
  // prioritize parse first route segments from URL
  // then override only if querystring collection values present
  // for use with core library and API wrappers
  // which map to this standardized client method
  // ie, not intended for use by public calling REST API methods

  // 0 params from current values of NpdsClient
  public void ParseNpdsSelectFilter()
  {
    ParseNpdsSelectFilter(RecordAccess.EName,
      ServiceType.EName, "", ServiceTag,
      EntityType.ToString(), "", EntityTag,
      InfosetStatus.ToString(), SearchFilter.EName, ResrepFormat.EName);
  }

  // 10 params
  public void ParseNpdsSelectFilter(string recordAccess,
    string serviceType, string serviceVersion, string serviceTag,
    string entityType, string entityVersion, string entityTag,
    string infosetStatus, string searchFilter, string resrepFormat)
  {
    if (!string.IsNullOrWhiteSpace(recordAccess))
    { RecordAccessReqst = recordAccess; }
    // else use current RecordAccess set by calling controller

    // NPDS ServiceType
    if (!string.IsNullOrWhiteSpace(serviceType))
    { ServiceTypeReqst = serviceType; }
    // else use current ServiceType set by calling controller

    // NPDS SearchFilter
    // should be set by ServiceType but can be overridden if non-empty
    if (!string.IsNullOrWhiteSpace(searchFilter))
    { SearchFilterReqst = searchFilter; }
    // else use current SearchFilter set by ServiceType

    // NPDS ServiceTag
    // should always be reset for every request
    if (string.IsNullOrWhiteSpace(serviceTag))
    {
      serviceType = ServiceType.EName;
      switch (serviceType)
      {
        case "Nexus":
          serviceTag = NPDSCD.DiristryTagDefault;
          break;
        case "PORTAL":
          serviceTag = NPDSCD.RegistryTagDefault;
          break;
        case "DOORS":
          serviceTag = NPDSCD.DirectoryTagDefault;
          break;
        case "Scribe":
          serviceTag = NPDSCD.RegistrarTagDefault;
          break;
        case "Core":
          // TODO: consider CCC service for Core Components Constituents
          // or separate services for NPDS component services vs persons/organizations
          // but must differentiate the core components for NPDS services from the 
          // common core used for ResrepFormats
          serviceTag = NPDSCD.ServiceTagDefault;
          break;
        default:
          throw new Exception($"{nameof(ParseNpdsSelectFilter)} case Service {serviceType} not implemented");
      }
    }
    ServiceTagReqst = serviceTag;

    // NPDS ResrepFormat
    // should always be reset for every request
    if (string.IsNullOrEmpty(resrepFormat))
    { resrepFormat = NPDSCD.ResrepFormatDefault.EName; }
    ResrepFormatReqst = resrepFormat;

    // NPDS EntityType
    if (string.IsNullOrEmpty(entityType)) { EntityType = NPDSCD.EntityTypeAnyAndAll; }
    else { EntityTypeReqst = entityType; }

    // NPDS EntityTag
    if (string.IsNullOrEmpty(entityTag)) { EntityTag = ESS; }
    else { EntityTagReqst = entityTag; }

    if (string.IsNullOrEmpty(entityVersion)) { EntityVersion = ESS; }
    else { EntityVersionReqst = entityVersion; }

    if (string.IsNullOrEmpty(infosetStatus)) { InfosetStatus = NPDSCD.InfosetStatusAnyAndAll; }
    else { InfosetStatusReqst = infosetStatus; }

#if DEBUG
    DebugNpdsSelectFilter(nameof(ParseNpdsSelectFilter), nameof(NpdsClientWrace));
#endif
  }

  // 6 params
  public void ParseNpdsSelectFilter(string serviceType, string serviceTag, string entityType, string entityTag, string searchFilter, string resrepFormat)
  {
    ParseNpdsSelectFilter("",
      serviceType, "", serviceTag,
      entityType, "", entityTag,
      "", searchFilter, resrepFormat);
  }

  // 5 params
  public void ParseNpdsSelectFilter(string serviceType, string serviceTag, string entityType, string entityTag, string searchFilter)
  {
    ParseNpdsSelectFilter("",
      serviceType, "", serviceTag,
      entityType, "", entityTag,
      "", searchFilter, "");
  }

  // 4 params
  public void ParseNpdsSelectFilter(string serviceType, string serviceTag, string entityType, string searchFilter)
  {
    ParseNpdsSelectFilter("",
      serviceType, "", serviceTag,
      entityType, "", "",
      "", searchFilter, "");
  }

  // 3 params
  public void ParseNpdsSelectFilter(string serviceType, string serviceTag, string entityTag)
  {
    ParseNpdsSelectFilter("",
      serviceType, "", serviceTag,
       "", "", entityTag,
      "", "", "");
  }

  // debug methods
  public virtual void DebugClientAccess(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugClientAccess)} called from Class = '{className}'; Method = '{methodName}';");
    Debug.WriteLine($"UserGuid = '{QebiUserGuid}'; PersonGuid = '{BridgePersonGuid}'; AgentGuid = '{NpdsAgentGuid}';");
    Debug.WriteLine($"RecordAccess = '{RecordAccess.EName}'; UserName = '{CiaamUserName}'; UserAlias = '{CiaamUserAlias}';");
    Debug.WriteLine($"IsUser ='{ClientIsUser}'; IsAgent = '{ClientIsAgent}'; IsAuthor = '{ClientIsAuthor}';");
    Debug.WriteLine($"IsReviewer = '{ClientIsReviewer}'; IsEditor = '{ClientIsEditor}'; IsAdmin ='{ClientIsAdmin}'");
    Debug.WriteLine($"{nameof(UserModeClientRequired)}: {UserModeClientRequired}; {nameof(ClientHasUserMode)}: {ClientHasUserMode}; {nameof(ClientCanUserWrite)}: {ClientCanUserWrite};");
    Debug.WriteLine($"{nameof(AgentModeClientRequired)}: {AgentModeClientRequired}; {nameof(ClientHasAgentMode)}: {ClientHasAgentMode}; {nameof(ClientCanAgentWrite)}: {ClientCanAgentWrite};");
    Debug.WriteLine($"{nameof(AuthorModeClientRequired)}: {AuthorModeClientRequired}; {nameof(ClientHasAuthorMode)}: {ClientHasAuthorMode}; {nameof(ClientCanAuthorWrite)}: {ClientCanAuthorWrite};");
    Debug.WriteLine($"{nameof(ReviewerModeClientRequired)}: {ReviewerModeClientRequired}; {nameof(ClientHasReviewerMode)}: {ClientHasReviewerMode}; {nameof(ClientCanAuthorWrite)}: {ClientCanAuthorWrite};");
    Debug.WriteLine($"{nameof(EditorModeClientRequired)}: {EditorModeClientRequired}; {nameof(ClientHasEditorMode)}: {ClientHasEditorMode}; {nameof(ClientCanEditorWrite)}: {ClientCanEditorWrite};");
    Debug.WriteLine($"{nameof(AdminModeClientRequired)}: {AdminModeClientRequired}; {nameof(ClientHasAdminMode)}: {ClientHasAdminMode}; {nameof(ClientCanAdminWrite)}: {ClientCanAdminWrite};");
  }

  public virtual void DebugNpdsSelectFilter(string methodName = "", string className = "")
  {
    Debug.WriteLine($"{nameof(DebugNpdsSelectFilter)} called from Class = '{className}'; Method = '{methodName}';");
    // SearchFilter should default to corresponding ServiceType
    // eg, SearchFilter = Diristry for ServiceType = Nexus
    // SearchFilter may be different for each ServiceType,
    // eg, SearchFilter = Registry for ServiceType = Scribe
    Debug.WriteLine($"ServerType = {ServerType.EName}; DatabaseType = {DatabaseType.EName}; RecordAccess = {RecordAccess.EName};");
    Debug.WriteLine($"ServiceType = {ServiceType.EName}; ServiceTag = {ServiceTag}; SearchFilter = {SearchFilter.EName}; ");
    Debug.WriteLine($"ResrepFormat = {ResrepFormat.EName}; EntityType = {EntityType}; EntityTag = {EntityTag};");
    Debug.WriteLine($"ResrepParamsPath = /{SearchFilter.EName}/{ServiceTag}/{EntityType}/{ResrepFormat.EName}");
    Debug.WriteLine($"with DiristryTag = '{DiristryTag}' and DiristryGuid = '{DiristryGuid}'");
    Debug.WriteLine($"with RegistryTag = '{RegistryTag}' and RegistryGuid = '{RegistryGuid}'");
    Debug.WriteLine($"with DirectoryTag = '{DirectoryTag}' and DirectoryGuid = '{DirectoryGuid}'");
    Debug.WriteLine($"with RegistrarTag = '{RegistrarTag}' and RegistrarGuid = '{RegistrarGuid}'");
  }

} // end class

// end file