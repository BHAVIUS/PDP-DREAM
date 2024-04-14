// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public IEnumerable<OtherTextUxm?> ListEditableOtherTexts(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<OtherTextUxm?> result;
    try
    {
      IQueryable<PortalOtherText> qry = this.PortalOtherTexts;
      if (NPDSDC.ClientHasAdminMode || NPDSDC.ClientHasEditorMode)
      { qry = qry.Where(rr => (rr.RecordGuid == guidKey)); }
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
        .OrderBy(ss => ss.HasPriority).ThenByDescending(ss => ss.UpdatedOn)
        .ToList();
    }
    catch
    {
      result = Enumerable.Empty<OtherTextUxm?>();
    }
    return result;
  }

  public IEnumerable<PortalOtherText?> ListStorableOtherTexts(Guid? diristryGuid, short entityTypeCode, short fieldFormatCode, int listCount = 0)
  {
    // TODO: re-eval defaults, make available as settable option for each of 3 including supermax
    if (listCount == 0) { listCount = NPDSDC.PageListCount; }
    IEnumerable<PortalOtherText?> result;
    try
    {
      IEnumerable<CoreResrepLeaf> resreps = this.CoreResrepLeafs
        .Include((CoreResrepLeaf rr) => rr.PortalOtherTexts)
        .Where((CoreResrepLeaf rr) => (rr.RecordDiristryGuid == diristryGuid) &&
          (rr.EntityTypeCode == entityTypeCode) && // (rr.InfosetOtherTextsCount > 0) 
          (rr.PortalOtherTexts.Where((PortalOtherText ss) => (ss.FieldFormatCode == fieldFormatCode)).Count() > 0))
        .OrderByDescending((CoreResrepLeaf rr) => rr.RecordUpdatedOn)
        .Take(listCount)
        .ToList();
      result = resreps
       .Select((CoreResrepLeaf rr) => rr.PortalOtherTexts
       .Where((PortalOtherText ss) => (ss.FieldFormatCode == fieldFormatCode))
       .OrderBy(ss => ss.HasPriority).ThenByDescending(ss => ss.UpdatedOn)
       .FirstOrDefault())
       .ToList();
    }
    catch
    {
      result = Enumerable.Empty<PortalOtherText?>();
    }
    return result;
  }

} // end class

// end file