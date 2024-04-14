// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public EntityLabelUxm GetEditableEntityLabelByKey(Guid guidKey)
  { return QueryStorableEntityLabelByKey(guidKey).ToEditable().SingleOrDefault(); }
  public EntityLabelUxm GetEditableEntityLabelByKey(string guidKey)
  { return GetEditableEntityLabelByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public CoreEntityLabel GetStorableEntityLabelByKey(Guid guidKey)
  { return QueryStorableEntityLabelByKey(guidKey).SingleOrDefault(); }
  public CoreEntityLabel GetStorableEntityLabelByKey(string guidKey)
  { return GetStorableEntityLabelByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<CoreEntityLabel> QueryStorableEntityLabelByKey(Guid guidKey)
  {
    IQueryable<CoreEntityLabel> qry = this.CoreEntityLabels;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file