// SqldbcUilResrepLeafEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;
using System.Threading.Tasks;

using Kendo.Mvc.Extensions;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // GetEditables //
  public NexusResrepEditModel? GetEditableResrepLeafByRKey(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<NexusResrepEditModel?> GetEditableResrepLeafByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusLeafByRKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public NexusResrepEditModel? GetEditableResrepLeafByIKey(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<NexusResrepEditModel?> GetEditableResrepLeafByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusLeafByIKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public NexusResrepEditModel? GetEditableResrepLeafByKey(Guid guidKey, bool isInfosetKey = false)
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
    var row = qry.ToEditable().SingleOrDefault();
    return row;
  }
  public NexusResrepEditModel? GetEditableResrepLeafByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepLeafByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepEditModel?> GetEditableResrepLeafByKeyAsync(Guid guidKey, bool isInfosetKey = false)
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
    var row = qry.ToEditable().SingleOrDefaultAsync();
    return row;
  }
  public Task<NexusResrepEditModel?> GetEditableResrepLeafByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepLeafByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  
  // GetStorables //

  // QueryStorables //

} // end class

// end file