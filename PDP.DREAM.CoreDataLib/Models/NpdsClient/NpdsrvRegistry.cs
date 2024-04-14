// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // ATTN: if nothing in request options and nothing in server parameter constraints
  //       then leave as nothing in response settings !!! (otherwise incorrect filtering results)
  //   ResponseSettings should have NPDS values
  //      which may change on every request, and which may be null as follows:
  //      if requested value null and constraint value null, then null (do not use default!!!);
  //      TODO: if requested value null and constraint value non-null, then any of the constraint values;
  //      if requested value non-null and constraint value null, then the requested value;
  //      if requested value non-null and constraint value non-null, then the requested value if valid value else first constraint value;

  // requested values from request options (URL routes or querystrings)

  private string reqRegistryTag = ESS;
  public string RegistryTagReqst
  {
    set {
      reqRegistryTag = value;
      if (!string.IsNullOrEmpty(reqRegistryTag))
      { RegistryTag = reqRegistryTag; }
    }
    get { return reqRegistryTag; }
  }

  private string reqRegistryGuid = ESS;
  public string RegistryGuidReqst
  {
    set {
      reqRegistryGuid = value;
      if (!string.IsNullOrEmpty(reqRegistryGuid))
      { RegistryGuid = PdpGuid.ParseToNullable(reqRegistryGuid, NPDSCD.RegistryGuidDefault); }
    }
    get { return reqRegistryGuid; }
  }

  public void RegistryGuidResetFromReqstAndDeflt()
  {
    RegistryGuid = PdpGuid.ParseToNullable(RegistryGuidReqst, NPDSCD.RegistryGuidDefault);
  }

  // selected values for response settings

  private string? registryTag = ESS;
  public string? RegistryTag
  {
    set {
      registryTag = ValidateServiceTag(value, NPDSCD.RegistryTagConstraint);
      if (string.IsNullOrEmpty(registryTag)) { registryGuid = EGS; }
      else { registryGuid = NPDSCD.NpdsServiceCache.GetByTag(registryTag); }
    }
    get { return registryTag; }
  }

  private Guid? registryGuid = EGS;
  public Guid? RegistryGuid
  {
    set {
      registryGuid = value;
      if (value.IsNullOrEmpty()) { registryTag = ESS; }
      else { registryTag = NPDSCD.NpdsServiceCache.GetByNullableGuid(registryGuid); }
    }
    get { return registryGuid; }
  }

} // end class

// end file