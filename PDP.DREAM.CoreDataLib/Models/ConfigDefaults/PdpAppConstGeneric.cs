// PdpAppConstGeneric.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // this class contains nothing but static constants and enums only
  // if enum value not set in code, it defaults to 0

  // nullable values

  // Nullable Empty String (NES)
  public static string? QebNES = string.Empty; 
  // Nullable Empty String Array (NESA)
  public static string[]? QebNESA = Array.Empty<string>(); 
  // Nullable Empty String Array List (NESAL)
  public static IList<string[]>? QebNESAL = (IList<string[]>?)new List<string[]>(); 
  // Nullable Empty Int String Array Dictionary (NEISAD)
  public static IDictionary<int, string[]>? QebNEISAD = (IDictionary<int, string[]>?)new Dictionary<int, string[]>();

  // non-nullable values

  // TODO: avoid use of this check as inconsistent across Nexus and Scribe ?
  public static string[] QebRazorAnonList = new string[] { "Index", "Help", "Examples" }; 

  public static JsonSerializerOptions QebKendoJsonOptions = new JsonSerializerOptions()
  {
    PropertyNamingPolicy = null,
    PropertyNameCaseInsensitive = false,
  };

} // end class

// end file