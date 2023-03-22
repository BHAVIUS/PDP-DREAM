// NpdscpRegistrar.cs 
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

  private string reqRegistrarTag = string.Empty;
  public string RegistrarTagReqst
  {
    set {
      reqRegistrarTag = value;
      if (!string.IsNullOrEmpty(reqRegistrarTag))
      { RegistrarTag = reqRegistrarTag; }
    }
    get { return reqRegistrarTag; }
  }

  private string reqRegistrarGuid = string.Empty;
  public string RegistrarGuidReqst
  {
    set {
      reqRegistrarGuid = value;
      if (!string.IsNullOrEmpty(reqRegistrarGuid))
      { RegistrarGuid = PdpGuid.ParseToNullable(reqRegistrarGuid, NPDSSD.RegistrarGuidDefault); }
    }
    get { return reqRegistrarGuid; }
  }

  public void RegistrarGuidResetFromReqstAndDeflt()
  {
    RegistrarGuid = PdpGuid.ParseToNullable(RegistrarGuidReqst, NPDSSD.RegistrarGuidDefault);
  }

  // selected values for response settings

  private string? registrarTag = string.Empty;
  public string? RegistrarTag
  {
    set {
      registrarTag = ValidateServiceTag(value, NPDSSD.RegistrarTagConstraint);
      if (string.IsNullOrEmpty(registrarTag)) { registrarGuid = Guid.Empty; }
      else { registrarGuid = NPDSSD.NpdsServiceCache.GetByTag(registrarTag); }
    }
    get { return registrarTag; }
  }

  private Guid? registrarGuid = Guid.Empty;
  public Guid? RegistrarGuid
  {
    set {
      registrarGuid = value;
      if (value.IsNullOrEmpty()) { registrarTag = string.Empty; }
      else { registrarTag = NPDSSD.NpdsServiceCache.GetByNullableGuid(registrarGuid); }
    }
    get { return registrarGuid; }
  }

} // end class

// end file