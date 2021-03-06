// PrcDiristry.cs 
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

    public string DiristryTagDeflt
    {
      set
      {
        defDiristryTag = value;
        defDiristryGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(defDiristryTag);
      }
      get { return defDiristryTag; }
    }
    private string defDiristryTag = NpdsServiceDefaults.Values.NpdsDefaultDiristryTag;

    public Guid DiristryGuidDeflt
    { get { return defDiristryGuid; } }
    private Guid defDiristryGuid = NpdsServiceDefaults.Values.NpdsDefaultDiristryGuid;

    // requested values from request options (URL routes or querystrings)

    public string DiristryTagReqst
    {
      set
      {
        reqDiristryTag = value;
        if (!string.IsNullOrEmpty(reqDiristryTag)) { DiristryTag = reqDiristryTag; }
      }
      get { return reqDiristryTag; }
    }
    private string reqDiristryTag = string.Empty;

    public string DiristryGuidReqst
    {
      set
      {
        reqDiristryGuid = value;
        if (!string.IsNullOrEmpty(reqDiristryGuid)) { DiristryGuid = PdpGuid.Parse(reqDiristryGuid, DiristryGuidDeflt); }
      }
      get { return reqDiristryGuid; }
    }
    private string reqDiristryGuid = string.Empty;

    public void DiristryGuidResetFromReqst()
    {
      DiristryGuid = PdpGuid.Parse(DiristryGuidReqst, DiristryGuidDeflt);
    }

    // selected values for response settings

    public string DiristryTag
    {
      set
      {
        diristryTag = ValidateServiceTag(value, NpdsServiceDefaults.Values.NpdsConstraintDiristryTag);
        diristryGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(diristryTag);
      }
      get
      {
        if (string.IsNullOrEmpty(diristryTag)) { diristryTag = DiristryTagDeflt; }
        return diristryTag;
      }
    }
    private string diristryTag = string.Empty;

    public Guid? DiristryGuid
    {
      set
      {
        diristryGuid = value;
        diristryTag = NpdsServiceDefaults.Values.NpdsServiceCache.GetByNullableGuid(diristryGuid);
      }
      get
      {
        if (PdpGuid.IsNullOrEmpty(diristryGuid)) { diristryGuid = DiristryGuidDeflt; }
        return diristryGuid;
      }
    }
    private Guid? diristryGuid = Guid.Empty;

  }

}
