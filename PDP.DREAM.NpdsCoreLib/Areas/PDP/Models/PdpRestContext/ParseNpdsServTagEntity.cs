using System;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    public void ParseNpdsServTagEntity(string serviceType, string serviceTag, string entityType, string action = "")
    {
      if (string.IsNullOrWhiteSpace(serviceType) || string.IsNullOrWhiteSpace(serviceTag)) { throw new Exception("serviceType and serviceTag cannot be empty in ParseNpdsServTagEntity"); }

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
