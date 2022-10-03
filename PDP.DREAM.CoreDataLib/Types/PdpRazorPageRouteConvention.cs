// PdpMvcPageRouteConvention.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc.ApplicationModels;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpRazorPageRouteConvention : IPageRouteModelConvention
{
  public PdpRazorPageRouteConvention(string ranpPage = "")
  {
    if (!string.IsNullOrWhiteSpace(ranpPage)) { pdpRanpPage = ranpPage; }
  }
  string pdpRanpPage = "PdpDreamPage";
  public void Apply(PageRouteModel model)
  {
    var selectorCount = model.Selectors.Count;
    for (var i = 0; i < selectorCount; i++)
    {
      var selector = model.Selectors[i];
      selector.AttributeRouteModel.Order = DepRaoRazorPage;
      var revisedTemplate = selector.AttributeRouteModel.Template.Replace("/", "_");
      var firstBrace = revisedTemplate.IndexOf("_{");
      if (firstBrace >0 ) { revisedTemplate = revisedTemplate.Substring(0, firstBrace); }
      selector.AttributeRouteModel.Name = pdpRanpPage + ":" + revisedTemplate;
    }
  }

}  // end class

// end file