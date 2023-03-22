// SqldbContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

// PDP.DREAM.NexusDataLib.Stores.NexusDbsqlContext
public partial class NexusDbsqlContext : CoreDbsqlContext
{
  // ATTN: OnConfiguring method required with DbContextOptionsBuilder required for EF Core 
  // ATTN: if use Entity Developer, then must delete generated redundant copy of OnConfiguring()
  // ATTN: if use Entity Developer with base-derived class, then must delete generated redundant copy of HasChanges()
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    if (NPDSCP == null) { NPDSCP = new NpdsClient(NpdsDatabaseType.Nexus); }
    if (!dbcob.IsConfigured) { dbcob.UseSqlServer(NPDSCP.DatabaseConstr); }
    if (dbcob.IsConfigured)
    {
      if (NPDSCP.ClientAppSiaaGuid.IsNullOrEmpty() && !PDPSS.AppSecureUiaaGuid.IsEmpty())
      { NPDSCP.ClientAppSiaaGuid = PDPSS.AppSecureUiaaGuid; }
    }
    else
    {
      throw new Exception("DbContext builder with NPDSCP has not been configured");
    }
  }

  // constructor with typed and untyped DbContextOptionsBuilder required for EntityFrameworkCore 
  public NexusDbsqlContext(DbContextOptionsBuilder dbcob) : base(dbcob) { }
  public NexusDbsqlContext(DbContextOptionsBuilder<NexusDbsqlContext> dbcob) : base(dbcob) { }

  // constructors with typed NpdsClient or database connection strings
  public NexusDbsqlContext(INpdsClient npdsc) : base(npdsc) { }
  public NexusDbsqlContext() : this(new NpdsClient(NpdsDatabaseType.Nexus)) { }

} // end class

// end file