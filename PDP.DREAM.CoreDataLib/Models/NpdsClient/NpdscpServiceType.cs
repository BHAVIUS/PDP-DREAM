// PrcServiceType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string reqServiceType = string.Empty;
  public string ServiceTypeReqst
  {
    set {
      reqServiceType = value;
      if (!string.IsNullOrEmpty(reqServiceType)) 
      { serviceType = ValidateServiceType(reqServiceType); }
    }
    get { return reqServiceType; }
  }

  // validated values

  private NpdsServiceType serviceType = NpdsServiceType.Nexus;
  public NpdsServiceType ServiceType
  {
    set { serviceType = ValidateServiceType(value); }
    get { return serviceType; }
  }

  // validators

  private NpdsServiceType ValidateServiceType(string strValue)
  {
    NpdsServiceType enmValue = PdpEnum<NpdsServiceType>.ParseString(strValue, NPDSSD.ServiceTypeDefault);
    enmValue = ValidateServiceType(enmValue);
    return enmValue;
  }
  private NpdsServiceType ValidateServiceType(NpdsServiceType enmValue)
  {
    // ATTN: see also ValidateSearchFilter()
    // SearchFilter should be set after setting ServiceType
    // use of switch below assures valid defaults for SearchFilter after changing ServiceType
    // SearchFilter may be changed from default, for example, it may be set to a
    // PORTAL Registry, DOORS Directory, Nexus Diristry, Scribe Registrar
    // when the ServiceType is a Scribe Registrar
    switch (enmValue)
    {
      case NpdsServiceType.Nexus:
        SearchFilter = NpdsSearchFilter.Diristry;
        break;
      case NpdsServiceType.PORTAL:
        SearchFilter = NpdsSearchFilter.Registry;
        break;
      case NpdsServiceType.DOORS:
        SearchFilter = NpdsSearchFilter.Directory;
        break;
      case NpdsServiceType.Scribe:
        SearchFilter = NpdsSearchFilter.Registrar;
        break;
      case NpdsServiceType.ACMS:
      case NpdsServiceType.Core:
        SearchFilter = NpdsSearchFilter.None;
        break;
      default:
        throw new Exception($"case not implemented for ServiceType value {enmValue.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
    }
    return enmValue;
  }

} // end class

// end file