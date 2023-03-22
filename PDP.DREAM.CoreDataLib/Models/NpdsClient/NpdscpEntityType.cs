// PrcEntityType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  defaults for filtering/selecting records and defaults for creating new records
public partial class NpdsClient
{
  // requested values
  private string reqEntityType = string.Empty;
  public string EntityTypeReqst
  {
    set
    {
      reqEntityType = value;
      EntityType = ValidateEntityType(reqEntityType);
    }
    get { return reqEntityType; }
  }

  // validated values
  private PdpAppConst.NpdsEntityType entityType = default;
  public PdpAppConst.NpdsEntityType EntityType
  {
    set { entityType = ValidateEntityType(value); }
    get
    {
      if (entityType == 0)
      { EntityType = NPDSSD.EntityTypeDefault; }
      return entityType;
    }
  }

  // validators

  private PdpAppConst.NpdsEntityType ValidateEntityType(string strValue)
  {
    PdpAppConst.NpdsEntityType enmValue = PdpEnum<PdpAppConst.NpdsEntityType>.ParseString(strValue, PdpAppConst.NpdsEntityType.AnyAndAll);
    return enmValue;
  }
  private PdpAppConst.NpdsEntityType ValidateEntityType(PdpAppConst.NpdsEntityType value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

} // end class

// end file