// LocationViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Utilities.PdpStringFrasFormFile;

namespace PDP.DREAM.NexusDataLib.Models;

public class LocationViewModel : CoreResrepModelBase
{
  public LocationViewModel()
  {
    // ATTN: non-nullable fields may be considered 'required' by some edit form validators
    //  but this consideration may not apply to Telerik Kendo widgets if not declared
    //  in the Kendo Jquery Html wrapper for the widget
    itemXnam = PdpAppConst.LocationItemXnam;
    if (string.IsNullOrEmpty(ssLocnXhtml)) { ssLocnXhtml = CreateLocationXhtml(); }
    if (string.IsNullOrEmpty(ssDispText)) { ssDispText = UrlWebAddress; }
  }

  public string? Location { get; set; } = string.Empty;

  private string ssLocnHtml = string.Empty;
  public string LocationHtml
  {
    get {
      if (string.IsNullOrEmpty(Location)) { ssLocnHtml = string.Empty; }
      else { ssLocnHtml = Location.StringEscapeHashLiteral(); }
      return ssLocnHtml;
    }
  }

  private string ssLocn128 = string.Empty;
  public string Location128
  {
    get {
      if (string.IsNullOrEmpty(Location)) { ssLocn128 = string.Empty; }
      else { ssLocn128 = Location.ToTruncatedPhrase(128).StringEscapeHashLiteral(); }
      return ssLocn128;
    }
  }

  private string? ssLocnXhtml;
  public string? LocationXhtml
  {
    get { return (string.IsNullOrEmpty(ssLocnXhtml) ? CreateLocationXhtml() : ssLocnXhtml); }
    set { ssLocnXhtml = value; }
  }

  // TODO: consider migrating DisplayText and DisplayImageUrl
  //    to a core model for use with all 10 Infosubsets (5 PORTAL, 5 DOORS)
  // TODO: consider generalizing alternative display properties dependent on FieldFormat

  private string? ssDispText = string.Empty;
  public string? DisplayText
  {
    get { return (string.IsNullOrEmpty(ssDispText) ? UrlWebAddress : ssDispText); }
    set { ssDispText = value; }
  }

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
  public double? Latitude { get; set; } = null;
  public double? Longitude { get; set; } = null;

  protected string CreateLocationXhtml()
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

  public string DisplayFormatLocationUrl(string locUrl, string dispText, string dispImgUrl)
  {
    string urlDispHtml = string.Empty;
    if (!string.IsNullOrEmpty(locUrl))
    {
      if (!string.IsNullOrEmpty(dispText) && !string.IsNullOrEmpty(dispImgUrl))
      { urlDispHtml = string.Format("<a href='{0}' target='_blank'><img alt='{1}' src='{2}' width='32' /></a>", locUrl, dispText, dispImgUrl); }
      else if (!string.IsNullOrEmpty(dispText))
      { urlDispHtml = string.Format("<a href='{0}' title='{0}' target='other'>{1}</a>", locUrl, dispText); }
      else if (!string.IsNullOrEmpty(dispImgUrl))
      { urlDispHtml = string.Format("<a href='{0}' target='_blank'><img alt='{0}' src='{1}' width='32' /></a>", locUrl, dispImgUrl); }
      else
      { urlDispHtml = string.Format("<a href='{0}' target='_blank'>{0}</a>", locUrl); }
    }
    return urlDispHtml;
  }

} // end class

// end file
