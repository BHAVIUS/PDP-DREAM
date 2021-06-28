using System;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class LocationEditModel : NexusEditModelBase
  {
    // non-nullable fields may be considered required by some edit form validators
    public LocationEditModel()
    {
      itemXnam = NpdsConst.LocationItemXnam;
    }

    public string? Location { get; set; } = string.Empty;

    public string? DisplayText
    {
      get { return (string.IsNullOrEmpty(locDispText) ? UrlWebAddress : locDispText); }
      set { locDispText = value; }
    }
    private string? locDispText = string.Empty;

    public string? DisplayImageUrl { get; set; } = string.Empty;

    public string? UrlWebAddress { get; set; } = string.Empty;

    public DateTime? UrlWebAddressValidated { get; set; } = null;

    public string? UrlWebAddressHtml { get { return $"<a href='{UrlWebAddress}' target='_blank'>{UrlWebAddress}</a>"; } }
    public string? EmailAddress { get; set; } = string.Empty;

    public DateTime? EmailAddressValidated { get; set; } = null;

    public string? StreetAddress { get; set; } = string.Empty;
    public DateTime? StreetAddressValidated { get; set; } = null;
    public string? ExtendedAddress { get; set; } = string.Empty;
    public string? CityLocality { get; set; } = string.Empty;
    public string? StateRegion { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    public string? Telephone { get; set; } = string.Empty;

    public string? GeocodeType { get; set; } = string.Empty;
    public string? GeocodeConfidence { get; set; } = string.Empty;
    public string? FormattedAddress { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

  }

}