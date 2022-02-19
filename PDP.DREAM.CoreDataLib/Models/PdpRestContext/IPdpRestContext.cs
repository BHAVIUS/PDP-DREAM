// IPdpRestContext.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  // TODO: not implemented by concrete
  public interface IPdpRestContext
  {
    // NpdsRestServiceParameters PSP { get; }
    // NpdsRestRequestOptions PRO { get; }
    // NpdsRestResponseSettings PRS { get; }

    NpdsConst.ResrepFormat ResrepFormat { get; set; }

    bool NpdsItemCanBeAccessed { get; }
    bool NpdsItemCanBeVerbosed { get; }
    bool NpdsItemDoesArchive { get; }
    bool NpdsItemDoesVerbose { get; }
    bool NpdsItemIsConcise { get; set; }
    bool NpdsItemIsPrivate { get; set; }

    string StatusCode { get; set; }
    string StatusName { get; set; }
    string StatusXhtml { get; set; }

   }

}
