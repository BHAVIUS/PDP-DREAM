using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class SendCodeUxm : RestTaskUxm
  {
    public string SelectedProvider { get; set; } = string.Empty;
    public ICollection<SelectListItem>? Providers { get; set; }
    public string ReturnUrl { get; set; } = string.Empty;
    public bool RememberMe { get; set; }

    public override void UpdateRestContext(PdpRestContext prc)
    {
      prc.ReturnUrl = this.ReturnUrl;
      prc.RememberMe = this.RememberMe;
    }

  }

}
