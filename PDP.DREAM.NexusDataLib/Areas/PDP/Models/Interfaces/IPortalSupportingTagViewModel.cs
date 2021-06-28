namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface IPortalSupportingTagViewModel : INexusViewModelBase
  {
    string SupportingTag { get; set; }
    bool IsPrincipal { get; set; }
  }
}