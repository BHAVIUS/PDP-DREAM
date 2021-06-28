using System;

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IPortalSupportingLabelViewModel : INexusViewModelBase
  {
    string SupportingLabel { get; set; }
    Boolean LabelIsPrincipal { get; set; }
    Boolean LabelIsRestricted { get; set; }
  }
}