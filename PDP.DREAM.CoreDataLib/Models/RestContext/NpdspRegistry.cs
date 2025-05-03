// PrcRegistry.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // ATTN: if nothing in request options and nothing in server parameter constraints
  //       then leave as nothing in response settings !!! (otherwise incorrect filtering results)

  //   ResponseSettings should have NPDS values
  //      which may change on every request, and which may be null as follows:
  //      if requested value null and constraint value null, then null (do not use default!!!);
  //      TODO: if requested value null and constraint value non-null, then any of the constraint values;
  //      if requested value non-null and constraint value null, then the requested value;
  //      if requested value non-null and constraint value non-null, then the requested value if valid value else first constraint value;

  // default values from service parameters

  public string RegistryTagDeflt
  {
    set
    {
      defRegistryTag = value;
      defRegistryGuid = PdpAppStatus.NPDSSD.NpdsServiceCache.GetByTag(defRegistryTag);
    }
    get { return defRegistryTag; }
  }
  private string defRegistryTag = PdpAppStatus.NPDSSD.NpdsDefaultRegistryTag;

  public Guid RegistryGuidDeflt
  { get { return defRegistryGuid; } }
  private Guid defRegistryGuid = PdpAppStatus.NPDSSD.NpdsDefaultRegistryGuid;

  // requested values from request options (URL routes or querystrings)

  public string RegistryTagReqst
  {
    set
    {
      reqRegistryTag = value;
      if (!string.IsNullOrEmpty(reqRegistryTag)) { RegistryTag = reqRegistryTag; }
    }
    get { return reqRegistryTag; }
  }
  private string reqRegistryTag = string.Empty;

  public string RegistryGuidReqst
  {
    set
    {
      reqRegistryGuid = value;
      if (!string.IsNullOrEmpty(reqRegistryGuid)) { RegistryGuid = PdpGuid.ParseToNullable(reqRegistryGuid, RegistryGuidDeflt); }
    }
    get { return reqRegistryGuid; }
  }
  private string reqRegistryGuid = string.Empty;

  public void RegistryGuidResetFromReqst()
  {
    RegistryGuid = PdpGuid.ParseToNullable(RegistryGuidReqst, RegistryGuidDeflt);
  }

  // selected values for response settings

  public string RegistryTag
  {
    set
    {
      registryTag = ValidateServiceTag(value, PdpAppStatus.NPDSSD.NpdsConstraintRegistryTag);
      registryGuid = PdpAppStatus.NPDSSD.NpdsServiceCache.GetByTag(registryTag);
    }
    get
    {
      if (string.IsNullOrEmpty(registryTag)) { registryTag = RegistryTagDeflt; }
      return registryTag;
    }
  }
  private string registryTag = string.Empty;

  public Guid? RegistryGuid
  {
    set
    {
      registryGuid = value;
      registryTag = PdpAppStatus.NPDSSD.NpdsServiceCache.GetByNullableGuid(registryGuid);
    }
    get
    {
      if (PdpGuid.IsNullOrEmpty(registryGuid)) { registryGuid = RegistryGuidDeflt; }
      return registryGuid;
    }
  }
  private Guid? registryGuid = Guid.Empty;

}

