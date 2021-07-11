// SqldbcUilLocationView.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static LocationViewModel ToViewable(this NexusLocation r)
    {
      var nre = new LocationViewModel()
      {
        RRFgroupGuid = r.FgroupGuidKey,
        RRRecordGuid = r.RecordGuidRef,
        HasIndex = r.HasIndex,
        HasPriority = r.HasPriority,
        IsMarked = r.IsMarked,
        IsPrincipal = r.IsPrincipal,
        IsDeleted = r.IsDeleted,
        ManagedByAgentGuid = r.ManagedByAgentGuidRef,
        ManagedByAgentName = r.ManagedByAgentUserName,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuidRef,
        CreatedByAgentName = r.CreatedByAgentUserName,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
        UpdatedByAgentName = r.UpdatedByAgentUserName,
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuidRef,
        DeletedByAgentName = r.DeletedByAgentUserName,
        //
        Location = r.Location,
        DisplayText = r.DisplayText,
        DisplayImageUrl = r.DisplayImageUrl,
        UrlWebAddress = r.UrlWebAddress,
        UrlWebAddressValidated = r.UrlWebAddressValidated,
        EmailAddress = r.EmailAddress,
        EmailAddressValidated = r.EmailAddressValidated,
        StreetAddress = r.StreetAddress,
        StreetAddressValidated = r.StreetAddressValidated,
        ExtendedAddress = r.ExtendedAddress,
        CityLocality = r.CityLocality,
        StateRegion = r.StateRegion,
        Country = r.Country,
        PostalCode = r.PostalCode,
        Telephone = r.Telephone,
        GeocodeType = r.GeocodeType,
        GeocodeConfidence = r.GeocodeConfidence,
        FormattedAddress = r.FormattedAddress,
        Latitude = r.Latitude,
        Longitude = r.Longitude
      };
      return nre;
    }

    public static IQueryable<LocationViewModel> ToViewable(this IQueryable<NexusLocation> query)
    {
      IQueryable<LocationViewModel> rows =
        from r in query
        select new LocationViewModel
        {
          RRFgroupGuid = r.FgroupGuidKey,
          RRRecordGuid = r.RecordGuidRef,
          HasIndex = r.HasIndex,
          HasPriority = r.HasPriority,
          IsMarked = r.IsMarked,
          IsPrincipal = r.IsPrincipal,
          IsDeleted = r.IsDeleted,
          ManagedByAgentGuid = r.ManagedByAgentGuidRef,
          ManagedByAgentName = r.ManagedByAgentUserName,
          CreatedOn = r.CreatedOn,
          CreatedByAgentGuid = r.CreatedByAgentGuidRef,
          CreatedByAgentName = r.CreatedByAgentUserName,
          UpdatedOn = r.UpdatedOn,
          UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
          UpdatedByAgentName = r.UpdatedByAgentUserName,
          DeletedOn = r.DeletedOn,
          DeletedByAgentGuid = r.DeletedByAgentGuidRef,
          DeletedByAgentName = r.DeletedByAgentUserName,
          //
          Location = r.Location,
          DisplayText = r.DisplayText,
          DisplayImageUrl = r.DisplayImageUrl,
          UrlWebAddress = r.UrlWebAddress,
          UrlWebAddressValidated = r.UrlWebAddressValidated,
          EmailAddress = r.EmailAddress,
          EmailAddressValidated = r.EmailAddressValidated,
          StreetAddress = r.StreetAddress,
          StreetAddressValidated = r.StreetAddressValidated,
          ExtendedAddress = r.ExtendedAddress,
          CityLocality = r.CityLocality,
          StateRegion = r.StateRegion,
          Country = r.Country,
          PostalCode = r.PostalCode,
          Telephone = r.Telephone,
          GeocodeType = r.GeocodeType,
          GeocodeConfidence = r.GeocodeConfidence,
          FormattedAddress = r.FormattedAddress,
          Latitude = r.Latitude,
          Longitude = r.Longitude
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<LocationViewModel> ListViewableLocations(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<LocationViewModel> result;
      try
      {
        IQueryable<NexusLocation> qry = this.NexusLocations;
        if (PRC.ClientHasAdminAccess || PRC.ClientHasEditorAccess)
        { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
        else
        {
          if (isLimited) { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuidRef == PRC.AgentGuid)); }
          else { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false)); }
        }
        result = qry.OrderBy(r => r.HasPriority).ToViewable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<LocationViewModel>();
      }
      return result;
    }

    public LocationViewModel GetViewableLocationByKey(Guid guidKey)
    { return QueryStorableLocationByKey(guidKey).ToViewable().SingleOrDefault(); }
    public LocationViewModel GetViewableLocationByKey(string guidKey)
    { return GetViewableLocationByKey(Guid.Parse(guidKey)); }

    public NexusLocation GetStorableLocationByKey(Guid guidKey)
    { return QueryStorableLocationByKey(guidKey).SingleOrDefault(); }
    public NexusLocation GetStorableLocationByKey(string guidKey)
    { return GetStorableLocationByKey(Guid.Parse(guidKey)); }

    public IQueryable<NexusLocation> QueryStorableLocationByKey(Guid guidKey)
    {
      IQueryable<NexusLocation> qry = this.NexusLocations;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
