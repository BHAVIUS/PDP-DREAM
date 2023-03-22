// QurcParseSelectFilterForView.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserRestContext
{
  public string ParseNpdsSelectFilterForView(string serviceType, string serviceTag,
    string entityType, string action = "", string title = "")
  {
    ParseNpdsService(serviceType, serviceTag,
"", "", entityType, "", "");

    string viewName = "";
    // ATTN: note importance of RecordAccess
    var role = RecordAccess.ToString();

    switch (action.ToLower())
    {
      case "view":
      case "read":
        viewName = "NexusRead";
        break;
      case "edit":
      case "write":
        viewName = "NexusWrite";
        break;
      case "":
        // do nothing, AJAX/REST call from previously actioned view
        break;
      default:
        throw new Exception("invalid action in ParseNpdsSelectFilterForView");
    }

    if (!string.IsNullOrEmpty(action))
    {
      // TODO: must be recoded with return string for the viewname
      // RazorViewName = viewName;

      string actionTitle = "";
      if (string.IsNullOrEmpty(role)) { actionTitle = action; }
      else { actionTitle = $"{action} {role}'s"; }
      string recordTitle = "";
      if (string.IsNullOrEmpty(title)) { recordTitle = "Resource Metadata Records from"; }
      else { recordTitle = title; }

      // TODO: must be recoded with make title proc independenty of select filter proc
      //   can be refactored analogous to what was done for Razor pages
      // RazorViewTitle = $"{actionTitle} {recordTitle} {ServiceTitle}";
    }

    return viewName;
  }

} // end class

// end file