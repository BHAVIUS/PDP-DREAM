// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ResrepSnapshotUxm CheckSnapshot(ResrepSnapshotUxm editObj)
  {
    editObj.NdisElemMsg = ESS;
    return editObj;
  }

  public virtual byte CheckSnapshots(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return CheckSnapshots(ref rrr);
  }
  public virtual byte CheckSnapshots(ref CoreResrepLeafUxm rrr)
  {
    byte statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    // TODO: complete this check function and other check functions
    var items = ListEditableResrepSnapshots(recordGuid).Select(r => r.ResrepSnapshot);
    if (items.Count() > 0)
    {
      statusCode = NPDSCD.InfosetStatusValid.ECode;
    }
    else
    {
      statusCode = NPDSCD.InfosetStatusNone.ECode;
    }
    rrr.NexusSnapshotsStatusCode = statusCode;
    return statusCode;
  }

} // end class

// end file