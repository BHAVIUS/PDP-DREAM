// IPortalSupportingLabelEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Models
{
  public interface INexusSupportingLabelEditModel : INexusEditModelBase
  {
    string SupportingLabel { get; set; }
  }

}
