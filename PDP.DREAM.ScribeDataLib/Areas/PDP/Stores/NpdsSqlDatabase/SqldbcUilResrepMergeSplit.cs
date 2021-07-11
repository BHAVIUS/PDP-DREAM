// SqldbcUilResrepMergeSplit.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{

  public partial class ScribeDbsqlContext
  {
 
    public ResrepMergeToSameModel MergeResrepRoot(ResrepMergeToSameModel editObj)
    {
      var errorMessage = string.Empty;
      var recordMessage = $" {editObj.ItemXnam} record ";
      var agentGuid = PRC.AgentGuid;
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
      var agentGuid = PRC.AgentGuid;
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

}