using System.ComponentModel;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class ResrepMergeToSameModel : NexusEditModelBase
  {
    public ResrepMergeToSameModel() { }

    [DisplayName("Handle of Record To Retain")]
    public string? RecordHandleToRetain { get; set; } = string.Empty;

    [DisplayName("Handle of Record To Delete")]
    public string? RecordHandleToDelete { get; set; } = string.Empty;

  }

}