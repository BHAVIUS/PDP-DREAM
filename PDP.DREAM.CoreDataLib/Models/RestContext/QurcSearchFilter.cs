// PrcSearchFilter.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Reflection;

using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // in file NpdsConstants.cs
  // public enum SearchFilter { None = 0, Diristry, Registry, Directory, Registrar, AllTags, AllGuids }

  // default values

  public NpdsSearchFilter SearchFilterDeflt
  { get { return defSearchFilter; } }
  private NpdsSearchFilter defSearchFilter = NPDSSD.NpdsDefaultSearchFilter;

  // requested values

  public string SearchFilterReqst
  {
    set { reqSearchFilter = value; searchFilter = ValidateSearchFilter(reqSearchFilter); }
    get { return reqSearchFilter; }
  }
  private string reqSearchFilter = string.Empty;

  // validated values

  public NpdsSearchFilter SearchFilter
  {
    set { searchFilter = ValidateSearchFilter(value); }
    get { return searchFilter; }
  }
  private NpdsSearchFilter searchFilter = default;

  // validators

  public void ValidateSearchFilter()
  {
    searchFilter = ValidateSearchFilter(reqSearchFilter);
  }

  private NpdsSearchFilter ValidateSearchFilter(string strValue)
  {
    NpdsSearchFilter enmValue = NpdsSearchFilter.None;
    switch (strValue.ToUpper())
    {
      case "NEXUS":
        enmValue = NpdsSearchFilter.Diristry;
        break;
      case "PORTAL":
        enmValue = NpdsSearchFilter.Registry;
        break;
      case "DOORS":
        enmValue = NpdsSearchFilter.Directory;
        break;
      case "SCRIBE":
        enmValue = NpdsSearchFilter.Registrar;
        break;
      default:
        enmValue = PdpEnum<NpdsSearchFilter>.ParseString(strValue, NpdsSearchFilter.None);
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
        DiristryGuidResetFromReqst();
        RegistryGuidResetFromReqst();
        DirectoryGuidResetFromReqst();
        RegistrarGuidResetFromReqst();
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