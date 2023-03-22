// NpdspcDirectory.cs 
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

  private string reqDirectoryTag = string.Empty;
  public string DirectoryTagReqst
  {
    set {
      reqDirectoryTag = value;
      if (!string.IsNullOrEmpty(reqDirectoryTag))
      { DirectoryTag = reqDirectoryTag; }
    }
    get { return reqDirectoryTag; }
  }

  private string reqDirectoryGuid = string.Empty;
  public string DirectoryGuidReqst
  {
    set {
      reqDirectoryGuid = value;
      if (!string.IsNullOrEmpty(reqDirectoryGuid))
      { DirectoryGuid = PdpGuid.ParseToNullable(reqDirectoryGuid, NPDSSD.DirectoryGuidDefault); }
    }
    get { return reqDirectoryGuid; }
  }

  public void DirectoryGuidResetFromReqstAndDeflt()
  {
    DirectoryGuid = PdpGuid.ParseToNullable(DirectoryGuidReqst, NPDSSD.DirectoryGuidDefault);
  }

  // selected values for response settings

  private string? directoryTag = string.Empty;
  public string? DirectoryTag
  {
    set {
      directoryTag = ValidateServiceTag(value, NPDSSD.DirectoryTagConstraint);
      if (string.IsNullOrEmpty(directoryTag)) { directoryGuid = Guid.Empty; }
      else { directoryGuid = NPDSSD.NpdsServiceCache.GetByTag(directoryTag); }
    }
    get { return directoryTag; }
  }

  private Guid? directoryGuid = Guid.Empty;
  public Guid? DirectoryGuid
  {
    set {
      directoryGuid = value;
      directoryTag = PdpAppStatus.NPDSSD.NpdsServiceCache.GetByNullableGuid(directoryGuid);
    }
    get { return directoryGuid; }
  }

} // end class

// end file