
namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // default values

    // requested values

    public bool CheckFormatReqst
    {
      set { reqChckForm = value; }
      get { return reqChckForm; }
    }
    private bool reqChckForm = false;

    // validated values

    public bool CheckFormat
    {
      get { return CheckFormatReqst; }
    }

  }

}
