// PdpMvcPageRouteConvention.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc.ApplicationModels;

using PDP.DREAM.CoreDataLib.Controllers;

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpMvcPageRouteConvention : IPageRouteModelConvention
{
  public void Apply(PageRouteModel model)
  {
    var selectorCount = model.Selectors.Count;
    for (var i = 0; i < selectorCount; i++)
    {
      var selector = model.Selectors[i];
      selector.AttributeRouteModel.Name = CoreDLC.ranpPage + ":" +
        selector.AttributeRouteModel.Template.Replace("/", "_");
    }
  }

}  // end class

// end file