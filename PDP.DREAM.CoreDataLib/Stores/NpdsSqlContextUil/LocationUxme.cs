// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class LocationExtnUxm : LocationUxm
{
  public LocationExtnUxm()
  {
    // ATTN: non-nullable fields may be considered 'required' by some edit form validators
    //  but this consideration may not apply to Telerik Kendo widgets if not declared
    //  in the Kendo Jquery Html wrapper for the widget
    itemXnam = LocationItemXnam;
  }

  // TODO: consider migrating DisplayText and DisplayImageUrl
  //    to a core model for use with all 10 infosubset facets (5 PORTAL, 5 DOORS)
  // TODO: implement DisplayImageText for tooltip to go with DisplayImageUrl
  // TODO: consider generalizing alternative display properties
  //    in manner dependent on FieldFormat

  private string? ssDispText = "";
  public string? DisplayText
  {
    set { ssDispText = value; }
    get {
      ssDispText = (string.IsNullOrWhiteSpace(ssDispText) ? UrlWebAddress : ssDispText);
      return ssDispText;
    }
  }

  public string? DisplayImageUrl { get; set; } = "";

  public string? UrlWebAddress { get; set; } = "";
  public DateTime? UrlWebAddressValidated { get; set; } = null;
  public string? UrlWebAddressHtml { get { return $"<a href='{UrlWebAddress}' target='_blank'>{UrlWebAddress}</a>"; } }

  public string? EmailAddress { get; set; } = "";
  public DateTime? EmailAddressValidated { get; set; } = null;

  public string? StreetAddress { get; set; } = "";
  public DateTime? StreetAddressValidated { get; set; } = null;

  // TODO: rename ExtendedAddress to avoid use of "Extended" else deprecate?
  //  what name is used by the address validation service for this property?
  public string? ExtendedAddress { get; set; } = "";
  public string? CityLocality { get; set; } = "";
  public string? StateRegion { get; set; } = "";
  public string? Country { get; set; } = "";
  public string? PostalCode { get; set; } = "";
  public string? Telephone { get; set; } = "";

  public string? GeocodeType { get; set; } = "";
  public string? GeocodeConfidence { get; set; } = "";
  public string? FormattedAddress { get; set; } = "";
  public double? Latitude { get; set; } = null;
  public double? Longitude { get; set; } = null;

  public string FormatLocationUrl()
  {
    var xhtml = "";
    if (!string.IsNullOrEmpty(UrlWebAddress))
    {
      if (!string.IsNullOrEmpty(DisplayText) && !string.IsNullOrEmpty(DisplayImageUrl))
      { xhtml = string.Format("<a href='{0}' target='_blank'><img alt='{1}' src='{2}' width='32' /></a>", UrlWebAddress, DisplayText, DisplayImageUrl); }
      else if (!string.IsNullOrEmpty(DisplayText))
      { xhtml = string.Format("<a href='{0}' title='{0}' target='_blank'>{1}</a>", UrlWebAddress, DisplayText); }
      else if (!string.IsNullOrEmpty(DisplayImageUrl))
      { xhtml = string.Format("<a href='{0}' target='_blank'><img alt='{0}' src='{1}' width='32' /></a>", UrlWebAddress, DisplayImageUrl); }
      else
      { xhtml = UrlWebAddressHtml; }
    }
    return xhtml;
  }

  private string? ssLocnXhtml;
  public string? LocationXhtml
  {
    get { return (string.IsNullOrEmpty(ssLocnXhtml) ? FormatLocationXhtml() : ssLocnXhtml); }
    set { ssLocnXhtml = value; }
  }

  // TODO: reconcile FormatLocationXhtml with FormatLocationVcard
  //   then deprecate the former, retain the latter
  protected string FormatLocationXhtml()
  {
    var xhtml = new StringBuilder("<div class='vcard'><div class='adr'>");
    if (!string.IsNullOrEmpty(UrlWebAddress) || !string.IsNullOrEmpty(EmailAddress) || !string.IsNullOrEmpty(Telephone))
    {
      xhtml.Append("<div>");
      if (!string.IsNullOrEmpty(UrlWebAddress)) { xhtml.AppendFormat("URL: <a class='url' href='{0}' target='_blank'>{0}</a> ", UrlWebAddress); }
      if (!string.IsNullOrEmpty(EmailAddress)) { xhtml.AppendFormat("Email: <a class='email' href='mailto:{0}'>{0}</a> ", EmailAddress); }
      if (!string.IsNullOrEmpty(Telephone)) { xhtml.AppendFormat("Telephone: <span class='tel'>{0}</span> ", Telephone); }
      xhtml.Append("</div>");
    }
    if (!string.IsNullOrEmpty(FormattedAddress))
    {
      xhtml.AppendFormat("<div>{0}</div>", FormattedAddress);
    }
    else if (!string.IsNullOrEmpty(StreetAddress) || !string.IsNullOrEmpty(CityLocality) || !string.IsNullOrEmpty(Country))
    {
      xhtml.Append("<div>");
      if (!string.IsNullOrEmpty(StreetAddress)) { xhtml.AppendFormat("{0} ", StreetAddress); }
      if (!string.IsNullOrEmpty(ExtendedAddress)) { xhtml.AppendFormat("{0}, ", ExtendedAddress); }
      if (!string.IsNullOrEmpty(CityLocality)) { xhtml.AppendFormat("{0} ", CityLocality); }
      if (!string.IsNullOrEmpty(StateRegion)) { xhtml.AppendFormat("{0} ", StateRegion); }
      if (!string.IsNullOrEmpty(PostalCode)) { xhtml.AppendFormat("{0} ", PostalCode); }
      if (!string.IsNullOrEmpty(Country)) { xhtml.AppendFormat("{0} ", Country); }
      xhtml.Append("</div>");
    }
    if ((Latitude.HasValue == true) || (Longitude.HasValue == true))
    {
      xhtml.AppendFormat("<div class='geo'>Lat: <span class='latitude'>{0}</span>, Lon: <span class='longitude'>{1}</span></div>",
        Latitude.GetValueOrDefault().ToString("F4"), Longitude.GetValueOrDefault().ToString("F4"));
    }
    xhtml.Append("</div></div>");
    return xhtml.ToString();
  }

  // TODO: code analogous Vcards for persons and organizations when EntityType is Person or Organization
  // in other words make Vcard formatted xhtml dependent on the EntityType,
  // but how to prevent redundancy with other infosubsets/facets/fields?
  public string FormatLocationVcard()
  {
    string xhtml = @"<div class='vcard'>";

    //"<div class='fn'>" + @PersonFirstName + " " + @PersonLastName + "</div>" +
    //"<div class='n'>" +
    //    "<span class='honorific-prefixes'></span>" +
    //    "<span class='given-name'>" + @PersonFirstName + "</span>" +
    //    "<span class='additional-names'></span>" +
    //    "<span class='family-name'>" + @PersonLastName + "</span>" +
    //    "<span class='honorific-suffixes'></span>" +
    //"</div>" +
    //"<div class='org'>" + @OrganizationName + "</div>" +

    if (!string.IsNullOrWhiteSpace(StreetAddress) || !string.IsNullOrWhiteSpace(CityLocality)
      || !string.IsNullOrWhiteSpace(PostalCode) || !string.IsNullOrWhiteSpace(Country))
    {
      xhtml = xhtml + "<div class='adr'>";
      if ((Latitude != 0) && (Longitude != 0))
      {
        xhtml = xhtml +
          "<div class='geo'>" +
            "<span class='latitude'>" + Latitude.ToString() + "</span>" +
            "<span class='longitude'>" + Longitude.ToString() + "</span>" +
          "</div>";
      }
      xhtml = xhtml +
        "<div class='street-address'>" + StreetAddress?.ToString() + "</div>" +
        "<div class='extended-address'>" + ExtendedAddress?.ToString() + "</div>" +
        "<div>" +
          "<span class='locality'>" + CityLocality?.ToString() + "</span>" +
          "<span class='region'>" + StateRegion?.ToString() + "</span>" +
          "<span class='postal-code'>" + PostalCode?.ToString() + "</span>" +
          "<span class='country-name'>" + Country?.ToString() + "</span>" +
        "</div>";
      xhtml = xhtml + "</div>";
    }
    if (!string.IsNullOrWhiteSpace(Telephone))
    {
      xhtml = xhtml + "<div class='tel'>" + Telephone.ToString() + "</div>";
    }
    if (!string.IsNullOrWhiteSpace(EmailAddress))
    {
      xhtml = xhtml + "<div class='email' href='mailto:" + EmailAddress.ToString() + "'>" + EmailAddress.ToString() + "</div>";
    }
    if (!string.IsNullOrWhiteSpace(UrlWebAddress))
    {
      xhtml = xhtml + "<div class='url' href='" + UrlWebAddress.ToString() + "'>" + UrlWebAddress.ToString() + "</div>";
    }
    xhtml = xhtml + "</div>";
    return xhtml;
  }

} // end class

// end file
