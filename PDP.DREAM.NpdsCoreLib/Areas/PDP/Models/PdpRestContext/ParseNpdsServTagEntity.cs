// ParseNpdsServTagEntity.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    public void ParseNpdsServTagEntity(string serviceType, string serviceTag, string entityType, string action = "")
    {
      if (string.IsNullOrWhiteSpace(serviceType)) {
        serviceType = SrvcDefs.NpdsDefaultServiceType.ToString(); 
      }
      if (string.IsNullOrWhiteSpace(serviceTag)) { 
        switch (serviceType.ToLower())
        {
          case "nexus":
            serviceTag = SrvcDefs.NpdsDefaultDiristryTag;
            break;
          case "portal":
            serviceTag = SrvcDefs.NpdsDefaultRegistryTag;
            break;
          case "doors":
            serviceTag = SrvcDefs.NpdsDefaultDirectoryTag;
            break;
          case "scribe":
            serviceTag = SrvcDefs.NpdsDefaultRegistrarTag;
            break;
          default:
            serviceTag = SrvcDefs.NpdsRootServiceTag;
            break;
        }
      }

      PRC.ServiceTypeReqst = serviceType;
      PRC.ServiceTagReqst = serviceTag;
      PRC.ValidateSearchFilter();
      PRC.EntityTypeReqst = entityType;
      PRC.EntityTag = string.Empty;
      PRC.InfosetStatus = NpdsConst.InfosetStatus.AnyAndAll;

      string viewTitle = "", viewName = "";
      var role = PRC.RecordAccess.ToString();

      switch (action)
      {
        case "View":
          viewName = "NexusView";
          break;
        case "Edit":
          viewName = "ScribeEdit";
          break;
        case "":
          // do nothing, AJAX/REST call from previously actioned view
          break;
        default:
          throw new Exception("invalid action in ParseNpdsServTagEntity");
      }

      if (!string.IsNullOrEmpty(viewName))
      {
        PRC.ViewName = viewName;
        if (string.IsNullOrEmpty(role)) { viewTitle = action; }
        else { viewTitle = $"{action} {role}'s"; }
        PRC.PageTitle = $"{viewTitle} Resource Metadata Records on {PRC.ServiceTitle} Service";
      }
    }

  }

}
