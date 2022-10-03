// PrcEntityTag.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Text.RegularExpressions;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // default values

  // requested values

  public string EntityTagReqst
  {
    set
    {
      reqEntityTag = value;
      EntityTag = reqEntityTag;
    }
    get { return reqEntityTag; }
  }
  private string reqEntityTag = string.Empty;

  // validated values

  public string EntityTag
  {
    set
    {
      if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, PdpAppConst.RegexPrincipalTag))
      {
        entityTag = value;
      }
      else
      {
        entityTag = string.Empty;
      }

    }
    get { return entityTag; }
  }
  private string entityTag = string.Empty;

}

