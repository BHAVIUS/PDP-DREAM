// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: re-eval conventions for select item lists
//  currently suffix "Mvc" associated with those lists that are IList<SelectListItem>?

public class PdpDropDownLists
{
  // ATTN: MVC object typed lists with SelectListItem types
  // maintain for use with MVC forms and AspNetCore
  // TODO: rebuild for use with Blazor components, what will be preferred type?
  // TODO: then migrate to use of Blazor components in Telerik KendoUI grid forms

  public IList<SelectListItem>? EntityTypeList { get; set; } = null;
  public IList<SelectListItem>? FieldFormatList { get; set; } = null;
  public IList<SelectListItem>? InfosetPortalStatusList { get; set; } = null;
  public IList<SelectListItem>? InfosetDoorsStatusList { get; set; } = null;

  // NPDS registrar non-constrained (Core*) SelectListItem approach for MVC forms
  public IList<SelectListItem>? CoreDiristryList { get; set; } = null;
  public IList<SelectListItem>? CoreRegistryList { get; set; } = null;
  public IList<SelectListItem>? CoreDirectoryList { get; set; } = null;
  public IList<SelectListItem>? CoreRegistrarList { get; set; } = null;

  // NPDS registrar constrained (Regc*) SelectListItem approach for MVC forms
  public IList<SelectListItem>? RegcDiristryList { get; set; } = null;
  public IList<SelectListItem>? RegcRegistryList { get; set; } = null;
  public IList<SelectListItem>? RegcDirectoryList { get; set; } = null;
  public IList<SelectListItem>? RegcRegistrarList { get; set; } = null;

  // NPDS infosubset lists
  public IList<SelectListItem>? RegcSupportingLabelList { get; set; } = null;

  // ACMS object lists
  public IList<SelectListItem>? DaylogTypeList { get; set; } = null;

} // end class

// end file