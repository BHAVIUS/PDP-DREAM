// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public DescriptionExtnUxm? GetEditableDescriptionExtnByKey(Guid guidKey)
  { return QueryStorableDescriptionExtnByKey(guidKey).ToEditable().SingleOrDefault(); }
  public DescriptionExtnUxm? GetEditableDescriptionExtnByKey(string guidKey)
  { return GetEditableDescriptionExtnByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public DoorsDescriptionExtn? GetStorableDescriptionExtnByKey(Guid guidKey)
  {
    DoorsDescriptionExtn? dto;
    try { dto = QueryStorableDescriptionExtnByKey(guidKey).SingleOrDefault(); }
    catch { dto = null; }
    return dto;
  }
  public DoorsDescriptionExtn? GetStorableDescriptionExtnByKey(string guidKey)
  { return GetStorableDescriptionExtnByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<DoorsDescriptionExtn?> QueryStorableDescriptionExtnByKey(Guid guidKey)
  {
    IQueryable<DoorsDescriptionExtn?> qry = this.DoorsDescriptionExtns;
    qry = qry.Where(ss => (ss.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file