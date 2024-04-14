// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ServiceRestrictionOrUxm : ResrepRootModelBase
{
  public ServiceRestrictionOrUxm()
  {
    itemXnam = PdpAppConst.ServiceRestrictionOrItemXnam;
  }

  private Guid? rstrAndGuid = EGS;
  public Guid? RestrictionAndGuid
  {
    get { return rstrAndGuid; }
    set {
      if (value == null) { rstrAndGuid = EGS; } else { rstrAndGuid = value; }
      rstrAndGuidStr = rstrAndGuid.ToString();
    }
  }
  private string? rstrAndGuidStr = ESS;
  public string? RestrictionAndGuidStr
  {
    get { return rstrAndGuidStr; }
    set {
      if (value == null) { rstrAndGuidStr = ESS; } else { rstrAndGuidStr = value; }
      rstrAndGuid = PdpGuid.ParseToNullable(rstrAndGuidStr, EGS);
    }
  }

  public byte RestrictionAndHasIndex { get; set; } = 0;
  public byte RestrictionAndHasPriority { get; set; } = 0;
  public string? RestrictionName { get; set; } = ESS;

  private Guid? rstrOrGuid = EGS;
  public Guid? RestrictionOrGuid
  {
    get { return rstrOrGuid; }
    set {
      if (value == null) { rstrOrGuid = EGS; } else { rstrOrGuid = value; }
      rstrOrGuidStr = rstrOrGuid.ToString();
    }
  }
  private string? rstrOrGuidStr = ESS;
  public string? RestrictionOrGuidStr
  {
    get { return rstrOrGuidStr; }
    set {
      if (value == null) { rstrOrGuidStr = ESS; } else { rstrOrGuidStr = value; }
      rstrOrGuid = PdpGuid.ParseToNullable(rstrOrGuidStr, EGS);
    }
  }

  public byte RestrictionOrHasIndex { get; set; } = 0;
  public byte RestrictionOrHasPriority { get; set; } = 0;
  public string? RestrictionValue { get; set; } = ESS;

} // end class

// end file