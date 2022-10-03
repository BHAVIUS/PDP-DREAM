// OtherTextEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class OtherTextEditModel : OtherTextViewModel
{
  public OtherTextEditModel()
  {
    itemXnam = PdpAppConst.OtherTextItemXnam;
  }

  //public string? OtherText { get; set; } = string.Empty;

  //public string? OtherTextHtml { get { return OtherText.StringEscapeHashLiteral(); } }

  //private string? ssotxt128;
  //public string? OtherText128
  //{
  //  get {
  //    if (string.IsNullOrEmpty(OtherText)) { ssotxt128 = string.Empty; }
  //    else { ssotxt128 = OtherText.ToPartialPhrase(128).StringEscapeHashLiteral(); }
  //    return ssotxt128;
  //  }
  //}

} // end class

// end file