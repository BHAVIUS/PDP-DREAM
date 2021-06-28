using System;

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IPortalEntityLabelEditModel : INexusEditModelBase
  {
    string EntityLabel { get; set; }
    string LabelUri { get; set; }
    string LabelUrl { get; set; }
    string TagToken { get; set; }

    bool LabelIsCanonical { get; set; }
    bool LabelIsResolvable { get; set; }
    bool LabelIsTagIndependent { get; set; }
  }
}
