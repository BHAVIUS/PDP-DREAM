// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// PDP.DREAM.CoreDataLib.Stores.CoreDbsqlContext
public partial class CoreDbsqlContext : PdpDbsqlContextBase
{
  // ATTN: OnConfiguring method required with DbContextOptionsBuilder required for EF Core 
  // ATTN: if use Entity Developer, then must delete generated redundant copy of OnConfiguring()
  // ATTN: if use Entity Developer with base-derived class, then must delete generated redundant copy of HasChanges()
  protected override void OnConfiguring(DbContextOptionsBuilder dbcob)
  {
    if (NPDSDC == null) { NPDSDC = new NpdsClientWrace(NPDSCD.DatabaseTypeCore); }
    NPDSDC.DbcontextIsConfigured(dbcob);
  }

  // constructor with typed and untyped DbContextOptionsBuilder required for EntityFrameworkCore 
  public CoreDbsqlContext(DbContextOptionsBuilder dbcob) : base(dbcob) { }
  public CoreDbsqlContext(DbContextOptionsBuilder<CoreDbsqlContext> dbcob) : base(dbcob) { }

  // constructors with typed NpdsClient or database connection strings
  public CoreDbsqlContext(INpdscwClient cnpds) : base(cnpds)
  {
    bingMaps = new BingMapsService(new HttpClient());
  }
  public CoreDbsqlContext() : this(new NpdsClientWrace(NPDSCD.DatabaseTypeCore))
  {
    bingMaps = new BingMapsService(new HttpClient());
  }

  protected readonly BingMapsService? bingMaps;

  public void LoadNpdsServiceCache()
  {
    var npdsCache = new PdpTagGuidDictionary();
    var npdsList = GetCccNpdsServiceList();
    foreach (SelectListItem npdsItem in npdsList)
    {
      var npdsTag = npdsItem.Text;
      var npdsGuid = Guid.Parse(npdsItem.Value);
#if DEBUG
      Debug.WriteLine($"npdsTag = '{npdsTag}', npdsGuid = '{npdsGuid}';");
#endif
      npdsCache.Add(npdsTag, npdsGuid);
    }
    // cache of NPDS Services PrincipalTags and InfosetGuids
    // ATTN: note use of InfosetGuids and not RecordGuids for the ResRep
    NPDSCD.NpdsServiceCache = npdsCache;
  }

} // end class

// end file