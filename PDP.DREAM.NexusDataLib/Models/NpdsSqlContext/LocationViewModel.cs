// LocationViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text;

namespace PDP.DREAM.NexusDataLib.Models
{
  public class LocationViewModel : NexusViewModelBase
  {
    public LocationViewModel()
    {
      if (string.IsNullOrEmpty(locXhtml)) { locXhtml = CreateLocationXhtml(); }
      if (string.IsNullOrEmpty(locDispText)) { locDispText = UrlWebAddress; }
    }

    public string? LocationXhtml
    {
      get { return (string.IsNullOrEmpty(locXhtml) ? CreateLocationXhtml() : locXhtml); }
      set { locXhtml = value; }
    }
    private string? locXhtml;


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

  }

}