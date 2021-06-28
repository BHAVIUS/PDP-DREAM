using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class NexusSnapshotEditModel : NexusEditModelBase
  {
    public NexusSnapshotEditModel()
    {
      itemXnam = NpdsConst.NexusResrepItemXnam + "Archived";
    }

    public string NexusSnapshot { get; set; } = string.Empty;

  }

}