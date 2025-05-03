// SqldbcUilResrepLeafViewGet.cs 
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
  public NexusResrepViewModel? GetViewableResrepLeafByRKey(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepLeafByIKey(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel? GetViewableResrepLeafByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepLeaf> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusLeafByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusLeafByRKey(guidKey);
    }
    var row = qry.ToViewable().SingleOrDefault();
    return row;
  }
  public NexusResrepViewModel? GetViewableResrepLeafByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepLeafByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepLeaf> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusLeafByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusLeafByRKey(guidKey);
    }
    var row = qry.ToViewable().SingleOrDefaultAsync();
    return row;
  }
  public Task<NexusResrepViewModel?> GetViewableResrepLeafByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetViewableResrepLeafByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  
  // GetStorables //
  public INexusResrepLeaf GetStorableNexusLeafByRKey(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).SingleOrDefault(); }
  public INexusResrepLeaf GetStorableNexusLeafByIKey(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).SingleOrDefault(); }

  // QueryStorables //
  public IQueryable<INexusResrepLeaf> QueryStorableNexusLeafByRKey(Guid guidKey)
  {
    IQueryable<INexusResrepLeaf> qry = this.NexusResrepLeafs;
    qry = qry.Where(r => (r.RecordGuidKey == guidKey));
    return qry;
  }
  public IQueryable<INexusResrepLeaf> QueryStorableNexusLeafByIKey(Guid guidKey)
  {
    IQueryable<INexusResrepLeaf> qry = this.NexusResrepLeafs;
    qry = qry.Where(r => (r.InfosetGuidKey == guidKey));
    return qry;
  }

} // end class

// end file