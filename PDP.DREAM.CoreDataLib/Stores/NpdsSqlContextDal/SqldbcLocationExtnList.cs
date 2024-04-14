// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public IEnumerable<LocationExtnUxm?> ListEditableLocationExtns(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<LocationExtnUxm?> result;
    try
    {
      IQueryable<DoorsLocationExtn> qry = this.DoorsLocationExtns;
      if (NPDSDC.ClientHasAdminMode || NPDSDC.ClientHasEditorMode)
      { qry = qry.Where(ss => (ss.RecordGuid == guidKey)); }
      else
      {
        if (isLimited)
        {
          qry = qry.Where(ss => (ss.RecordGuid == guidKey) &&
            (ss.IsDeleted == false) && (ss.UpdatedByAgentGuid == NPDSDC.NpdsAgentGuid));
        }
        else
        {
          qry = qry.Where(ss => (ss.RecordGuid == guidKey) &&
            (ss.IsDeleted == false));
        }
      }
     result = qry.ToEditable().AsEnumerable()
        .OrderBy(ss => ss.HasPriority)
        .ThenByDescending(ss => ss.UpdatedOn)
        .ToList();
    }
    catch (SqlException exc)
    {
      var msg = exc.Message;
      result = Enumerable.Empty<LocationExtnUxm?>();
    }
    return result;
  }

} // end class

// end file