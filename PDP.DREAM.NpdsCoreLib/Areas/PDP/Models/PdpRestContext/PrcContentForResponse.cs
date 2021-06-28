using System.Text;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // options for NPDS response
  public partial class PdpRestContext
  {
    public string? ResponseHeader { get; set; }

    public string? ResponseStatus { get; set; }

    public string? ResponseNote
    {
      set
      {
        var sb = new StringBuilder(ResponseNote);
        sb.AppendLine(value);
        respNote = sb.ToString();
      }
      get
      {
        if (string.IsNullOrEmpty(respNote))
        {
          if (!string.IsNullOrEmpty(ServiceNote)) { respNote = ServiceNote; };
        }
        return respNote;
      }
    }
    private string? respNote;

    public NpdsResrepList? ResponseAnswer { set; get; }
    public NpdsResrepList? ResponseRelated { set; get; }
    public NpdsResrepList? ResponseReferred { set; get; }

    public NpdsResrepList? CoreRecords { set; get; }
    public NpdsResrepList? PortalRecords { set; get; }
    public NpdsResrepList? DoorsRecords { set; get; }
    public NpdsResrepList? NexusRecords { set; get; }

  }

}