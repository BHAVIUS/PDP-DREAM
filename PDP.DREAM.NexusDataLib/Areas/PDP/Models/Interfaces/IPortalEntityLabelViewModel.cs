// IPortalEntityLabelViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IPortalEntityLabelViewModel : INexusViewModelBase
  {
    string EntityLabel { get; set; }
    string LabelUri { get; set; }
    string LabelUrl { get; set; }
    string TagToken { get; set; }
    bool IsCanonical { get; set; }
    bool IsResolvable { get; set; }
  }
}