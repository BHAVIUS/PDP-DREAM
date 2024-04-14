// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<LocationExtnUxm?> ToEditable(this IQueryable<DoorsLocationExtn> query)
  {
    IQueryable<LocationExtnUxm?> rows =
      from r in query
      select new LocationExtnUxm
      {
        RRFgroupGuid = r.FgroupGuid,
        RRRecordGuid = r.RecordGuid,
        HasIndex = r.HasIndex,
        HasPriority = r.HasPriority,
        IsMarked = r.IsMarked,
        IsPrincipal = r.IsPrincipal,
        IsDeleted = r.IsDeleted,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        CreatedByAgentAlias = (r.CreatedByAgentAlias ?? ""),
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        UpdatedByAgentAlias = (r.UpdatedByAgentAlias ?? ""),
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuid,
        DeletedByAgentAlias = (r.DeletedByAgentAlias ?? ""),
        //
        ManagedByAgentGuid = r.ManagedByAgentGuid,
        ManagedByAgentAlias = (r.ManagedByAgentAlias ?? ""),
        //
        FieldFormatCode = r.FieldFormatCode,
        FieldFormatName = r.FieldFormatName,
        Location = (r.Location ?? ""),
        DisplayText = (r.DisplayText ?? ""),
        DisplayImageUrl = (r.DisplayImageUrl ?? ""),
        UrlWebAddress = (r.UrlWebAddress ?? ""),
        UrlWebAddressValidated = r.UrlWebAddressValidated,
        EmailAddress = (r.EmailAddress ?? ""),
        EmailAddressValidated = r.EmailAddressValidated,
        StreetAddress = (r.StreetAddress ?? ""),
        StreetAddressValidated = r.StreetAddressValidated,
        // TODO: change Extended to some alternative or deprecate use
        ExtendedAddress = (r.ExtendedAddress ?? ""),  
        CityLocality = (r.CityLocality ?? ""),
        StateRegion = (r.StateRegion ?? ""),
        Country = (r.Country ?? ""),
        PostalCode = (r.PostalCode ?? ""),
        Telephone = (r.Telephone ?? ""),
        GeocodeType = (r.GeocodeType ?? ""),
        GeocodeConfidence = (r.GeocodeConfidence ?? ""),
        Latitude = r.Latitude,
        Longitude = r.Longitude,
        FormattedAddress = (r.FormattedAddress ?? ""),
      };
    return rows;
  }

} // end class

// end file