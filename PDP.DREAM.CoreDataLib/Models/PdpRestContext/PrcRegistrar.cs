// PrcRegistrar.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
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

    public string RegistrarTagDeflt
    {
      set
      {
        defRegistrarTag = value;
        defRegistrarGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(defRegistrarTag);
      }
      get { return defRegistrarTag; }
    }
    private string defRegistrarTag = NpdsServiceDefaults.Values.NpdsDefaultRegistrarTag;

    public Guid RegistrarGuidDeflt
    { get { return defRegistrarGuid; } }
    private Guid defRegistrarGuid = NpdsServiceDefaults.Values.NpdsDefaultRegistrarGuid;

    // requested values from request options (URL routes or querystrings)

    public string RegistrarTagReqst
    {
      set
      {
        reqRegistrarTag = value;
        if (!string.IsNullOrEmpty(reqRegistrarTag)) { RegistrarTag = reqRegistrarTag; }
      }
      get { return reqRegistrarTag; }
    }
    private string reqRegistrarTag = string.Empty;

    public string RegistrarGuidReqst
    {
      set
      {
        reqRegistrarGuid = value;
        if (!string.IsNullOrEmpty(reqRegistrarGuid)) { RegistrarGuid = PdpGuid.Parse(reqRegistrarGuid, RegistrarGuidDeflt); }
      }
      get { return reqRegistrarGuid; }
    }
    private string reqRegistrarGuid = string.Empty;

    public void RegistrarGuidResetFromReqst()
    {
      RegistrarGuid = PdpGuid.Parse(RegistrarGuidReqst, RegistrarGuidDeflt);
    }

    // selected values for response settings

    public string RegistrarTag
    {
      set
      {
        registrarTag = ValidateServiceTag(value, NpdsServiceDefaults.Values.NpdsConstraintRegistrarTag);
        registrarGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(registrarTag);
      }
      get
      {
        if (string.IsNullOrEmpty(registrarTag)) { registrarTag = RegistrarTagDeflt; }
        return registrarTag;
      }
    }
    private string registrarTag = string.Empty;

    public Guid? RegistrarGuid
    {
      set
      {
        registrarGuid = value;
        registrarTag = NpdsServiceDefaults.Values.NpdsServiceCache.GetByNullableGuid(registrarGuid);
      }
      get
      {
        if (PdpGuid.IsNullOrEmpty(registrarGuid)) { registrarGuid = RegistrarGuidDeflt; }
        return registrarGuid;
      }
    }
    private Guid? registrarGuid = Guid.Empty;

  }

}
