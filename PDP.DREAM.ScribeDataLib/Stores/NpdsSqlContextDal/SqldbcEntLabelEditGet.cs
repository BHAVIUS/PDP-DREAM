// SqldbcUilEntLabelEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public EntityLabelEditModel GetEditableEntityLabelByKey(Guid guidKey)
  { return QueryStorableEntityLabelByKey(guidKey).ToEditable().SingleOrDefault(); }
  public EntityLabelEditModel GetEditableEntityLabelByKey(string guidKey)
  { return GetEditableEntityLabelByKey(Guid.Parse(guidKey)); }

} // end class

// end file