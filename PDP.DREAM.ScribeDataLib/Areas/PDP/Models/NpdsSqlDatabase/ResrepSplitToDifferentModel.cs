using System.ComponentModel;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class ResrepSplitToDifferentModel : NexusEditModelBase
  {
    public ResrepSplitToDifferentModel() { }

    [DisplayName("Handle of Record To Split")]
    public string? RecordHandleToSplit { get; set; } = string.Empty;

  }

}