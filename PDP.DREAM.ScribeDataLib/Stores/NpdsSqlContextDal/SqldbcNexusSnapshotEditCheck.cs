// SqldbcUilResrepSnapshotEditCheck.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public NexusSnapshotEditModel CheckSnapshot(NexusSnapshotEditModel editObj)
  {
    editObj.PdpStatusMessage = string.Empty;
    return editObj;
  }

  public virtual short CheckSnapshots(Guid recordGuid)
  {
    var rrr = GetEditableResrepStemByRKey(recordGuid);
    return CheckSnapshots(ref rrr);
  }
  public virtual short CheckSnapshots(ref NexusResrepEditModel rrr)
  {
    short statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    // TODO: complete this check function and other check functions
    var items = ListEditableSnapshots(recordGuid).Select(r => r.NexusSnapshot);
    if (items.Count() > 0)
    {
      statusCode = (short)PdpAppConst.NpdsInfosetStatus.Unknown;
    }
    else
    {
      statusCode = (short)PdpAppConst.NpdsInfosetStatus.None;
    }
    rrr.NexusSnapshotsStatusCode = statusCode;
    return statusCode;
  }

}

