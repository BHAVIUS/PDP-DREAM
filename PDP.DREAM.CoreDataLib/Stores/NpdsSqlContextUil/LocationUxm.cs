// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class LocationUxm : ResrepRootModelBase
{
  public LocationUxm()
  {
    // ATTN: non-nullable fields may be considered 'required' by some edit form validators
    //  but this consideration may not apply to Telerik Kendo widgets if not declared
    //  in the Kendo Jquery Html wrapper for the widget
    itemXnam = LocationItemXnam;
  }

  private string? ssLocn = ESS;
  public string? Location
  {
    set {
      ssLocn = value;
      if (string.IsNullOrEmpty(ssLocn))
      {
        ssLocnTkgr = ESS;
        ssLocnJsam = ESS;
      }
      else
      {
        // for visible infosubset field displayed in TKG row
        ssLocnTkgr = ssLocn.TruncateForTkgr(MCGenLong);
        // for hidden infosubset field displayed in JavaScript popup alert
        ssLocnJsam = ssLocn.TruncateToJsam();
      }
    }
    get { return ssLocn; }
  }

  private string ssLocnTkgr = ESS;
  public string LocationTkgr
  {
    get { return ssLocnTkgr; }
  }

  // TODO: consider also alternative uses of DisplayText, DisplayImageUrl, etc
  private string ssLocnJsam = ESS;
  public string LocationJsam
  {
    get { return ssLocnJsam; }
  }

} // end class

// end file
