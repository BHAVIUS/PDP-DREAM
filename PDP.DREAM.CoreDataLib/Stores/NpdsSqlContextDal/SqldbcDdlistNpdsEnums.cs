// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public IList<SelectListItem> GetEntityTypeList()
  {
    // query from CoreEntityTypeItem
    IEnumerable<CoreEntityTypeItem> dalItems = this.CoreEntityTypeItems.AsEnumerable()
      .Where(dali => (
      (dali.TypeEditedByAuthor == true && NPDSDC.ClientHasAuthorMode == true && dali.TypeIsComponent == false) ||
      (dali.TypeEditedByEditor == true && NPDSDC.ClientHasEditorMode == true && dali.TypeIsComponent == false) ||
      (dali.TypeEditedByAdmin == true && NPDSDC.ClientHasAdminMode == true)
      )).OrderBy(dali => dali.TypeName);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.CodeKey == NPDSCD.EntityTypeDefault.ECode)
        select new SelectListItem
        {
          Text = dali.TypeName,
          Value = dali.CodeKey.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetFieldFormatList()
  {
    // query from CoreEntityTypeItem
    IEnumerable<CoreFieldFormatItem> dalItems = this.CoreFieldFormatItems.AsEnumerable()
      .Where(dali => ((0 < dali.CodeKey) && (dali.CodeKey != 100) && (dali.CodeKey < 255)))
      .OrderBy(dali => dali.FormatName);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems =
      from dali in dalItems
      let itemSelected = (dali.CodeKey == NPDSCD.FieldFormatDefault.ECode)
      select new SelectListItem
      {
        Text = dali.FormatName,
        Value = dali.CodeKey.ToString(),
        Selected = itemSelected
      };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetInfosetPortalStatusList()
  {
    // query from CoreInfosetStatusItem
    IEnumerable<CoreInfosetStatusItem> dalItems = this.CoreInfosetStatusItems.AsEnumerable()
      .Where(dali => (
      (dali.StatusEditedByAuthor == true && NPDSDC.ClientHasAuthorMode == true) ||
      (dali.StatusEditedByEditor == true && NPDSDC.ClientHasEditorMode == true) ||
      (dali.StatusEditedByAdmin == true && NPDSDC.ClientHasAdminMode == true)
      )).OrderBy(dali => dali.StatusName);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.CodeKey == NPDSCD.InfosetStatusNewItem.ECode)
        select new SelectListItem
        {
          Text = dali.StatusName,
          Value = dali.CodeKey.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetInfosetDoorsStatusList()
  {
    // query from CoreInfosetStatusItem
    IEnumerable<CoreInfosetStatusItem> dalItems = this.CoreInfosetStatusItems.AsEnumerable()
      .Where(dali => (
      (dali.StatusEditedByAuthor == true && NPDSDC.ClientHasAuthorMode == true) ||
      (dali.StatusEditedByEditor == true && NPDSDC.ClientHasEditorMode == true) ||
      (dali.StatusEditedByAdmin == true && NPDSDC.ClientHasAdminMode == true)
      )).OrderBy(dali => dali.StatusName);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.CodeKey == NPDSCD.InfosetStatusNewItem.ECode)
        select new SelectListItem
        {
          Text = dali.StatusName,
          Value = dali.CodeKey.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

} // end class

// end file