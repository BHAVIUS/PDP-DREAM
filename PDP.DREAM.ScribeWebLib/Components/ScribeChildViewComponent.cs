using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.ScribeDataLib.Models;
using PDP.DREAM.ScribeDataLib.Stores;

// A view component defines its logic in an InvokeAsync method that returns a Task<IViewComponentResult>
//    or in a synchronous Invoke method that returns an IViewComponentResult.
//    Parameters come directly from invocation of the view component, not from model binding.

namespace PDP.DREAM.ScribeWebLib.Components
{
  // for views corresponding to child tables in database
  public class ScribeChildViewComponent : ViewComponent
  {
    protected readonly ScribeDbsqlContext dbc; // DataBaseContext
    protected NexusResrepEditModel? rrr; // ResRepRecord
    public ScribeChildViewComponent(ScribeDbsqlContext dataCntxt) { dbc = dataCntxt; }

    public virtual async Task<IViewComponentResult> InvokeAsync(string componentView, string recordGuid, bool isInfosetKey = false)
    {
      if (!string.IsNullOrEmpty(recordGuid))
      {
        rrr = await dbc.GetEditableResrepStemByKeyAsync(recordGuid, isInfosetKey);
      }
      return View(componentView, rrr);
    }

  }

}
