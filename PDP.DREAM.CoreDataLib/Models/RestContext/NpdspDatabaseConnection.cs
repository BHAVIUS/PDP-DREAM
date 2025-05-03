// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // TODO: update / revise / simplify / reconcile with PrcServiceTag!!!
  private string? dbConnStr = null;
  public string DbConnectionString
  {
    set { dbConnStr = value; }
    get {
      // reset to a default only if empty
      if (string.IsNullOrEmpty(dbConnStr))
      {
        switch (NodeType)
        {
          case PdpAppConst.NpdsNodeType.Authoritative:
            switch (DatabaseType)
            {
              case PdpAppConst.NpdsDatabaseType.Nexus:
                dbConnStr = PdpAppStatus.NPDSSD.NpdsDiristryDbconstr;
                break;
              case PdpAppConst.NpdsDatabaseType.PORTAL:
                dbConnStr = PdpAppStatus.NPDSSD.NpdsRegistryDbconstr;
                break;
              case PdpAppConst.NpdsDatabaseType.DOORS:
                dbConnStr = PdpAppStatus.NPDSSD.NpdsDirectoryDbconstr;
                break;
              case PdpAppConst.NpdsDatabaseType.Scribe:
                dbConnStr = PdpAppStatus.NPDSSD.NpdsRegistrarDbconstr;
                break;
              case PdpAppConst.NpdsDatabaseType.Core:
              default:
                dbConnStr = PdpAppStatus.NPDSSD.NpdsCoreDbconstr;
                break;
            }
            break;
          case PdpAppConst.NpdsNodeType.Forwarding:
          case PdpAppConst.NpdsNodeType.Caching:
            switch (DatabaseType)
            {
              case PdpAppConst.NpdsDatabaseType.Nexus:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = PdpAppStatus.NPDSSD.NpdsNexusCacheDbconstr; }
                break;
              case PdpAppConst.NpdsDatabaseType.PORTAL:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = PdpAppStatus.NPDSSD.NpdsPortalCacheDbconstr; }
                break;
              case PdpAppConst.NpdsDatabaseType.DOORS:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = PdpAppStatus.NPDSSD.NpdsDoorsCacheDbconstr; }
                break;
              case PdpAppConst.NpdsDatabaseType.Core:
              default:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = PdpAppStatus.NPDSSD.NpdsCoreDbconstr; }
                break;
            }
            break;
          case PdpAppConst.NpdsNodeType.None:
            dbConnStr = PdpAppStatus.NPDSSD.NpdsCoreDbconstr;
            break;
        } // end switch
      } // end if
      return dbConnStr;
    } // end get
  } // end property

} // end class

// end file