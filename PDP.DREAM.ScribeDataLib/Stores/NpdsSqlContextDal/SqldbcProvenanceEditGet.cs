// SqldbcUilProvenanceEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ProvenanceEditModel GetEditableProvenanceByKey(Guid guidKey)
  { return QueryStorableProvenanceByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ProvenanceEditModel GetEditableProvenanceByKey(string guidKey)
  { return GetEditableProvenanceByKey(Guid.Parse(guidKey)); }

} // end class

// end file