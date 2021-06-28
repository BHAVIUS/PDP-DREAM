using System;

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IPortalEntityLabelViewModel : INexusViewModelBase
  {
    string EntityLabel { get; set; }
    string LabelUri { get; set; }
    string LabelUrl { get; set; }
    string TagToken { get; set; }
    bool IsCanonical { get; set; }
    bool IsResolvable { get; set; }
  }
}