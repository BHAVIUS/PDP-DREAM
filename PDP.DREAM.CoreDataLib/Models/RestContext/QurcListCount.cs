// PrcListCount.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // TODO: finish implementation with property ListCount and appsetting for ListCountDefault (in addition to internal NpdsConstants.ListCountDefault and NpdsConstants.ListCountMaxDefault etc etc)
  // TODO: not finished as of 6/5/17

  // default values

  public int ListCountDeflt
  {
    set { countDeflt = value; }
    get { return countDeflt; }
  }
  private int countDeflt = PdpAppConst.ListCountDeflt;

  // requested values

  public int ListCountReqst
  {
    set { listContReq = value; }
    get { return listContReq; }
  }
  private int listContReq = PdpAppConst.ListCountDeflt;

  // validated values

  public int ListCount
  {
    set
    {
      if ((0 < value) && (value < PdpAppConst.ListCountSuperMaxDeflt))
      {
        lstCountMax = value;
      }
      else
      {
        lstCountMax = PdpAppConst.ListCountMaxDeflt;
      }
    }
    get
    {
      if (ListCountReqst > 0) { ListCount = ListCountReqst; }
      return lstCountMax;
    }
  }
  private int lstCountMax = PdpAppConst.ListCountMaxDeflt;

}

