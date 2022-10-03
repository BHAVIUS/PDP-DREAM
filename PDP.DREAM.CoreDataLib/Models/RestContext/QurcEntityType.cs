// PrcEntityType.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  defaults for filtering/selecting records and defaults for creating new records
public partial class QebUserRestContext
{
  // default values

  public PdpAppConst.NpdsEntityType EntityTypeDeflt // for filtering/selecting records
  { get { return defEntityType; } }
  private PdpAppConst.NpdsEntityType defEntityType = PdpAppStatus.NPDSSD.NpdsDefaultEntityType;
  public PdpAppConst.NpdsEntityType EntityTypeNewDeflt // for creating new records
  { get { return defNewEntityType; } }
  private PdpAppConst.NpdsEntityType defNewEntityType = PdpAppStatus.NPDSSD.NpdsDefaultNewEntityType;

  // requested values
  public string EntityTypeReqst
  {
    set
    {
      reqEntityType = value;
      EntityType = ValidateEntityType(reqEntityType);
    }
    get { return reqEntityType; }
  }
  private string reqEntityType = string.Empty;

  // validated values
  public PdpAppConst.NpdsEntityType EntityType
  {
    set { entityType = ValidateEntityType(value); }
    get
    {
      if (entityType == 0)
      { EntityType = EntityTypeDeflt; }
      return entityType;
    }
  }
  private PdpAppConst.NpdsEntityType entityType = default;

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