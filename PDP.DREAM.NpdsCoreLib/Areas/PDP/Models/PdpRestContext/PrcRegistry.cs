// PrcRegistry.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
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
        defRegistryGuid = NpdsServiceDefaults.GetValues.NpdsServiceCache.GetByTag(defRegistryTag);
      }
      get { return defRegistryTag; }
    }
    private string defRegistryTag = NpdsServiceDefaults.GetValues.NpdsDefaultRegistryTag;

    public Guid RegistryGuidDeflt
    { get { return defRegistryGuid; } }
    private Guid defRegistryGuid = NpdsServiceDefaults.GetValues.NpdsDefaultRegistryGuid;

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
        if (!string.IsNullOrEmpty(reqRegistryGuid)) { RegistryGuid = PdpGuid.Parse(reqRegistryGuid, RegistryGuidDeflt); }
      }
      get { return reqRegistryGuid; }
    }
    private string reqRegistryGuid = string.Empty;

    public void RegistryGuidResetFromReqst()
    {
      RegistryGuid = PdpGuid.Parse(RegistryGuidReqst, RegistryGuidDeflt);
    }

    // selected values for response settings

    public string RegistryTag
    {
      set
      {
        registryTag = ValidateServiceTag(value, NpdsServiceDefaults.GetValues.NpdsConstraintRegistryTag);
        registryGuid = NpdsServiceDefaults.GetValues.NpdsServiceCache.GetByTag(registryTag);
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
        registryTag = NpdsServiceDefaults.GetValues.NpdsServiceCache.GetByNullableGuid(registryGuid);
      }
      get
      {
        if (PdpGuid.IsNullOrEmpty(registryGuid)) { registryGuid = RegistryGuidDeflt; }
        return registryGuid;
      }
    }
    private Guid? registryGuid = Guid.Empty;

  }

}
