using PDP.DREAM.NpdsDataLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IDoorsLocationViewModel : INexusViewModelBase
  {
    bool IsPrincipal { get; set; }
    string Location { get; set; }
    string LocationXml { get; set; }
    string LocationXhtml { get; set; }
    string LocationRdfOwl { get; set; }
  }

}
