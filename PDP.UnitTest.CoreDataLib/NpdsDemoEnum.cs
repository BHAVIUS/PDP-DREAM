// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.UnitTest.CoreDataLib;

public class NpdsDemoEnum : PdpEnumManager
{
  public NpdsDemoEnum() { Selected = PdpEnumManager.GetByCode<NpdsDemoEnum>(0); }

  private NpdsDemoEnum(byte code, string name, string desc) : base(code, name, desc) { }

  public static readonly NpdsDemoEnum Core = new NpdsDemoEnum(0, "Core", "NPDS Core");
  public static readonly NpdsDemoEnum Nexus = new NpdsDemoEnum(1, "Nexus", "Nexus Diristry");
  public static readonly NpdsDemoEnum PORTAL = new NpdsDemoEnum(2, "PORTAL", "PORTAL Registry");
  public static readonly NpdsDemoEnum DOORS = new NpdsDemoEnum(3, "DOORS", "DOORS Directory");
  public static readonly NpdsDemoEnum Scribe = new NpdsDemoEnum(4, "Scribe", "Scribe Registrar");
}
