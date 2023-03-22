// NpdscpRegistry.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
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

  private string reqRegistryTag = string.Empty;
  public string RegistryTagReqst
  {
    set {
      reqRegistryTag = value;
      if (!string.IsNullOrEmpty(reqRegistryTag))
      { RegistryTag = reqRegistryTag; }
    }
    get { return reqRegistryTag; }
  }

  private string reqRegistryGuid = string.Empty;
  public string RegistryGuidReqst
  {
    set {
      reqRegistryGuid = value;
      if (!string.IsNullOrEmpty(reqRegistryGuid))
      { RegistryGuid = PdpGuid.ParseToNullable(reqRegistryGuid, NPDSSD.RegistryGuidDefault); }
    }
    get { return reqRegistryGuid; }
  }

  public void RegistryGuidResetFromReqstAndDeflt()
  {
    RegistryGuid = PdpGuid.ParseToNullable(RegistryGuidReqst, NPDSSD.RegistryGuidDefault);
  }

  // selected values for response settings

  private string? registryTag = string.Empty;
  public string? RegistryTag
  {
    set {
      registryTag = ValidateServiceTag(value, NPDSSD.RegistryTagConstraint);
      if (string.IsNullOrEmpty(registryTag)) { registryGuid = Guid.Empty; }
      else { registryGuid = NPDSSD.NpdsServiceCache.GetByTag(registryTag); }
    }
    get { return registryTag; }
  }

  private Guid? registryGuid = Guid.Empty;
  public Guid? RegistryGuid
  {
    set {
      registryGuid = value;
      if (value.IsNullOrEmpty()) { registryTag = string.Empty; }
      else { registryTag = NPDSSD.NpdsServiceCache.GetByNullableGuid(registryGuid); }
    }
    get { return registryGuid; }
  }

} // end class

// end file