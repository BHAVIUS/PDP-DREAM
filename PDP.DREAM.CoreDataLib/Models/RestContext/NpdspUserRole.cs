// QebUserRestContext
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // default values

  public NamesForIdentityRoles NpdsUserRoleDeflt 
  { get { return defNpdsUserRole; } }
  private NamesForIdentityRoles defNpdsUserRole = NamesForIdentityRoles.NpdsAnon;

  // requested values
  public string NpdsUserRoleReqst
  {
    set
    {
      reqNpdsUserRole = value;
      NpdsUserRole = ValidateNpdsUserRole(reqNpdsUserRole);
    }
    get { return reqNpdsUserRole; }
  }
  private string reqNpdsUserRole = string.Empty;

  // validated values
  public NamesForIdentityRoles NpdsUserRole
  {
    set { npdsUserRole = ValidateNpdsUserRole(value); }
    get
    {
      if (npdsUserRole == 0)
      { NpdsUserRole = NpdsUserRoleDeflt; }
      return npdsUserRole;
    }
  }
  private NamesForIdentityRoles npdsUserRole = default;

  // validators

  private NamesForIdentityRoles ValidateNpdsUserRole(string strValue, NamesForIdentityRoles defValue = NamesForIdentityRoles.NpdsAnon)
  {
    NamesForIdentityRoles enmValue = PdpEnum<NamesForIdentityRoles>.ParseString(strValue, defValue);
    return enmValue;
  }
  private NamesForIdentityRoles ValidateNpdsUserRole(NamesForIdentityRoles value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

} // end class

// end file