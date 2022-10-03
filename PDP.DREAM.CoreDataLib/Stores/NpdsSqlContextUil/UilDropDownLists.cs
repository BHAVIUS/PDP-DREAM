// UilDropDownLists.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace PDP.DREAM.CoreDataLib.Models;

public class UilDropDownLists
{
  public IList<EntityTypeListItem>? EntityTypeList { get; set; } = null;
  public IList<FieldFormatListItem>? FieldFormatList { get; set; } = null;
  public IList<InfosetPortalStatusListItem>? InfosetPortalStatusList { get; set; } = null;
  public IList<InfosetDoorsStatusListItem>? InfosetDoorsStatusList { get; set; } = null;

  // maintain for Telerik KendoUI grid forms
  public IList<RecordServiceListItem>? CoreServiceList { get; set; } = null;
  public IList<RecordDiristryListItem>? CoreDiristryList { get; set; } = null; // Nexus
  public IList<RecordRegistryListItem>? CoreRegistryList { get; set; } = null; // PORTAL
  public IList<RecordDirectoryListItem>? CoreDirectoryList { get; set; } = null; // DOORS
  public IList<RecordRegistrarListItem>? CoreRegistrarList { get; set; } = null; // Scribe


  // maintain registrar non-constrained (Core) SelectListItem approach for MVC forms
  public IList<SelectListItem>? CoreDiristryListMvc { get; set; } = null;
  public IList<SelectListItem>? CoreRegistryListMvc { get; set; } = null;
  public IList<SelectListItem>? CoreDirectoryListMvc { get; set; } = null;
  public IList<SelectListItem>? CoreRegistrarListMvc { get; set; } = null;


  // maintain registrar constrained (Regc) SelectListItem approach for MVC forms
  public IList<SelectListItem>? RegcDiristryListMvc { get; set; } = null;
  public IList<SelectListItem>? RegcRegistryListMvc { get; set; } = null;
  public IList<SelectListItem>? RegcDirectoryListMvc { get; set; } = null;
  public IList<SelectListItem>? RegcRegistrarListMvc { get; set; } = null;

  public IList<SelectListItem>? SupportingLabelList { get; set; } = null;

} // end class

// end file