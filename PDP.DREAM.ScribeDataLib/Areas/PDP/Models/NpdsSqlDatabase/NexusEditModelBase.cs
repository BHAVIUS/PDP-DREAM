// NexusEditModelBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public abstract class NexusEditModelBase : NexusViewModelBase
  {
    public bool PdpStatusItemStored { get; set; } = false;

    // TODO: LINQ problems with byte array on .Include()
    // for property HasVersion in the Core Audit table field collection
    // public byte[]? HasVersion { get; set; } = null;
    //

  }

}
