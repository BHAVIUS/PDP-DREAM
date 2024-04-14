// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // ATTN: C# enum use must be internal only if/when used for PDP code
  // ATTN: if C# enum value not set in code, it defaults to 0
  // ATTN: do not use enums for NPDS apis, instead may use custom enumerators

  public enum AcgtCodeBranch
  {
    Net6Aoraki, Net7Cervin, Net8Gangkhar, Net8Tahtali
  }
  public const AcgtCodeBranch AcgtBranchDefault = AcgtCodeBranch.Net8Tahtali;

  public enum AcgtCodeRazor
  {
    Views, Pages, Components, Apis, All
  }
  public const AcgtCodeRazor AcgtRazorDefault = AcgtCodeRazor.All;

  public enum AcgtCodeDatabase
  {
    Core, PORTAL, DOORS, Nexus, Scribe
  }
  public const AcgtCodeDatabase AcgtDatabaseDefault = AcgtCodeDatabase.Core;

}

