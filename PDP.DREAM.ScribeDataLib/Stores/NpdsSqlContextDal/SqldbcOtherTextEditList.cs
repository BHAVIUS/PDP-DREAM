// SqldbcUilOtherTextEditList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public IEnumerable<OtherTextEditModel?> ListEditableOtherTexts(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<OtherTextEditModel?> result;
    try
    {
      IQueryable<NexusOtherText> qry = this.NexusOtherTexts;
      if (QURC.ClientHasAdminAccess || QURC.ClientHasEditorAccess)
      { qry = qry.Where(rr => (rr.RecordGuidRef == guidKey)); }
      else
      {
        if (isLimited)
        {
          qry = qry.Where(ss => (ss.RecordGuidRef == guidKey) &&
            (ss.IsDeleted == false) && (ss.UpdatedByAgentGuidRef == QURC.QebAgentGuid));
        }
        else
        {
          qry = qry.Where(ss => (ss.RecordGuidRef == guidKey) &&
            (ss.IsDeleted == false));
        }
      }
      result = qry.ToEditable().AsEnumerable()
        .OrderBy(ss => ss.HasPriority).ThenByDescending(ss => ss.UpdatedOn)
        .ToList();
    }
    catch
    {
      result = Enumerable.Empty<OtherTextEditModel?>();
    }
    return result;
  }

  public IEnumerable<NexusOtherText?> ListStorableOtherTexts(Guid diristryGuid, short entityTypeCode, short fieldFormatCode, int listCount = 0)
  {
    // TODO: re-eval defaults, make available as settable option for each of 3 including supermax
    if (listCount == 0) { listCount = QURC.ListCount; }
    IEnumerable<NexusOtherText?> result;
    try
    {
      IEnumerable<NexusResrepLeaf> resreps = this.NexusResrepLeafs
        .Include((NexusResrepLeaf rr) => rr.NexusOtherTexts)
        .Where((NexusResrepLeaf rr) => (rr.RecordDiristryGuidRef == diristryGuid) &&
          (rr.EntityTypeCodeRef == entityTypeCode) && // (rr.InfosetOtherTextsCount > 0) 
          (rr.NexusOtherTexts.Where((NexusOtherText ss) => (ss.FieldFormatCodeRef == fieldFormatCode)).Count() > 0))
        .OrderByDescending((NexusResrepLeaf rr) => rr.RecordUpdatedOn)
        .Take(listCount)
        .ToList();
      result = resreps
       .Select((NexusResrepLeaf rr) => rr.NexusOtherTexts
       .Where((NexusOtherText ss) => (ss.FieldFormatCodeRef == fieldFormatCode))
       .OrderBy(ss => ss.HasPriority).ThenByDescending(ss => ss.UpdatedOn)
       .FirstOrDefault())
       .ToList();
    }
    catch
    {
      result = Enumerable.Empty<NexusOtherText?>();
    }
    return result;
  }

} // end class

// end file