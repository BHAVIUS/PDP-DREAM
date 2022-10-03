// IDoorsLocationViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public interface IDoorsLocationViewModel : ICoreResrepViewModel
{
  bool IsPrincipal { get; set; }
  string Location { get; set; }
  string LocationXml { get; set; }
  string LocationXhtml { get; set; }
  string LocationRdfOwl { get; set; }
}

