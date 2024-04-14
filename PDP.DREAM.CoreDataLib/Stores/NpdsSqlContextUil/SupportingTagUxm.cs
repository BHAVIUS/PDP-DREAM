// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class SupportingTagUxm : ResrepRootModelBase
{
  public SupportingTagUxm()
  {
    itemXnam = PdpAppConst.SupportingTagItemXnam;
  }

  public string? SupportingTag { get; set; } = ESS;

  private string ssStagHtml = ESS;
  public string SupportingTagHtml
  {
    get {
      if (string.IsNullOrEmpty(SupportingTag)) { ssStagHtml = ESS; }
      else { ssStagHtml = SupportingTag.StringEscapeHashLiteral(); }
      return ssStagHtml;
    }
  }

  private string ssStag128 = ESS;
  public string SupportingTag128
  {
    get {
      if (string.IsNullOrEmpty(SupportingTag)) { ssStag128 = ESS; }
      else { ssStag128 = SupportingTag.TruncateForTkgr(128).StringEscapeHashLiteral(); }
      return ssStag128;
    }
  }

} // end class

// end file