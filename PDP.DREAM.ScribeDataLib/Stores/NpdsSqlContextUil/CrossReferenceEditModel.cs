// CrossReferenceEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Models;

public class CrossReferenceEditModel : CrossReferenceViewModel
{
  public CrossReferenceEditModel()
  {
    itemXnam = PdpAppConst.CrossReferenceItemXnam;
  }

  //public string? CrossReference { get; set; } = string.Empty;

  //public string? CrossReferenceHtml { get { return CrossReference.StringEscapeHashLiteral(); } }

  //private string? sscref128;
  //public string? CrossReference128
  //{
  //  get {
  //    if (string.IsNullOrEmpty(CrossReference)) { sscref128 = string.Empty; }
  //    else { sscref128 = CrossReference.ToPartialPhrase(128).StringEscapeHashLiteral(); }
  //    return sscref128;
  //  }
  //}

} // end class

// end file