// IPortalSupportingLabelViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.NexusDataLib.Models;

public interface IPortalSupportingLabelViewModel : ICoreResrepViewModel
{
  string SupportingLabel { get; set; }
  Boolean LabelIsPrincipal { get; set; }
  Boolean LabelIsRestricted { get; set; }
}
