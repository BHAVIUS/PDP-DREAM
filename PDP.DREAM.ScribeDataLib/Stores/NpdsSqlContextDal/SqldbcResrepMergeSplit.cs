// SqldbcUilResrepMergeSplit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{

  public ResrepMergeToSameModel MergeResrepRoot(ResrepMergeToSameModel editObj)
  {
    var errorMessage = string.Empty;
    var recordMessage = $" {editObj.ItemXnam} record ";
    var agentGuid = QURC.QebAgentGuid;
    var errorCode = ScribeResrepEditMergeToSame(agentGuid, editObj.RecordHandleToRetain, editObj.RecordHandleToDelete);
    if (errorCode < 0) { errorMessage = $"Database error = {errorCode} while deleting {recordMessage}"; }
    if (string.IsNullOrEmpty(errorMessage)) { 
      editObj.PdpStatusMessage = $"{recordMessage} merged in database";
      editObj.PdpStatusItemStored = true;
    }
    else { 
      editObj.PdpStatusMessage = errorMessage;
      editObj.PdpStatusItemStored = false;
    }
    return editObj;
  }

  public ResrepSplitToDifferentModel SplitResrepRoot(ResrepSplitToDifferentModel editObj)
  {
    var errorMessage = string.Empty;
    var recordMessage = $" {editObj.ItemXnam} record ";
    var agentGuid = QURC.QebAgentGuid;
    var errorCode = ScribeResrepEditSplitToDifferent(agentGuid, editObj.RecordHandleToSplit);
    if (errorCode < 0) { errorMessage = $"Database error = {errorCode} while deleting {recordMessage}"; }
    if (string.IsNullOrEmpty(errorMessage))
    {
      editObj.PdpStatusMessage = $"{recordMessage} merged in database";
      editObj.PdpStatusItemStored = true;
    }
    else
    {
      editObj.PdpStatusMessage = errorMessage;
      editObj.PdpStatusItemStored = false;
    }
    return editObj;
  }

}

