// SqldbContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// PDP.DREAM.CoreDataLib.Stores.CoreDbsqlContext
public partial class CoreDbsqlContext : DbsqlContextBase, INpdsDataService
{
  // ATTN: OnConfiguring method required with DbContextOptionsBuilder required for EF Core 
  // ATTN: if use Entity Developer, then must delete generated redundant copy of OnConfiguring()
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    if (NPDSCP == null) { NPDSCP = new NpdsClient(NpdsDatabaseType.Core); }
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
  public CoreDbsqlContext(DbContextOptionsBuilder dbcob) : base(dbcob) { }
  public CoreDbsqlContext(DbContextOptionsBuilder<CoreDbsqlContext> dbcob) : base(dbcob) { }

  // constructors with typed NpdsClient or database connection strings
  public CoreDbsqlContext(INpdsClient npdsc) : base(npdsc) { }
  public CoreDbsqlContext() : this(new NpdsClient(NpdsDatabaseType.Core)) { }

} // end class

// end file