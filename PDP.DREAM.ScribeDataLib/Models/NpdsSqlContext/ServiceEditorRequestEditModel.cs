// ServiceEditorRequestEditModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.ScribeDataLib.Models
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