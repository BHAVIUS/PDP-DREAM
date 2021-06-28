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
