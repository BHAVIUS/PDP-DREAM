// PrcServiceTitle.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // default values

  // requested values

  // validated values
  public string ServiceTitle
  {
    get
    {
      switch (ServiceType)
      {
        case PdpAppConst.NpdsServiceType.Nexus:
          serviceTitle = $"{DiristryTag} Diristry";
          break;
        case PdpAppConst.NpdsServiceType.PORTAL:
          serviceTitle = $"{RegistryTag} Registry";
          break;
        case PdpAppConst.NpdsServiceType.DOORS:
          serviceTitle = $"{DirectoryTag} Directory";
          break;
        case PdpAppConst.NpdsServiceType.Scribe:
          serviceTitle = $"{RegistrarTag} Registrar";
          break;
        case PdpAppConst.NpdsServiceType.Core:
          serviceTitle = "NPDS Core Service";
          break;
        default:
          throw new Exception("invalid ServiceType in ServiceTitle get");
      }
      return serviceTitle;
    }
  }
  private string serviceTitle = string.Empty;

  // validators

}

