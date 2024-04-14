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

  private string reqDiristryTag = ESS;
  public string DiristryTagReqst
  {
    set {
      reqDiristryTag = value;
      if (!string.IsNullOrEmpty(reqDiristryTag))
      { DiristryTag = reqDiristryTag; }
    }
    get { return reqDiristryTag; }
  }

  private string reqDiristryGuid = ESS;
  public string DiristryGuidReqst
  {
    set {
      reqDiristryGuid = value;
      if (!string.IsNullOrEmpty(reqDiristryGuid))
      { DiristryGuid = PdpGuid.ParseToNullable(reqDiristryGuid, NPDSCD.DiristryGuidDefault); }
    }
    get { return reqDiristryGuid; }
  }

  public void DiristryGuidResetFromReqstAndDeflt()
  {
    DiristryGuid = PdpGuid.ParseToNullable(DiristryGuidReqst, NPDSCD.DiristryGuidDefault);
  }

  // selected values for response settings

  private string? diristryTag = ESS;
  public string? DiristryTag
  {
    set {
      diristryTag = ValidateServiceTag(value, NPDSCD.DiristryTagConstraint);
      if (string.IsNullOrEmpty(diristryTag)) { diristryGuid = EGS; }
      else { diristryGuid = NPDSCD.NpdsServiceCache.GetByTag(diristryTag); }
    }
    get { return diristryTag; }
  }

  private Guid? diristryGuid = EGS;
  public Guid? DiristryGuid
  {
    set {
      diristryGuid = value;
      if (value.IsNullOrEmpty()) { diristryTag = ESS; }
      else { diristryTag = NPDSCD.NpdsServiceCache.GetByNullableGuid(diristryGuid); }
    }
    get { return diristryGuid; }
  }

} // end class

// end file