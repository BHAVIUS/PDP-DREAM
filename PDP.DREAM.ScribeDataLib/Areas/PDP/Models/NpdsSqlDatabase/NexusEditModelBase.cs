using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

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
