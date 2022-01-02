// PrcRegistry.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
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
        defRegistryGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(defRegistryTag);
      }
      get { return defRegistryTag; }
    }
    private string defRegistryTag = NpdsServiceDefaults.Values.NpdsDefaultRegistryTag;

    public Guid RegistryGuidDeflt
    { get { return defRegistryGuid; } }
    private Guid defRegistryGuid = NpdsServiceDefaults.Values.NpdsDefaultRegistryGuid;

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
        registryTag = ValidateServiceTag(value, NpdsServiceDefaults.Values.NpdsConstraintRegistryTag);
        registryGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(registryTag);
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
        registryTag = NpdsServiceDefaults.Values.NpdsServiceCache.GetByNullableGuid(registryGuid);
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
