// IDoorsLocationEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Models;

public interface IDoorsLocationEditModel : INexusEditModelBase
{
  bool LocationIsPrincipal { get; set; }
  string LocationXml { get; set; }
  string LocationXhtml { get; set; }
  string LocationRdfOwl { get; set; }

  string DisplayText { get; set; }
  string DisplayImageUrl { get; set; }
  string UrlWebAddress { get; set; }
  string EmailAddress { get; set; }
  string StreetAddress { get; set; }
  string ExtendedAddress { get; set; }
  string CityLocality { get; set; }
  string StateRegion { get; set; }
  string Country { get; set; }
  string PostalCode { get; set; }
  string Telephone { get; set; }

  string GeocodeType { get; set; }
  string GeocodeConfidence { get; set; }
  string FormattedAddress { get; set; }

  double? Latitude { get; set; }
  double? Longitude { get; set; }
}
