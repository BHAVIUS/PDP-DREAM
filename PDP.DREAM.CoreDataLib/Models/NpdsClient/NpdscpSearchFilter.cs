// NpdscpSearchFilter.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // TODO: re-evaluate alternate name SelectType for SearchFilter
  // current: ServerType, ServiceType, SearchFilter, ServiceTag
  // alternate 1: ServerType, ServiceType, SelectType, ServiceTag
  // alternate 2: ServerType, ServiceType, SelectFilter, ServiceTag
  // if keep current SearchFilter, then SearchFilter also associated with SearchScope?

  // requested values

  private string reqSearchFilter = string.Empty;
  public string SearchFilterReqst
  {
    set { reqSearchFilter = value; searchFilter = ValidateSearchFilter(reqSearchFilter); }
    get { return reqSearchFilter; }
  }

  // validated values

  private NpdsSearchFilter searchFilter = default;
  public NpdsSearchFilter SearchFilter
  {
    set { searchFilter = ValidateSearchFilter(value); }
    get { return searchFilter; }
  }

  // validators

  private NpdsSearchFilter ValidateSearchFilter(string strValue)
  {
    NpdsSearchFilter enmValue = NpdsSearchFilter.None;
    switch (strValue.ToLower())
    {
      case "diristry":
        enmValue = NpdsSearchFilter.Diristry;
        break;
      case "registry":
        enmValue = NpdsSearchFilter.Registry;
        break;
      case "directory":
        enmValue = NpdsSearchFilter.Directory;
        break;
      case "registrar":
        enmValue = NpdsSearchFilter.Registrar;
        break;
      case "none":
        enmValue = NpdsSearchFilter.None;
        break;
      default:
        enmValue = PdpEnum<NpdsSearchFilter>.ParseString(strValue, NPDSSD.SearchFilterDefault);
        break;
    }
    enmValue = ValidateSearchFilter(enmValue);
    return enmValue;
  }
  private NpdsSearchFilter ValidateSearchFilter(NpdsSearchFilter enmValue)
  {
    switch (enmValue)
    {
      case NpdsSearchFilter.Diristry:
        DiristryTag = DiristryTagReqst;
        RegistryTag = string.Empty;
        DirectoryTag = string.Empty;
        RegistrarTag = string.Empty;
        break;
      case NpdsSearchFilter.Registry:
        DiristryTag = string.Empty;
        RegistryTag = RegistryTagReqst;
        DirectoryTag = string.Empty;
        RegistrarTag = string.Empty;
        break;
      case NpdsSearchFilter.Directory:
        DiristryTag = string.Empty;
        RegistryTag = string.Empty;
        DirectoryTag = DirectoryTagReqst;
        RegistrarTag = string.Empty;
        break;
      case NpdsSearchFilter.Registrar:
        DiristryTagReqst = string.Empty;
        RegistryTagReqst = string.Empty;
        DirectoryTagReqst = string.Empty;
        RegistrarTag = RegistrarTagReqst;
        break;
      case NpdsSearchFilter.AllTags:
        DiristryTag = DiristryTagReqst;
        RegistryTag = RegistryTagReqst;
        DirectoryTag = DirectoryTagReqst;
        RegistrarTag = RegistrarTagReqst;
        break;
      case NpdsSearchFilter.AllGuids:
        DiristryGuidResetFromReqstAndDeflt();
        RegistryGuidResetFromReqstAndDeflt();
        DirectoryGuidResetFromReqstAndDeflt();
        RegistrarGuidResetFromReqstAndDeflt();
        break;
      case NpdsSearchFilter.None:
        DiristryTag = string.Empty;
        RegistryTag = string.Empty;
        DirectoryTag = string.Empty;
        RegistrarTag = string.Empty;
        break;
      default:
        throw new Exception($"case not implemented for SearchFilter value {enmValue.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
    }
    return enmValue;
  }

} // end class

// end file