// IPortalSupportingLabelViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IPortalSupportingLabelViewModel : INexusViewModelBase
  {
    string SupportingLabel { get; set; }
    Boolean LabelIsPrincipal { get; set; }
    Boolean LabelIsRestricted { get; set; }
  }
}