// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // GetEditables //
  public OtherTextUxm GetEditableOtherTextByKey(Guid guidKey)
  { return QueryStorableOtherTextByKey(guidKey).ToEditable().SingleOrDefault(); }
  public OtherTextUxm GetEditableOtherTextByKey(string guidKey)
  { return GetEditableOtherTextByKey(Guid.Parse(guidKey)); }

  // GetStorables //
  public PortalOtherText GetStorableOtherTextByKey(Guid guidKey)
  { return QueryStorableOtherTextByKey(guidKey).SingleOrDefault(); }
  public PortalOtherText GetStorableOtherTextByKey(string guidKey)
  { return GetStorableOtherTextByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<PortalOtherText> QueryStorableOtherTextByKey(Guid guidKey)
  {
    IQueryable<PortalOtherText> qry = this.PortalOtherTexts;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }

} // end class

// end file