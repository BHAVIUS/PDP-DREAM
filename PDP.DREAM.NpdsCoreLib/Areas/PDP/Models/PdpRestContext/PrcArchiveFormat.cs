namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {

    public bool ArchiveFormatReqst { get; set; }

    public bool ArchiveFormat
    {
      get { return (ArchiveFormatReqst && ClientIsAuthorized); }
    }

  }

}
