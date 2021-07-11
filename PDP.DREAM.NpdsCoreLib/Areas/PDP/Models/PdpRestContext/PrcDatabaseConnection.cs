// PrcDatabaseConnection.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // TODO: update / revise / simplify / reconcile with PrcServiceTag!!!

    public string DbConnectionString
    {
      set { dbConnStr = value; }
      get
      {
        if (string.IsNullOrEmpty(dbConnStr)) // reset to a default only if empty
        {
          switch (NodeType)
          {
            case NpdsConst.NodeType.Authoritative:
              switch (DatabaseType)
              {
                case NpdsConst.DatabaseType.Nexus:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsNexusAuth1Dbconstr; }
                   break;
                case NpdsConst.DatabaseType.PORTAL:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsPortalAuth1Dbconstr; }
                  break;
                case NpdsConst.DatabaseType.DOORS:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsDoorsAuth1Dbconstr; }
                   break;
                default:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsCoreDbconstr; }
                  break;
              }
              break;
            case NpdsConst.NodeType.Caching:
              switch (DatabaseType)
              {
                case NpdsConst.DatabaseType.Nexus:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsNexusCacheDbconstr; }
                  break;
                case NpdsConst.DatabaseType.PORTAL:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsPortalCacheDbconstr; }
                  break;
                case NpdsConst.DatabaseType.DOORS:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsDoorsCacheDbconstr; }
                  break;
                default:
                  if (string.IsNullOrEmpty(dbConnStr))
                  { dbConnStr = NpdsServiceDefaults.GetValues.NpdsCoreDbconstr; }
                  break;
              }
              break;
            default:
              if (string.IsNullOrEmpty(dbConnStr))
              { dbConnStr = NpdsServiceDefaults.GetValues.NpdsCoreDbconstr; }
              break;
          }
        }
        return dbConnStr;
      }

    }
    private string dbConnStr = string.Empty;

  }

}
