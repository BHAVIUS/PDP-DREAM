// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // ListEditables //
  public IEnumerable<ResrepServiceUxm> ListEditableResrepServices(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<ResrepServiceUxm> result;
    try
    {
      IQueryable<CoreResrepService> qry = this.CoreResrepServices;
      if (NPDSDC.ClientHasAdminMode || NPDSDC.ClientHasEditorMode)
      { qry = qry.Where(r => (r.RecordGuid == guidKey)); }
      else
      {
        if (isLimited) { qry = qry.Where(r => (r.RecordGuid == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuid == NPDSDC.NpdsAgentGuid)); }
        else { qry = qry.Where(r => (r.RecordGuid == guidKey) && (r.IsDeleted == false)); }
      }
      result = qry.OrderBy(r => r.HasPriority).ToEditable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ResrepServiceUxm>();
    }
    return result;
  }

  // ListStorables //

} // end class

// end file