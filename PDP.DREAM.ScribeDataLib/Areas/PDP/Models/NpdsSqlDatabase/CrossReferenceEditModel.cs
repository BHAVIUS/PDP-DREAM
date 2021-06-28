
using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class CrossReferenceEditModel : NexusEditModelBase
  {
    public CrossReferenceEditModel()
    {
      itemXnam = NpdsConst.CrossReferenceItemXnam;
    }

    public string? CrossReference { get; set; }

  }

}