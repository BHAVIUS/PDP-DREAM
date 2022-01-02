using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.ScribeWebLib.Components
{
  // for views corresponding to parent tables in database
  public class ScribeParentViewComponent : ViewComponent
  {
    public ScribeParentViewComponent() { }

    public virtual async Task<IViewComponentResult> InvokeAsync(string componentView)
    {
      return View(componentView);
    }

  }

}
