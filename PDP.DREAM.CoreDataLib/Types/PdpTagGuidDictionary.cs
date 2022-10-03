// PdpTagGuidDictionary.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpTagGuidDictionary : PdpBiDictionary<string, Guid>
{
  public Guid GetByTag(string theTag)
  {
    TryGetByLeft(theTag, out Guid theGuid);
    return theGuid;
  }
  public Guid GetByNullableTag(string? theTag)
  {
    return GetByTag((string)theTag);
  }

  public string GetByGuid(Guid theGuid)
  {
    TryGetByRight(theGuid, out string theTag);
    return theTag;
  }
  public string GetByNullableGuid(Guid? theGuid)
  {
    return GetByGuid((Guid)theGuid);
  }

}

