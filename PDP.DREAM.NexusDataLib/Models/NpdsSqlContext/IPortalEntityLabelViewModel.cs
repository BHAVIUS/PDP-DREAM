// IPortalEntityLabelViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models
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