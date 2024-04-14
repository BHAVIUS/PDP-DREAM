// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public DescriptionUxm? GetEditableDescriptionByKey(Guid guidKey)
  { return QueryStorableDescriptionByKey(guidKey).ToEditable().SingleOrDefault(); }
  public DescriptionUxm? GetEditableDescriptionByKey(string guidKey)
  { return GetEditableDescriptionByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsDescription? GetStorableDescriptionByKey(Guid guidKey)
  {
    DoorsDescription? dto;
    try { dto = QueryStorableDescriptionByKey(guidKey).SingleOrDefault(); }
    catch { dto = null; }
    return dto;
  }
  public DoorsDescription? GetStorableDescriptionByKey(string guidKey)
  { return GetStorableDescriptionByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsDescription?> QueryStorableDescriptionByKey(Guid guidKey)
  {
    IQueryable<DoorsDescription?> qry = this.DoorsDescriptions;
    qry = qry.Where(ss => (ss.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file