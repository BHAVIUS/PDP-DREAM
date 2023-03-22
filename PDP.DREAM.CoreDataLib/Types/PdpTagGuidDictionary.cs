// PdpTagGuidDictionary.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpTagGuidDictionary : PdpBiDictionary<string, Guid>
{
  public Guid GetByNullableTag(string? theTag)
  {
    var theGuid = Guid.Empty;
    if (theTag != null) { TryGetByLeft(theTag, out theGuid); }
    return theGuid;
  }
  public Guid GetByTag(string theTag)
  {
    return GetByNullableTag((string?)theTag);
  }

  public string GetByNullableGuid(Guid? theGuid)
  {
    return GetByGuid((Guid)theGuid);
  }
  public string GetByGuid(Guid theGuid)
  {
    var theTag = string.Empty;
    if (theGuid != Guid.Empty) { TryGetByRight(theGuid, out theTag); }
    return theTag;
  }

}

