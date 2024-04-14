// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  // validated values

  private string? serviceTitle = ESS;
  public string? ServiceTitle
  {
    set {
      if (!string.IsNullOrEmpty(value)) { serviceTitle = value; }
    }
    get {
      switch (SearchFilter.EName)
      {
        case DdeSearchFilter.Diristry:
          serviceTitle = $"{DiristryTag} Diristry";
          break;
        case DdeSearchFilter.Registry:
          serviceTitle = $"{RegistryTag} Registry";
          break;
        case DdeSearchFilter.Directory:
          serviceTitle = $"{DirectoryTag} Directory";
          break;
        case DdeSearchFilter.Registrar:
          serviceTitle = $"{RegistrarTag} Registrar";
          break;
        case DdeSearchFilter.Services:
        case DdeSearchFilter.Persons:
        case DdeSearchFilter.Organizations:
        case DdeSearchFilter.AllTags:
        case DdeSearchFilter.AllGuids:
        case DdeSearchFilter.None:
          serviceTitle = $"NPDS {ServiceType.EName} = '{ServiceTag}' by Filter = '{SearchFilter.EName}'";
          break;
        default:
          throw new Exception("invalid SearchFilter in ServiceTitle get");
      }
      return serviceTitle;
    }

  }

  // validators

} // end class

// end file