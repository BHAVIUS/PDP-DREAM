// IPortalSupportingTagViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IPortalSupportingTagViewModel : INexusViewModelBase
  {
    string SupportingTag { get; set; }
    bool IsPrincipal { get; set; }
  }
}