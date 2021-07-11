// IDoorsLocationViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IDoorsLocationViewModel : INexusViewModelBase
  {
    bool IsPrincipal { get; set; }
    string Location { get; set; }
    string LocationXml { get; set; }
    string LocationXhtml { get; set; }
    string LocationRdfOwl { get; set; }
  }

}
