// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public class PdpDbsqlContext : PdpDbsqlContextBase, INpdsDbsqlContext
{
  // ATTN: OnConfiguring method required with DbContextOptionsBuilder required for EF Core 
  // ATTN: if use Entity Developer, then must delete generated redundant copy of OnConfiguring()
  // ATTN: if use Entity Developer with base-derived class, then must delete generated redundant copy of HasChanges()
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    // TODO: recheck defaults to avoid null check
    // if (NPDSCP == null) { NPDSCP = new NpdsClient(NPDSCD.DatabaseTypeCore, NPDSSD.CoreDbconstr); }
    if (NPDSDC == null) { throw new ArgumentNullException(nameof(NPDSDC)); }
    NPDSDC.DbcontextIsConfigured(dbcob);
  }

  // constructor with typed and untyped DbContextOptionsBuilder required for EntityFrameworkCore 
  public PdpDbsqlContext(DbContextOptionsBuilder dbcob) : base(dbcob) { }
  public PdpDbsqlContext(DbContextOptionsBuilder<PdpDbsqlContext> dbcob) : base(dbcob) { }

  // constructors with typed NpdsClient or database connection strings
  public PdpDbsqlContext(INpdscwClient npdsc) : base(npdsc) { }
  public PdpDbsqlContext() : this(new NpdsClientWrace(NPDSCD.DatabaseTypeCore)) { }
  public PdpDbsqlContext(NpdsDatabaseType dbType) : base(dbType) { }

} // end class

// end file