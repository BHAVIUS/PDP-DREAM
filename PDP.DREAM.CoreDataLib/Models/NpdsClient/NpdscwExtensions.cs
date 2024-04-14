// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static class NpdscwExtensions
{
  public static NpdsClientWrace SetDatabaseType(this NpdsClientWrace cnpds, NpdsDatabaseType dbType)
  {
    cnpds.DatabaseType = dbType;
    return cnpds;
  }

} // end class

// end file