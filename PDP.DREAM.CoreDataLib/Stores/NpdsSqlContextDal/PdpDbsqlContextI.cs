// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public interface INpdsDbsqlContext : IPdpDbsqlContext
{
  public INpdscwClient NPDSDC { get; set; }
}

public interface IPdpDbsqlContext
{
  public SqlConnection? DbsqlConnect();

  public void DbsqlDisconnect();

  public DatabaseFacade DbsqlDatabase { get; }

  public DbContext DbsqlContext { get; }

}