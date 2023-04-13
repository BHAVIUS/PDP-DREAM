// PdpAppConstGeneric.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // this class contains nothing but static constants and enums only
  // if enum value not set in code, it defaults to 0

  // nullable values


  // non-nullable values

  public static JsonSerializerOptions QebKendoJsonOptions = new JsonSerializerOptions()
  {
    PropertyNamingPolicy = null,
    PropertyNameCaseInsensitive = false,
  };

} // end class

// end file