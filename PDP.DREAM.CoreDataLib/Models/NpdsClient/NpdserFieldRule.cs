// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // FieldRule determines restrictions/conditions on record fields in the different formats
  // Required and Permitted are named and specified by NPDS standard
  // Extended and Prohibited are unnamed and unspecified by NPDS standard
  public enum NpdsFieldRule { None = 0, Required = 1, Permitted = 2, Extended = 3, Prohibited = 4 };

} // end class

// end file