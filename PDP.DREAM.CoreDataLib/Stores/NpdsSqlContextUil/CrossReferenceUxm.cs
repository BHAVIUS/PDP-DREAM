// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class CrossReferenceUxm : ResrepRootModelBase
{
  public CrossReferenceUxm()
  {
    itemXnam = PdpAppConst.CrossReferenceItemXnam;
  }

  public string? CrossReference { get; set; } = ESS;

  private string ssCref128 = ESS;
  public string CrossReference128
  {
    get {
      if (string.IsNullOrEmpty(CrossReference)) { ssCref128 = ESS; }
      else { ssCref128 = CrossReference.TruncateForTkgr(128).StringEscapeHashLiteral(); }
      return ssCref128;
    }
  }

  private string ssCrefHtml = ESS;
  public string CrossReferenceHtml
  {
    get {
      if (string.IsNullOrEmpty(CrossReference)) { ssCrefHtml = ESS; }
      else { ssCrefHtml = CrossReference.StringEscapeHashLiteral(); }
      return ssCrefHtml;
    }
  }

} // end class

// end file