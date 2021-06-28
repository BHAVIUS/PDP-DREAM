using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class ProvenanceEditModel : NexusEditModelBase
  {
    public ProvenanceEditModel()
    {
      itemXnam = NpdsConst.ProvenanceItemXnam;
    }

    public string? Provenance { get; set; }

  }

}