// NexusEditModelBase.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public abstract class NexusEditModelBase : NexusViewModelBase
{
  public bool PdpStatusItemStored { get; set; } = false;

  // TODO: LINQ problems with byte array on .Include()
  // for property HasVersion in the Core Audit table field collection
  // public byte[]? HasVersion { get; set; } = null;
  //

}
