// PrcInfosetStatus.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  (A) defaults for filtering/selecting records and (B) defaults for creating new records
//
// default below currently intended for (A) filtering/selecting records
public partial class QebUserRestContext
{
  // default values

  public PdpAppConst.NpdsInfosetStatus InfosetStatusDeflt
  { get { return defInfosetStatus; } }
  private PdpAppConst.NpdsInfosetStatus defInfosetStatus = PdpAppConst.NpdsInfosetStatus.AnyAndAll;

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

  public PdpAppConst.NpdsInfosetStatus InfosetStatus
  {
    set { infosetStatus = ValidateInfosetStatus(value); }
    get { return infosetStatus; }
  }
  // initialize to the default which should be "Any"
  private PdpAppConst.NpdsInfosetStatus infosetStatus = PdpAppConst.NpdsInfosetStatus.AnyAndAll;

  // validators

  private PdpAppConst.NpdsInfosetStatus ValidateInfosetStatus(string strValue)
  {
    // if not a valid value, then reset to the default which should be "Any"
    PdpAppConst.NpdsInfosetStatus enmValue = PdpEnum<PdpAppConst.NpdsInfosetStatus>.ParseString(strValue, InfosetStatusDeflt);
    return enmValue;
  }
  private PdpAppConst.NpdsInfosetStatus ValidateInfosetStatus(PdpAppConst.NpdsInfosetStatus value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

