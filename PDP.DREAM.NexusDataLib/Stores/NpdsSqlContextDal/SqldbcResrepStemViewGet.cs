// SqldbcUilResrepStemViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;
using System.Threading.Tasks;

using Kendo.Mvc.Extensions;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public NexusResrepViewModel? GetViewableResrepStemByRKey(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepStemByIKey(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepStemByKey(Guid guidKey, bool isInfosetKey = false)
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
    var row = qry.ToViewable().SingleOrDefault();
    return row;
  }
  public NexusResrepViewModel? GetViewableResrepStemByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepStemByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByKeyAsync(Guid guidKey, bool isInfosetKey = false)
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
    var row = qry.ToViewable().SingleOrDefaultAsync();
    return row;
  }
  public Task<NexusResrepViewModel?> GetViewableResrepStemByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepStemByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  
  // GetStorables //
  public INexusResrepStem GetStorableNexusStemByRKey(Guid guidKey)
  { return QueryStorableNexusStemByRKey(guidKey).SingleOrDefault(); }
  public INexusResrepStem GetStorableNexusStemByIKey(Guid guidKey)
  { return QueryStorableNexusStemByIKey(guidKey).SingleOrDefault(); }

  // QueryStorables //
  public IQueryable<INexusResrepStem> QueryStorableNexusStemByRKey(Guid guidKey)
  {
    IQueryable<INexusResrepStem> qry = this.NexusResrepStems;
    qry = qry.Where(r => (r.RecordGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepStem> QueryStorableNexusStemByIKey(Guid guidKey)
  {
    IQueryable<INexusResrepStem> qry = this.NexusResrepStems;
    qry = qry.Where(r => (r.InfosetGuidKey == guidKey));
    return qry;
  }

} // end class

// end file