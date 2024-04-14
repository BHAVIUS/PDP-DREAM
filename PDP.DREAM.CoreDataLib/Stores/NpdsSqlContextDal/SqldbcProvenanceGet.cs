// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public ProvenanceUxm GetEditableProvenanceByKey(Guid guidKey)
  { return QueryStorableProvenanceByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ProvenanceUxm GetEditableProvenanceByKey(string guidKey)
  { return GetEditableProvenanceByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsProvenance GetStorableProvenanceByKey(Guid guidKey)
  { return QueryStorableProvenanceByKey(guidKey).SingleOrDefault(); }
  public DoorsProvenance GetStorableProvenanceByKey(string guidKey)
  { return GetStorableProvenanceByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsProvenance> QueryStorableProvenanceByKey(Guid guidKey)
  {
    IQueryable<DoorsProvenance> qry = this.DoorsProvenances;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file