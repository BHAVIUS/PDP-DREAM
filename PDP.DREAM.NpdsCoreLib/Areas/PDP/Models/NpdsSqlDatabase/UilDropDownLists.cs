using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class UilDropDownLists
  {
    public IEnumerable<EntityTypeListItem>? EntityTypeList { get; set; } = null;

    public IEnumerable<InfosetPortalStatusListItem>? InfosetPortalStatusList { get; set; } = null;
    public IEnumerable<InfosetDoorsStatusListItem>? InfosetDoorsStatusList { get; set; } = null;

    public IList<RecordServiceListItem>? ServiceList { get; set; } = null;
    public IList<RecordDiristryListItem>? DiristryList { get; set; } = null;
    public IList<RecordRegistryListItem>? RegistryList { get; set; } = null;
    public IList<RecordDirectoryListItem>? DirectoryList { get; set; } = null;
    public IList<RecordRegistrarListItem>? RegistrarList { get; set; } = null;
	
    // maintain SelectListItem approach for ASP.NET Core MVC forms
	
    public IList<SelectListItem>? DiristryListMvc { get; set; } = null;
    public IList<SelectListItem>? RegistryListMvc { get; set; } = null;
    public IList<SelectListItem>? RegistrarListMvc { get; set; } = null;
    public IList<SelectListItem>? DirectoryListMvc { get; set; } = null;

    public IList<SelectListItem>? SupportingLabelList { get; set; } = null;
  }

}
