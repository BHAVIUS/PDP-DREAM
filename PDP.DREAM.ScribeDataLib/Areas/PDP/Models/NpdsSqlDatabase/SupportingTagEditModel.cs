using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class SupportingTagEditModel : NexusEditModelBase
  {
    public SupportingTagEditModel()
    {
      itemXnam = NpdsConst.SupportingTagItemXnam;
    }

     public string? SupportingTag { get; set; }

    public string? SupportingTagHtml { get { return HtmlEscapeHashLiteral(SupportingTag); } }

  }

}
