// PdpTagGuidDictionary.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

namespace PDP.DREAM.NpdsCoreLib.Types
{

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

}
