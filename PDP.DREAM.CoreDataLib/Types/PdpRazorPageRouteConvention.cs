// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpRazorPageRouteConvention : IPageRouteModelConvention
{
  private string depRan = DepRanRazorPage; // route app name
  private int depRao = DepRaoRazorPage; // route app order
  public PdpRazorPageRouteConvention(string ranPage = "", int raoPage = 0)
  {
    if (string.IsNullOrWhiteSpace(ranPage))
    { depRan = DepRanRazorPage; }
    else
    { depRan = ranPage; }
    if (raoPage == 0)
    { depRao = DepRaoRazorPage; }
    else
    { depRao = raoPage; }
  }
  public void Apply(PageRouteModel model)
  {
    var selectorCount = model.Selectors.Count;
    for (var i = 0; i < selectorCount; i++)
    {
      var selector = model.Selectors[i];
      selector.AttributeRouteModel.Order = depRao;
      var revisedTemplate = selector.AttributeRouteModel.Template.Replace("/", "_");
      var firstBrace = revisedTemplate.IndexOf("_{");
      if (firstBrace > 0) { revisedTemplate = revisedTemplate.Substring(0, firstBrace); }
      selector.AttributeRouteModel.Name = $"{depRan}:{revisedTemplate}";
    }
  }

}  // end class

// end file