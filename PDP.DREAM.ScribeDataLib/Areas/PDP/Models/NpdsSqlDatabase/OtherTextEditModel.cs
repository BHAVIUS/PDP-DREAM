using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class OtherTextEditModel : NexusEditModelBase
  {
    public OtherTextEditModel()
    {
      itemXnam = NpdsConst.OtherTextItemXnam;
    }

    public string? OtherText { get; set; }

  }

}
