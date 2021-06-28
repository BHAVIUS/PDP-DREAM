
namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {

    public bool EchoFormatReqst
    {
      set { reqEchoFormat = value; }
      get { return reqEchoFormat; }
    }
    private bool reqEchoFormat = false;

    public bool EchoFormat
    {
      get { return EchoFormatReqst; }
    }

  }
}
