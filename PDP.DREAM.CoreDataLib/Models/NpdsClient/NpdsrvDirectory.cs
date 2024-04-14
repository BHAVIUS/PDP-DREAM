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

  private string reqDirectoryTag = ESS;
  public string DirectoryTagReqst
  {
    set {
      reqDirectoryTag = value;
      if (!string.IsNullOrEmpty(reqDirectoryTag))
      { DirectoryTag = reqDirectoryTag; }
    }
    get { return reqDirectoryTag; }
  }

  private string reqDirectoryGuid = ESS;
  public string DirectoryGuidReqst
  {
    set {
      reqDirectoryGuid = value;
      if (!string.IsNullOrEmpty(reqDirectoryGuid))
      { DirectoryGuid = PdpGuid.ParseToNullable(reqDirectoryGuid, NPDSCD.DirectoryGuidDefault); }
    }
    get { return reqDirectoryGuid; }
  }

  public void DirectoryGuidResetFromReqstAndDeflt()
  {
    DirectoryGuid = PdpGuid.ParseToNullable(DirectoryGuidReqst, NPDSCD.DirectoryGuidDefault);
  }

  // selected values for response settings

  private string? directoryTag = ESS;
  public string? DirectoryTag
  {
    set {
      directoryTag = ValidateServiceTag(value, NPDSCD.DirectoryTagConstraint);
      if (string.IsNullOrEmpty(directoryTag)) { directoryGuid = EGS; }
      else { directoryGuid = NPDSCD.NpdsServiceCache.GetByTag(directoryTag); }
    }
    get { return directoryTag; }
  }

  private Guid? directoryGuid = EGS;
  public Guid? DirectoryGuid
  {
    set {
      directoryGuid = value;
      directoryTag = PdpAppStatus.NPDSCD.NpdsServiceCache.GetByNullableGuid(directoryGuid);
    }
    get { return directoryGuid; }
  }

} // end class

// end file