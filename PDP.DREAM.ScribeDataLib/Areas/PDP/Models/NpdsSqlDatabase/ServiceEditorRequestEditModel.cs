﻿using System;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class ServiceEditorRequestEditModel : NexusEditModelBase
  {
    public ServiceEditorRequestEditModel()
    {
      itemXnam = "ServiceEditorRequest";
    }

    public string? ResrepRecordHandle { get; set; } = string.Empty;
    public string? ResrepEntityName { get; set; } = string.Empty;

    public Guid? AccessRequestedForAgentGuid { get; set; } = null;
    public string? AccessRequestedForAgentName { get; set; } = string.Empty;
    public Guid? AccessApprovedByAgentGuid { get; set; } = null;
    public string? AccessApprovedByAgentName { get; set; } = string.Empty;

    public bool RequestIsApproved { get; set; }
    public bool RequestIsDenied { get; set; }
    public bool EditorHasServiceAccess { get; set; }

  }

}