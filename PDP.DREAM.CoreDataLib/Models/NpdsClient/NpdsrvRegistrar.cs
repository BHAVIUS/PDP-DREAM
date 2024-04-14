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

  private string reqRegistrarTag = ESS;
  public string RegistrarTagReqst
  {
    set {
      reqRegistrarTag = value;
      if (!string.IsNullOrEmpty(reqRegistrarTag))
      { RegistrarTag = reqRegistrarTag; }
    }
    get { return reqRegistrarTag; }
  }

  private string reqRegistrarGuid = ESS;
  public string RegistrarGuidReqst
  {
    set {
      reqRegistrarGuid = value;
      if (!string.IsNullOrEmpty(reqRegistrarGuid))
      { RegistrarGuid = PdpGuid.ParseToNullable(reqRegistrarGuid, NPDSCD.RegistrarGuidDefault); }
    }
    get { return reqRegistrarGuid; }
  }

  public void RegistrarGuidResetFromReqstAndDeflt()
  {
    RegistrarGuid = PdpGuid.ParseToNullable(RegistrarGuidReqst, NPDSCD.RegistrarGuidDefault);
  }

  // selected values for response settings

  private string? registrarTag = ESS;
  public string? RegistrarTag
  {
    set {
      registrarTag = ValidateServiceTag(value, NPDSCD.RegistrarTagConstraint);
      if (string.IsNullOrEmpty(registrarTag)) { registrarGuid = EGS; }
      else { registrarGuid = NPDSCD.NpdsServiceCache.GetByTag(registrarTag); }
    }
    get { return registrarTag; }
  }

  private Guid? registrarGuid = EGS;
  public Guid? RegistrarGuid
  {
    set {
      registrarGuid = value;
      if (value.IsNullOrEmpty()) { registrarTag = ESS; }
      else { registrarTag = NPDSCD.NpdsServiceCache.GetByNullableGuid(registrarGuid); }
    }
    get { return registrarGuid; }
  }

} // end class

// end file