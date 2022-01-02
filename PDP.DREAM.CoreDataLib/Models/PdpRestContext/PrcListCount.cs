// PrcListCount.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  public partial class PdpRestContext
  {
    // TODO: finish implementation with property ListCount and appsetting for ListCountDefault (in addition to internal NpdsConstants.ListCountDefault and NpdsConstants.ListCountMaxDefault etc etc)
    // TODO: not finished as of 6/5/17

    // default values

    public int ListCountDeflt
    {
      set { countDeflt = value; }
      get { return countDeflt; }
    }
    private int countDeflt = NpdsConst.ListCountDeflt;

    // requested values

    public int ListCountReqst
    {
      set { listContReq = value; }
      get { return listContReq; }
    }
    private int listContReq = NpdsConst.ListCountDeflt;

    // validated values

    public int ListCount
    {
      set
      {
        if ((0 < value) && (value < NpdsConst.ListCountSuperMaxDeflt))
        {
          lstCountMax = value;
        }
        else
        {
          lstCountMax = NpdsConst.ListCountMaxDeflt;
        }
      }
      get
      {
        if (ListCountReqst > 0) { ListCount = ListCountReqst; }
        return lstCountMax;
      }
    }
    private int lstCountMax = NpdsConst.ListCountMaxDeflt;

  }

}
