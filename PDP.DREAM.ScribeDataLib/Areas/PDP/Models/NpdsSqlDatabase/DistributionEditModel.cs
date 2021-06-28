using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class DistributionEditModel : NexusEditModelBase
  {
    public DistributionEditModel()
    {
      itemXnam = NpdsConst.DistributionItemXnam;
    }

    public string? Distribution { get; set; }

  }

}