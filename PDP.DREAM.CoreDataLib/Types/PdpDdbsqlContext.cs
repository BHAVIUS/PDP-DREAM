// PdpXunitBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpDbsqlContext<TDbsql> : DbsqlContextBase
  where TDbsql : class, new()
{
  public PdpDbsqlContext() 
  { 
    // npdsDbcontxt = new PdpDbsqlContext<TDbsql>(); 
  }

  public NpdsDatabaseType NpdsDbtype
  { get { return base.NPDSCP.DatabaseType; } }
  public string? NpdsDbconstr
  { get { return base.NPDSCP.DatabaseConstr; } }

  private PdpDbsqlContext<TDbsql> npdsDbcontxt;
  public PdpDbsqlContext<TDbsql> NpdsDbcontxt
  {
    set { this.npdsDbcontxt = value; }
    get { return this.npdsDbcontxt; }
  }

}
