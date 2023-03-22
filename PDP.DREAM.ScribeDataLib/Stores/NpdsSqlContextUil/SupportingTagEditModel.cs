// SupportingTagEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Models;

public class SupportingTagEditModel : SupportingTagViewModel
{
  public SupportingTagEditModel()
  {
    itemXnam = PdpAppConst.SupportingTagItemXnam;
  }

  //public string? SupportingTag { get; set; } = string.Empty;

  //public string? SupportingTagHtml { get { return SupportingTag.StringEscapeHashLiteral(); } }

} // end class

// end file