// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // TODO: update / revise / simplify / reconcile with ServiceTag
  private string? dbcnstr = ESS;
  public string? DatabaseConstr
  {
    set {
      dbcnstr = value;
      switch (DatabaseType.EName)
      {
        case DdeDatabaseType.QEBI:
          DbcnstrQebi = dbcnstr;
          break;
        case DdeDatabaseType.Core:
          DbcnstrCore = dbcnstr;
          break;
        case DdeDatabaseType.Nexus:
          DbcnstrNexus = dbcnstr;
          break;
        case DdeDatabaseType.PORTAL:
          DbcnstrPortal = dbcnstr;
          break;
        case DdeDatabaseType.DOORS:
          DbcnstrDoors = dbcnstr;
          break;
        case DdeDatabaseType.Scribe:
          DbcnstrScribe = dbcnstr;
          break;
        case DdeDatabaseType.ACMS:
          DbcnstrAcms = dbcnstr;
          break;
        case DdeDatabaseType.Bridge:
          DbcnstrBridge = dbcnstr;
          break;
        case DdeDatabaseType.Vocab:
          DbcnstrVocab = dbcnstr;
          break;
        case DdeDatabaseType.Cache:
        default:
          DbcnstrCache = dbcnstr;
          break;
      } // end switch
    } // end set

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
      switch (DatabaseType.EName)
      {
        case DdeDatabaseType.QEBI:
          dbcnstr = DbcnstrQebi;
          break;
        case DdeDatabaseType.Core:
          dbcnstr = DbcnstrCore;
          break;
        case DdeDatabaseType.Nexus:
          dbcnstr = DbcnstrNexus;
          break;
        case DdeDatabaseType.PORTAL:
          dbcnstr = DbcnstrPortal;
          break;
        case DdeDatabaseType.DOORS:
          dbcnstr = DbcnstrDoors;
          break;
        case DdeDatabaseType.Scribe:
          dbcnstr = DbcnstrScribe;
          break;
        case DdeDatabaseType.ACMS:
          dbcnstr = DbcnstrAcms;
          break;
        case DdeDatabaseType.Bridge:
          dbcnstr = DbcnstrBridge;
          break;
        case DdeDatabaseType.Vocab:
          dbcnstr = DbcnstrVocab;
          break;
        case DdeDatabaseType.Cache:
        default:
          dbcnstr = DbcnstrCache;
          break;
      }
      return dbcnstr;
    } // end get

  } // end property

} // end class

// end file