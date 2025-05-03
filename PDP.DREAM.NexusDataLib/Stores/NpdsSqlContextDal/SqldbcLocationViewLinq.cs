// SqldbcUilLocationViewLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
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
        FieldFormatCode = r.FieldFormatCodeRef,
        FieldFormatName = r.FieldFormatName,
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

} // end class

// end file