// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // TODO: update / revise / simplify / reconcile with ServiceTag!!!
  private string? cnctnstr = string.Empty;
  public string? DatabaseConstr
  {
    // set only for overrides when testing ? or better to avoid use ?
    set {
      cnctnstr = value;
      switch (DatabaseType)
      {
        case NpdsDatabaseType.SIAA:
          QebiDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.Core:
          CoreDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.Nexus:
          NexusDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.PORTAL:
          PortalDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.DOORS:
          DoorsDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.Scribe:
          ScribeDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.ACMS:
          cnctnstr = AcmsDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.Vocab:
          cnctnstr = VocabDbconstr = cnctnstr;
          break;
        case NpdsDatabaseType.Cache:
        default:
          CacheDbconstr = cnctnstr;
          break;
      }
    }
    // TODO: implement and activate NodeType switch in NpdscpNodeType.cs
    // TODO: NodeType switch must be reconciled with DatabaseType switch
    //switch (NodeType)
    //{
    //  case NpdsNodeType.Authoritative:
    //    break;
    //  case NpdsNodeType.Caching:
    //    cnctnstr = CacheDbconstr;
    //    break;
    //  case NpdsNodeType.Forwarding:
    //  case NpdsNodeType.None:
    //    throw new NotImplementedException();
    //} // end switch
    get {
      switch (DatabaseType)
      {
        case NpdsDatabaseType.SIAA:
          cnctnstr = QebiDbconstr;
          break;
        case NpdsDatabaseType.Core:
          cnctnstr = CoreDbconstr;
          break;
        case NpdsDatabaseType.Nexus:
          cnctnstr = NexusDbconstr;
          break;
        case NpdsDatabaseType.PORTAL:
          cnctnstr = PortalDbconstr;
          break;
        case NpdsDatabaseType.DOORS:
          cnctnstr = DoorsDbconstr;
          break;
        case NpdsDatabaseType.Scribe:
          cnctnstr = ScribeDbconstr;
          break;
        case NpdsDatabaseType.ACMS:
          cnctnstr = AcmsDbconstr;
          break;
        case NpdsDatabaseType.Vocab:
          cnctnstr = VocabDbconstr;
          break;
        case NpdsDatabaseType.Cache:
        default:
          cnctnstr = CacheDbconstr;
          break;
      }
      return cnctnstr;
    } // end get
  } // end property

} // end class

// end file