// ProvenanceEditModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models
{
  public class ProvenanceEditModel : NexusEditModelBase
  {
    public ProvenanceEditModel()
    {
      itemXnam = NpdsConst.ProvenanceItemXnam;
    }

    public string? Provenance { get; set; }

  }

}