// PrcInfosetStatus.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  (A) defaults for filtering/selecting records and (B) defaults for creating new records
//
// default below currently intended for (A) filtering/selecting records
public partial class NpdsClient
{
  // requested values

  private string reqInfosetStatus = string.Empty;
  public string InfosetStatusReqst
  {
    set
    {
      reqInfosetStatus = value;
      infosetStatus = ValidateInfosetStatus(reqInfosetStatus);
    }
    get { return reqInfosetStatus; }
  }

  // validated values

  // initialize to the default which should be "Any"
  private PdpAppConst.NpdsInfosetStatus infosetStatus = PdpAppConst.NpdsInfosetStatus.AnyAndAll;
  public PdpAppConst.NpdsInfosetStatus InfosetStatus
  {
    set { infosetStatus = ValidateInfosetStatus(value); }
    get { return infosetStatus; }
  }

  // validators

  private PdpAppConst.NpdsInfosetStatus ValidateInfosetStatus(string strValue)
  {
    // if not a valid value, then reset to the default which should be "Any"
    PdpAppConst.NpdsInfosetStatus enmValue = PdpEnum<PdpAppConst.NpdsInfosetStatus>.ParseString(strValue, NpdsInfosetStatus.AnyAndAll);
    return enmValue;
  }
  private PdpAppConst.NpdsInfosetStatus ValidateInfosetStatus(PdpAppConst.NpdsInfosetStatus value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

