// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public ProvenanceExtnUxm GetEditableProvenanceExtnByKey(Guid guidKey)
  { return QueryStorableProvenanceExtnByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ProvenanceExtnUxm GetEditableProvenanceExtnByKey(string guidKey)
  { return GetEditableProvenanceExtnByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsProvenanceExtn GetStorableProvenanceExtnByKey(Guid guidKey)
  { return QueryStorableProvenanceExtnByKey(guidKey).SingleOrDefault(); }
  public DoorsProvenanceExtn GetStorableProvenanceExtnByKey(string guidKey)
  { return GetStorableProvenanceExtnByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsProvenanceExtn> QueryStorableProvenanceExtnByKey(Guid guidKey)
  {
    IQueryable<DoorsProvenanceExtn> qry = this.DoorsProvenanceExtns;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file