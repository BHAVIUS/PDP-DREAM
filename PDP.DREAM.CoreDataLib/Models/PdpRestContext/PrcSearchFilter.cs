// PrcSearchFilter.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Reflection;

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpRestContext
{
  // in file NpdsConstants.cs
  // public enum SearchFilter { None = 0, Diristry, Registry, Directory, Registrar, AllTags, AllGuids }

  // default values

  public NpdsConst.SearchFilter SearchFilterDeflt
  { get { return defSearchFilter; } }
  private NpdsConst.SearchFilter defSearchFilter = NpdsServiceDefaults.Values.NpdsDefaultSearchFilter;

  // requested values

  public string SearchFilterReqst
  {
    set { reqSearchFilter = value; searchFilter = ValidateSearchFilter(reqSearchFilter); }
    get { return reqSearchFilter; }
  }
  private string reqSearchFilter = string.Empty;

  // validated values

  public NpdsConst.SearchFilter SearchFilter
  {
    set { searchFilter = ValidateSearchFilter(value); }
    get { return searchFilter; }
  }
  private NpdsConst.SearchFilter searchFilter = default;

  // validators

  public void ValidateSearchFilter()
  {
    searchFilter = ValidateSearchFilter(reqSearchFilter);
  }

  private NpdsConst.SearchFilter ValidateSearchFilter(string strValue)
  {
    NpdsConst.SearchFilter enmValue = NpdsConst.SearchFilter.None;
    switch (strValue.ToUpper())
    {
      case "NEXUS":
        enmValue = NpdsConst.SearchFilter.Diristry;
        break;
      case "PORTAL":
        enmValue = NpdsConst.SearchFilter.Registry;
        break;
      case "DOORS":
        enmValue = NpdsConst.SearchFilter.Directory;
        break;
      case "SCRIBE":
        enmValue = NpdsConst.SearchFilter.Registrar;
        break;
      default:
        enmValue = PdpEnum<NpdsConst.SearchFilter>.Parse(strValue, NpdsConst.SearchFilter.None);
        break;
    }
    enmValue = ValidateSearchFilter(enmValue);
    return enmValue;
  }
  private NpdsConst.SearchFilter ValidateSearchFilter(NpdsConst.SearchFilter enmValue)
  {
    switch (enmValue)
    {
      case NpdsConst.SearchFilter.Diristry:
        PRC.DiristryTag = PRC.DiristryTagReqst;
        PRC.RegistryTag = string.Empty;
        PRC.DirectoryTag = string.Empty;
        PRC.RegistrarTag = string.Empty;
        break;
      case NpdsConst.SearchFilter.Registry:
        PRC.DiristryTag = string.Empty;
        PRC.RegistryTag = PRC.RegistryTagReqst;
        PRC.DirectoryTag = string.Empty;
        PRC.RegistrarTag = string.Empty;
        break;
      case NpdsConst.SearchFilter.Directory:
        PRC.DiristryTag = string.Empty;
        PRC.RegistryTag = string.Empty;
        PRC.DirectoryTag = PRC.DirectoryTagReqst;
        PRC.RegistrarTag = string.Empty;
        break;
      case NpdsConst.SearchFilter.Registrar:
        PRC.DiristryTagReqst = string.Empty;
        PRC.RegistryTagReqst = string.Empty;
        PRC.DirectoryTagReqst = string.Empty;
        PRC.RegistrarTag = PRC.RegistrarTagReqst;
        break;
      case NpdsConst.SearchFilter.AllTags:
        PRC.DiristryTag = PRC.DiristryTagReqst;
        PRC.RegistryTag = PRC.RegistryTagReqst;
        PRC.DirectoryTag = PRC.DirectoryTagReqst;
        PRC.RegistrarTag = PRC.RegistrarTagReqst;
        break;
      case NpdsConst.SearchFilter.AllGuids:
        PRC.DiristryGuidResetFromReqst();
        PRC.RegistryGuidResetFromReqst();
        PRC.DirectoryGuidResetFromReqst();
        PRC.RegistrarGuidResetFromReqst();
        break;
      case NpdsConst.SearchFilter.None:
        PRC.DiristryTag = string.Empty;
        PRC.RegistryTag = string.Empty;
        PRC.DirectoryTag = string.Empty;
        PRC.RegistrarTag = string.Empty;
        break;
      default:
        throw new Exception($"case not implemented for SearchFilter value {enmValue.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
    }
    return enmValue;
  }

}
