// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// PDP.DREAM.CoreDataLib.Stores.NexusDbsqlContext
public partial class NexusDbsqlContext : CoreDbsqlContext
{
  // ATTN: OnConfiguring method required with DbContextOptionsBuilder required for EF Core 
  // ATTN: if use Entity Developer, then must delete generated redundant copy of OnConfiguring()
  // ATTN: if use Entity Developer with base-derived class, then must delete generated redundant copy of HasChanges()
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    if (NPDSDC == null) { NPDSDC = new NpdsClientWrace(NPDSCD.DatabaseTypeNexus); }
    NPDSDC.DbcontextIsConfigured(dbcob);
  }

  // constructor with typed and untyped DbContextOptionsBuilder required for EntityFrameworkCore 
  public NexusDbsqlContext(DbContextOptionsBuilder dbcob) : base(dbcob) { }
  public NexusDbsqlContext(DbContextOptionsBuilder<NexusDbsqlContext> dbcob) : base(dbcob) { }

  // constructors with typed NpdsClient or database connection strings
  public NexusDbsqlContext(INpdscwClient cnpds) : base(cnpds) { }
  public NexusDbsqlContext() : this(new NpdsClientWrace(NPDSCD.DatabaseTypeNexus)) { }

} // end class

// end file