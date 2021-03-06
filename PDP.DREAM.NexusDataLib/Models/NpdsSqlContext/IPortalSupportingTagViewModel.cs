// IPortalSupportingTagViewModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models
{
  public interface IPortalSupportingTagViewModel : INexusViewModelBase
  {
    string SupportingTag { get; set; }
    bool IsPrincipal { get; set; }
  }
}