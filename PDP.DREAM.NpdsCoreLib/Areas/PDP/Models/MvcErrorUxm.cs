namespace PDP.DREAM.NpdsCoreLib.Models
{
  public class MvcErrorUxm
  {
    public string RequestId { get; set; } = string.Empty;
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
  }

}