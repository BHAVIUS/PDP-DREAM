// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  public static readonly Guid EGS = Guid.Empty; // Empty Guid Symbol
  public const string ESS = ""; // Empty String Symbol
  public const int NIS = -1; // Null Integer Symbol (context must be math int aware)
  public const int ZIS = 0; // Zero Integer Symbol (context must be math int aware)

  public static JsonSerializerOptions QebKendoJsonOptions = new JsonSerializerOptions()
  {
    PropertyNamingPolicy = null,
    PropertyNameCaseInsensitive = false,
  };

} // end class

// end file