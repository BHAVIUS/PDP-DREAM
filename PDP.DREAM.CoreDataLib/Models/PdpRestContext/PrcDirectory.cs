// PrcDirectory.cs 
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

    public string DirectoryTagDeflt
    {
      set
      {
        defDirectoryTag = value;
        defDirectoryGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(defDirectoryTag);
      }
      get { return defDirectoryTag; }
    }
    private string defDirectoryTag = NpdsServiceDefaults.Values.NpdsDefaultDirectoryTag;

    public Guid DirectoryGuidDeflt
    { get { return defDirectoryGuid; } }
    private Guid defDirectoryGuid = NpdsServiceDefaults.Values.NpdsDefaultDirectoryGuid;

    // requested values from request options (URL routes or querystrings)

    public string DirectoryTagReqst
    {
      set
      {
        reqDirectoryTag = value;
        if (!string.IsNullOrEmpty(reqDirectoryTag)) { DirectoryTag = reqDirectoryTag; }
      }
      get { return reqDirectoryTag; }
    }
    private string reqDirectoryTag = string.Empty;

    public string DirectoryGuidReqst
    {
      set
      {
        reqDirectoryGuid = value;
        if (!string.IsNullOrEmpty(reqDirectoryGuid)) { DirectoryGuid = PdpGuid.Parse(reqDirectoryGuid, DirectoryGuidDeflt); }
      }
      get { return reqDirectoryGuid; }
    }
    private string reqDirectoryGuid = string.Empty;

    public void DirectoryGuidResetFromReqst()
    {
      DirectoryGuid = PdpGuid.Parse(DirectoryGuidReqst, DirectoryGuidDeflt);
    }

    // selected values for response settings

    public string DirectoryTag
    {
      set
      {
        directoryTag = ValidateServiceTag(value, NpdsServiceDefaults.Values.NpdsConstraintDirectoryTag);
        directoryGuid = NpdsServiceDefaults.Values.NpdsServiceCache.GetByTag(directoryTag);
      }
      get
      {
        if (string.IsNullOrEmpty(directoryTag)) { directoryTag = DirectoryTagDeflt; }
        return directoryTag;
      }
    }
    private string directoryTag = string.Empty;

    public Guid? DirectoryGuid
    {
      set
      {
        directoryGuid = value;
        directoryTag = NpdsServiceDefaults.Values.NpdsServiceCache.GetByNullableGuid(directoryGuid);
      }
      get
      {
        if (PdpGuid.IsNullOrEmpty(directoryGuid)) { directoryGuid = DirectoryGuidDeflt; }
        return directoryGuid;
      }
    }
    private Guid? directoryGuid = Guid.Empty;

  }

}
