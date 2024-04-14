// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //

  public ResrepServiceUxm GetEditableResrepServiceByKey(Guid guidKey)
  { return QueryResrepServiceByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ResrepServiceUxm GetEditableResrepServiceByKey(string guidKey)
  { return GetEditableResrepServiceByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  public CoreResrepService GetStorableResrepServiceByKey(Guid guidKey)
  { return QueryResrepServiceByKey(guidKey).SingleOrDefault(); }
  public CoreResrepService GetStorableResrepServiceByKey(string guidKey)
  { return GetStorableResrepServiceByKey(Guid.Parse(guidKey)); }

  // QueryStorables //

  public IQueryable<CoreResrepService> QueryResrepServiceByKey(Guid guidKey)
  {
    IQueryable<CoreResrepService> qry = this.CoreResrepServices;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file