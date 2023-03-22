// SqldbcUilResrepStemEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // GetEditables //
  public NexusResrepEditModel? GetEditableResrepStemByRKey(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<NexusResrepEditModel?> GetEditableResrepStemByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public NexusResrepEditModel? GetEditableResrepStemByIKey(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<NexusResrepEditModel?> GetEditableResrepStemByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public NexusResrepEditModel? GetEditableResrepStemByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepStem> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusStemByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusStemByRKey(guidKey);
    }
    var row = qry.ToEditable().SingleOrDefault();
    return row;
  }
  public NexusResrepEditModel? GetEditableResrepStemByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepStemByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepEditModel?> GetEditableResrepStemByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepStem> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusStemByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusStemByRKey(guidKey);
    }
    var row = qry.ToEditable().SingleOrDefaultAsync();
    return row;
  }
  public Task<NexusResrepEditModel?> GetEditableResrepStemByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepStemByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  
  // GetStorables //

  // QueryStorables //

} // end class

// end file