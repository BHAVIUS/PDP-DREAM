// PrcServiceTitle.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  // validated values

  private string? serviceTitle = string.Empty;
  public string? ServiceTitle
  {
    set {
      if (!string.IsNullOrEmpty(value)) { serviceTitle = value; }
    }
    get {
      switch (SearchFilter)
      {
        case NpdsSearchFilter.Diristry:
          serviceTitle = $"{DiristryTag} Diristry";
          break;
        case NpdsSearchFilter.Registry:
          serviceTitle = $"{RegistryTag} Registry";
          break;
        case NpdsSearchFilter.Directory:
          serviceTitle = $"{DirectoryTag} Directory";
          break;
        case NpdsSearchFilter.Registrar:
          serviceTitle = $"{RegistrarTag} Registrar";
          break;
        case NpdsSearchFilter.AllTags:
          serviceTitle = NpdsSearchFilter.AllTags.ToString();
          break;
        case NpdsSearchFilter.AllGuids:
          serviceTitle = NpdsSearchFilter.AllGuids.ToString();
          break;
        case NpdsSearchFilter.None:
          serviceTitle = NpdsSearchFilter.None.ToString();
          break;
        default:
          throw new Exception("invalid ServiceType in ServiceTitle get");
      }
      return serviceTitle;
    }

  }

  // validators

} // end class

// end file