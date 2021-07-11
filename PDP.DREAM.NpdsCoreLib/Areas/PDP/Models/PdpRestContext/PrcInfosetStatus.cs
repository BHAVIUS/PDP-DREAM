// PrcInfosetStatus.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // TODO: revise for better conventions on difference between
  //  (A) defaults for filtering/selecting records and (B) defaults for creating new records
  //
  // default below currently intended for (A) filtering/selecting records
  public partial class PdpRestContext
  {
    // default values

    public NpdsConst.InfosetStatus InfosetStatusDeflt
    { get { return defInfosetStatus; } }
    private NpdsConst.InfosetStatus defInfosetStatus = NpdsConst.InfosetStatus.AnyAndAll;

    // requested values

    public string InfosetStatusReqst
    {
      set
      {
        reqInfosetStatus = value;
        infosetStatus = ValidateInfosetStatus(reqInfosetStatus);
      }
      get { return reqInfosetStatus; }
    }
    private string reqInfosetStatus = string.Empty;

    // validated values

    public NpdsConst.InfosetStatus InfosetStatus
    {
      set { infosetStatus = ValidateInfosetStatus(value); }
      get { return infosetStatus; }
    }
    // initialize to the default which should be "Any"
    private NpdsConst.InfosetStatus infosetStatus = NpdsConst.InfosetStatus.AnyAndAll;

    // validators

    private NpdsConst.InfosetStatus ValidateInfosetStatus(string strValue)
    {
      // if not a valid value, then reset to the default which should be "Any"
      NpdsConst.InfosetStatus enmValue = PdpEnum<NpdsConst.InfosetStatus>.Parse(strValue, InfosetStatusDeflt);
      return enmValue;
    }
    private NpdsConst.InfosetStatus ValidateInfosetStatus(NpdsConst.InfosetStatus value)
    {
      // TODO: finish coding this virtual implementation with AI rules
      return value;
    }

  }

}
