// PdpAppConstGeneric.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // this class contains nothing but static constants and enums only
  // if enum value not set in code, it defaults to 0

  // nullable values

  // ATTN: this approach never worked !!!
  // Nullable Empty String (NES)
  // public readonly static string? QebNES = string.Empty; 
  // Nullable Empty String Array (NESA)
  // public readonly static string[]? QebNESA = Array.Empty<string>(); 
  // Nullable Empty String Array List (NESAL)
  // public static IList<string[]>? QebNESAL = (IList<string[]>?)new List<string[]>(); 
  // public readonly static IList<string[]>? QebNESAL = new List<string[]>(); 
  // Nullable Empty Int String Array Dictionary (NEISAD)
  // public static IDictionary<int, string[]>? QebNEISAD = (IDictionary<int, string[]>?)new Dictionary<int, string[]>();
  // public readonly static IDictionary<int, string[]>? QebNEISAD = new Dictionary<int, string[]>();

  // non-nullable values

  public static JsonSerializerOptions QebKendoJsonOptions = new JsonSerializerOptions()
  {
    PropertyNamingPolicy = null,
    PropertyNameCaseInsensitive = false,
  };

  // TODO: avoid use of this check as inconsistent across Nexus and Scribe; should be deprecated
  public readonly static string[] QebRazorAnonList = new string[] { "Index", "Help", "Examples" };

} // end class

// end file