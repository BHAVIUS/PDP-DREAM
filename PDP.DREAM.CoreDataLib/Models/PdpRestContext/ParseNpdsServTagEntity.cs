// ParseNpdsServTagEntity.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpRestContext
{
  public void ParseNpdsServTagEntity(string serviceType, string serviceTag, string entityType, string action = "")
  {
    // null empty missing values reset to default values
    if (string.IsNullOrWhiteSpace(serviceType))
    {
      serviceType = NPDSSD.NpdsDefaultServiceType.ToString();
    }
    if (string.IsNullOrWhiteSpace(serviceTag))
    {
      switch (serviceType.ToLower())
      {
        case "nexus":
          serviceTag = NPDSSD.NpdsDefaultDiristryTag;
          break;
        case "portal":
          serviceTag = NPDSSD.NpdsDefaultRegistryTag;
          break;
        case "doors":
          serviceTag = NPDSSD.NpdsDefaultDirectoryTag;
          break;
        case "scribe":
          serviceTag = NPDSSD.NpdsDefaultRegistrarTag;
          break;
        default:
          serviceTag = NPDSSD.NpdsRootServiceTag;
          break;
      }
    }

    // TODO: recode logic of settings to eliminate any redundancies and/or cycles
    // current code requires ordered sequence when setting values
    // service tag request should precede the service type request
    PRC.ServiceTagReqst = serviceTag;
    // service type request should reset search filter request
    PRC.ServiceTypeReqst = serviceType;
    // service tag and service type should be set before calling ValidateSearchFilter
    // TODO: code an overload of ValidateSearchFilter that accepts service type/tag as input args
    PRC.ValidateSearchFilter();
    PRC.EntityTypeReqst = entityType;
    PRC.EntityTag = string.Empty;
    PRC.InfosetStatus = NpdsConst.InfosetStatus.AnyAndAll;

    string viewTitle = "";
    string viewName = "";
    var role = PRC.RecordAccess.ToString();

    switch (action)
    {
      case "View":
        viewName = "NexusRead";
        break;
      case "Edit":
        viewName = "NexusWrite";
        break;
      case "":
        // do nothing, AJAX/REST call from previously actioned view
        break;
      default:
        throw new Exception("invalid action in ParseNpdsServTagEntity");
    }

    if (!string.IsNullOrEmpty(action))
    {
      PRC.ViewName = viewName;
      if (string.IsNullOrEmpty(role)) { viewTitle = action; }
      else { viewTitle = $"{action} {role}'s"; }
      PRC.PageTitle = $"{viewTitle} Resource Metadata Records from {PRC.ServiceTitle}";
    }
  }

} // class
