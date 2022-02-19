// SendCodeUxm.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Models
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
