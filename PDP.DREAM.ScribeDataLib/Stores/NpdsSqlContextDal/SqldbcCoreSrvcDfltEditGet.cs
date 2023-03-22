// SqldbcUilServiceDefaultEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // GetEditables //

  public ServiceDefaultEditModel GetEditableServiceDefaultByKey(Guid guidKey)
  { return QueryServiceDefaultByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ServiceDefaultEditModel GetEditableServiceDefaultByKey(string guidKey)
  { return GetEditableServiceDefaultByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  public NexusCoreServiceDefault GetStorableServiceDefaultByKey(Guid guidKey)
  { return QueryServiceDefaultByKey(guidKey).SingleOrDefault(); }
  public NexusCoreServiceDefault GetStorableServiceDefaultByKey(string guidKey)
  { return GetStorableServiceDefaultByKey(Guid.Parse(guidKey)); }

  // QueryStorables //

  public IQueryable<NexusCoreServiceDefault> QueryServiceDefaultByKey(Guid guidKey)
  {
    IQueryable<NexusCoreServiceDefault> qry = this.NexusCoreServiceDefaults;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file