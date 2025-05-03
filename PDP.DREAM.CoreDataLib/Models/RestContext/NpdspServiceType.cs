// PrcServiceType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // in class PDP.DREAM.CoreDataLib.Models.PdpAppConst
  // Core root, Nexus diristry, PORTAL registry, DOORS directory, Scribe registrar
  // public enum NpdsServiceType { Core = 0, Nexus = 1, PORTAL = 2, DOORS = 3, Scribe = 4 };
  // public enum NpdsServerType { Root = 0, Diristry = 1, Registry = 2, Directory = 3, Registrar = 4 };

  // default values

  public PdpAppConst.NpdsServiceType ServiceTypeDeflt { get; }
    = PdpAppStatus.NPDSSD.NpdsDefaultServiceType;

  // requested values

  public string ServiceTypeReqst
  {
    set {
      reqServiceType = value;
      if (!string.IsNullOrEmpty(reqServiceType)) { serviceType = ValidateServiceType(reqServiceType); }
    }
    get { return reqServiceType; }
  }
  private string reqServiceType = string.Empty;

  // validated values

  public PdpAppConst.NpdsServiceType ServiceType
  {
    set { serviceType = ValidateServiceType(value); }
    get { return serviceType; }
  }
  private PdpAppConst.NpdsServiceType serviceType = PdpAppConst.NpdsServiceType.Nexus;

  // validators

  private PdpAppConst.NpdsServiceType ValidateServiceType(string strValue)
  {
    PdpAppConst.NpdsServiceType enmValue = PdpEnum<PdpAppConst.NpdsServiceType>.ParseString(strValue, PdpAppConst.NpdsServiceType.Nexus);
    enmValue = ValidateServiceType(enmValue);
    return enmValue;
  }
  private PdpAppConst.NpdsServiceType ValidateServiceType(PdpAppConst.NpdsServiceType enmValue)
  {
    switch (enmValue)
    {
      case PdpAppConst.NpdsServiceType.Nexus:
        SearchFilterReqst = PdpAppConst.NpdsSearchFilter.Diristry.ToString();
        break;
      case PdpAppConst.NpdsServiceType.PORTAL:
        SearchFilterReqst = PdpAppConst.NpdsSearchFilter.Registry.ToString();
        break;
      case PdpAppConst.NpdsServiceType.DOORS:
        SearchFilterReqst = PdpAppConst.NpdsSearchFilter.Directory.ToString();
        break;
      case PdpAppConst.NpdsServiceType.Scribe:
        SearchFilterReqst = PdpAppConst.NpdsSearchFilter.Registrar.ToString();
        break;
      default:
        throw new Exception($"case not implemented for ServiceType value {enmValue.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
    }
    return enmValue;
  }

}
