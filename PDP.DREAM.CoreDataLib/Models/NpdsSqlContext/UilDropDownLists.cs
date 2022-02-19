// UilDropDownLists.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace PDP.DREAM.CoreDataLib.Models
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
