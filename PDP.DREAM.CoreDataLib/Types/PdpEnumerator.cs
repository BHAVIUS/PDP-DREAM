// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

// TODO: rebuild the enums as PdpEnumRecords 
// so can maintain direct control of any implemented features
// PdpRestContext AI System uses enum convention of None = 0 
//
// TODO: rebuild with db sourced values for enums
//  allowing for dynamic extended values in range [101..255]
//  while reserving range [0..100] for PDP core
// use explicit byte codes for those fields stored in database record classs
// dynamic database "enums" corresponding to those stored in
// database record classs are meant to be extensible
// int <= 100 PDP reserved, int >= 101 PDP extended
//
// TODO: convert to dynamic database lookup on app startup
// Facets == aka NPDS infosubset child record of NPDS infoset parent record
// public const byte FacetIndexDefault = 1;
// public const byte FacetPriorityDefault = 255;

public abstract class PdpEnumerator
{
  public PdpEnumerator(Byte ecod, String enam, String edes)
  {
    ECode = ecod;
    EName = enam;
    EDesc = edes;
  }

  // byte code number in range 0,1,2,...,255
  public Byte ECode { get; init; }

  // string name compatible with nvarchar(32)
  public String EName { get; init; }

  // string description compatible with nvarchar(128)
  public String EDesc { get; init; }
}

public class NpdsClientRole
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsDatabaseAccess
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsDatabaseType
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsEntityType
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsFieldFormat
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsInfosetStatus
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsMessageFormat
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsNodeType
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsQuadCast
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsRecordAccess
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsResrepFormat
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsSearchFilter
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsSearchScope
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsSemanticLevel
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsServerType
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

public class NpdsServiceType
  (byte ecod, string enam, string edes) : PdpEnumerator(ecod, enam, edes);

// end file