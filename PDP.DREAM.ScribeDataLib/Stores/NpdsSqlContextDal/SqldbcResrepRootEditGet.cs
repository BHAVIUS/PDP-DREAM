// SqldbcUilResrepRootEditGet.cs 
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
  public NexusResrepEditModel? GetEditableResrepRootByRKey(Guid guidKey)
  { return QueryStorableNexusRootByRKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<NexusResrepEditModel?> GetEditableResrepRootByRKeyAsync(Guid guidKey)
  { return QueryStorableNexusRootByRKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public NexusResrepEditModel? GetEditableResrepRootByIKey(Guid guidKey)
  { return QueryStorableNexusRootByIKey(guidKey).ToEditable().SingleOrDefault(); }
  public Task<NexusResrepEditModel?> GetEditableResrepRootByIKeyAsync(Guid guidKey)
  { return QueryStorableNexusRootByIKey(guidKey).ToEditable().SingleOrDefaultAsync(); }
  public NexusResrepEditModel? GetEditableResrepRootByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepRoot> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusRootByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusRootByRKey(guidKey);
    }
    var row = qry.ToEditable().SingleOrDefault();
    return row;
  }
  public NexusResrepEditModel? GetEditableResrepRootByKey(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepRootByKey(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  public Task<NexusResrepEditModel?> GetEditableResrepRootByKeyAsync(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<INexusResrepRoot> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableNexusRootByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableNexusRootByRKey(guidKey);
    }
    var row = qry.ToEditable().SingleOrDefaultAsync();
    return row;
  }
  public Task<NexusResrepEditModel?> GetEditableResrepRootByKeyAsync(string guidKey, bool isInfosetKey = false)
  {
    return GetEditableResrepRootByKeyAsync(PdpGuid.ParseToNonNullable(guidKey), isInfosetKey);
  }
  
  // GetStorables //

  // QueryStorables //

} // end class

// end file