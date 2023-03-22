// QebUserRestContext
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string reqClientRole = string.Empty;
  public string ClientRoleReqst
  {
    set
    {
      reqClientRole = value;
      ClientRole = ValidateClientRole(reqClientRole);
    }
    get { return reqClientRole; }
  }

  // validated values

  private NamesForClientRoles clientRole = default;
  public NamesForClientRoles ClientRole
  {
    set { clientRole = ValidateClientRole(value); }
    get
    {
      if (clientRole == 0)
      { ClientRole = NamesForClientRoles.NpdsAnon; }
      return clientRole;
    }
  }

  // validators

  private NamesForClientRoles ValidateClientRole(string strValue, NamesForClientRoles defValue = NamesForClientRoles.NpdsAnon)
  {
    NamesForClientRoles enmValue = PdpEnum<NamesForClientRoles>.ParseString(strValue, defValue);
    return enmValue;
  }
  private NamesForClientRoles ValidateClientRole(NamesForClientRoles value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

} // end class

// end file