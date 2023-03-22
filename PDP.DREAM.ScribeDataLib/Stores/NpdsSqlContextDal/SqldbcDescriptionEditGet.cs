// SqldbcUilDescriptionEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // GetEditables //

  public DescriptionEditModel GetEditableDescriptionByKey(Guid guidKey)
  { return QueryStorableDescriptionByKey(guidKey).ToEditable().SingleOrDefault(); }
  public DescriptionEditModel GetEditableDescriptionByKey(string guidKey)
  { return GetEditableDescriptionByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  // QueryStorables //

} // end class

// end file