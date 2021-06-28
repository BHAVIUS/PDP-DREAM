using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class SupportingLabelEditModel : NexusEditModelBase
  {
    public SupportingLabelEditModel()
    {
      itemXnam = NpdsConst.SupportingLabelItemXnam;
    }

    public string? SupportingLabel { get; set; }

  }

}
