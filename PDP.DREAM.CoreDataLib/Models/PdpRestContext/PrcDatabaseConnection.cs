// PrcDatabaseConnection.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpRestContext
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
          case NpdsConst.NodeType.Authoritative:
            switch (DatabaseType)
            {
              case NpdsConst.DatabaseType.Nexus:
                dbConnStr = NpdsServiceDefaults.Values.NpdsDiristryDbconstr;
                break;
              case NpdsConst.DatabaseType.PORTAL:
                dbConnStr = NpdsServiceDefaults.Values.NpdsRegistryDbconstr;
                break;
              case NpdsConst.DatabaseType.DOORS:
                dbConnStr = NpdsServiceDefaults.Values.NpdsDirectoryDbconstr;
                break;
              case NpdsConst.DatabaseType.Scribe:
                dbConnStr = NpdsServiceDefaults.Values.NpdsRegistrarDbconstr;
                break;
              case NpdsConst.DatabaseType.Core:
              default:
                dbConnStr = NpdsServiceDefaults.Values.NpdsCoreDbconstr;
                break;
            }
            break;
          case NpdsConst.NodeType.Forwarding:
          case NpdsConst.NodeType.Caching:
            switch (DatabaseType)
            {
              case NpdsConst.DatabaseType.Nexus:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = NpdsServiceDefaults.Values.NpdsNexusCacheDbconstr; }
                break;
              case NpdsConst.DatabaseType.PORTAL:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = NpdsServiceDefaults.Values.NpdsPortalCacheDbconstr; }
                break;
              case NpdsConst.DatabaseType.DOORS:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = NpdsServiceDefaults.Values.NpdsDoorsCacheDbconstr; }
                break;
              case NpdsConst.DatabaseType.Core:
              default:
                if (string.IsNullOrEmpty(dbConnStr))
                { dbConnStr = NpdsServiceDefaults.Values.NpdsCoreDbconstr; }
                break;
            }
            break;
          case NpdsConst.NodeType.None:
            dbConnStr = NpdsServiceDefaults.Values.NpdsCoreDbconstr;
            break;
        } // end switch
      } // end if
      return dbConnStr;
    } // end get
  } // end property
} // end class
