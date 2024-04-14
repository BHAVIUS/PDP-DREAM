// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private int countReqst = PdpAppConst.ListCountMinDeflt;
  public int ListCountReqst
  {
    set {
      countReqst = value;
      if ((0 < countReqst) && (countReqst < PdpAppConst.ListCountSuperMaxDeflt))
      {
        listCount = countReqst;
      }
      else
      {
        listCount = PdpAppConst.ListCountMinDeflt;
      }
    }
    get { return countReqst; }
  }

  // validated values

  private int listCount = PdpAppConst.ListCountMaxDeflt;
  public int PageListCount
  {
    set { listCount = value; }
    get { return listCount; }
  }

} // end class

// end file