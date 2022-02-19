// BingMapsService.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PDP.DREAM.CoreDataLib.Services;

public class BingMapsService : IGeolocater
{
  public BingMapsService(HttpClient client)
  {
    client.BaseAddress = new Uri(BingMapsRequestUriBase);
    BingMapsClient = client;
  }
  public const string BingMapsRequestUriBase = "http://dev.virtualearth.net/REST/v1/Locations?";
  public HttpClient BingMapsClient { get; }

  public string BingMapsRequestUrl(string apiKey, string country, string stateRegion, string postalCode, string cityLocality, string streetAddress)
  {
    var sb = new StringBuilder(BingMapsRequestUriBase);
    if (!string.IsNullOrEmpty(country)) { sb.AppendFormat("countryRegion={0}&", country); }
    if (!string.IsNullOrEmpty(stateRegion)) { sb.AppendFormat("adminDistrict={0}&", stateRegion); }
    if (!string.IsNullOrEmpty(postalCode)) { sb.AppendFormat("postalCode={0}&", postalCode); }
    if (!string.IsNullOrEmpty(cityLocality)) { sb.AppendFormat("locality={0}&", cityLocality); }
    if (!string.IsNullOrEmpty(streetAddress)) { sb.AppendFormat("addressLine={0}&", streetAddress); }
    sb.AppendFormat("key={0}", apiKey);
    return sb.ToString();
  }

  public Response? GetJsonResponse(string requestUrl)
  {
    var jsonRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl);
    var jsonResponse = BingMapsClient.Send(jsonRequest);
    Response? bingResponse = null;
    if (jsonResponse.IsSuccessStatusCode)
    {
      if (jsonResponse.StatusCode != HttpStatusCode.OK)
      {
        throw new Exception(string.Format("Server error (HTTP {0}: {1}).", jsonResponse.StatusCode, jsonResponse.ReasonPhrase));
      }
      DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
      bingResponse = (Response)jsonSerializer.ReadObject(jsonResponse.Content.ReadAsStream());
    }
    return bingResponse;
  }
  public Location? ParseLocationFromJsonResponse(Response? bingResponse)
  {
    Location? loc = null;
    if ((bingResponse?.StatusCode == 200) && (bingResponse?.ResourceSets?[0].EstimatedTotal > 0))
    {
      loc = (Location?)bingResponse?.ResourceSets?[0].Resources?[0];
    }
    return loc;
  }

  public string GetBingEntityType(Location? loc)
  { if (loc == null) return ""; else return loc.EntityType; }

  public string GetBingConfidence(Location? loc)
  { if (loc == null) return ""; else return loc.Confidence; }

  public string GetBingFormattedAddress(Location? loc)
  { if ((loc == null) || (loc.Address == null)) return ""; else return loc.Address.FormattedAddress; }

  public double? GetLatitude(Location? loc)
  { if ((loc == null) || (loc.Point == null)) return null; else return loc.Point?.Coordinates?[0]; }

  public double? GetLongitude(Location? loc)
  { if ((loc == null) || (loc.Point == null)) return null; else return loc.Point?.Coordinates?[1]; }
}

[DataContract]
public class BingAddress
{
  [DataMember(Name = "addressLine")]
  public string AddressLine { get; set; } = string.Empty;
  [DataMember(Name = "adminDistrict")]
  public string AdminDistrict { get; set; } = string.Empty;
  [DataMember(Name = "adminDistrict2")]
  public string AdminDistrict2 { get; set; } = string.Empty;
  [DataMember(Name = "countryRegion")]
  public string CountryRegion { get; set; } = string.Empty;
  [DataMember(Name = "formattedAddress")]
  public string FormattedAddress { get; set; } = string.Empty;
  [DataMember(Name = "locality")]
  public string Locality { get; set; } = string.Empty;
  [DataMember(Name = "postalCode")]
  public string PostalCode { get; set; } = string.Empty;
}

[DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
public class DataflowJob : Resource
{
  [DataMember(Name = "completedDate")]
  public string CompletedDate { get; set; } = string.Empty;
  [DataMember(Name = "createdDate")]
  public string CreatedDate { get; set; } = string.Empty;
  [DataMember(Name = "status")]
  public string Status { get; set; } = string.Empty;
  [DataMember(Name = "errorMessage")]
  public string ErrorMessge { get; set; } = string.Empty;
  [DataMember(Name = "failedEntityCount")]
  public int FailedEntityCount { get; set; }
  [DataMember(Name = "processedEntityCount")]
  public int ProcessedEntityCount { get; set; }
  [DataMember(Name = "totalEntityCount")]
  public int TotalEntityCount { get; set; }
}

[DataContract]
public class Hint
{
  [DataMember(Name = "hintType")]
  public string HintType { get; set; } = string.Empty;
  [DataMember(Name = "value")]
  public string Value { get; set; } = string.Empty;
}

[DataContract]
public class Instruction
{
  [DataMember(Name = "maneuverType")]
  public string ManeuverType { get; set; } = string.Empty;
  [DataMember(Name = "text")]
  public string Text { get; set; } = string.Empty;
  [DataMember(Name = "value")]
  public string Value { get; set; } = string.Empty;
}

[DataContract]
public class ItineraryItem
{
  [DataMember(Name = "travelMode")]
  public string TravelMode { get; set; } = string.Empty;
  [DataMember(Name = "travelDistance")]
  public double TravelDistance { get; set; }
  [DataMember(Name = "travelDuration")]
  public long TravelDuration { get; set; }
  [DataMember(Name = "maneuverPoint")]
  public Point? ManeuverPoint { get; set; }
  [DataMember(Name = "instruction")]
  public Instruction? Instruction { get; set; }
  [DataMember(Name = "compassDirection")]
  public string CompassDirection { get; set; } = string.Empty;
  [DataMember(Name = "hint")]
  public Hint[]? Hint { get; set; }
  [DataMember(Name = "warning")]
  public Warning[]? Warning { get; set; }
}

[DataContract]
public class Line
{
  [DataMember(Name = "point")]
  public Point[]? Point { get; set; }
  [DataMember(Name = "coordinates")]
  public double[][]? Coordinates { get; set; }
}

[DataContract]
public class Link
{
  [DataMember(Name = "role")]
  public string Role { get; set; } = string.Empty;
  [DataMember(Name = "name")]
  public string Name { get; set; } = string.Empty;
  [DataMember(Name = "value")]
  public string Value { get; set; } = string.Empty;
  [DataMember(Name = "url")]
  public string Url { get; set; } = string.Empty;
}

[DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
public class Location : Resource
{
  [DataMember(Name = "entityType")]
  public string EntityType { get; set; } = string.Empty;
  [DataMember(Name = "address")]
  public BingAddress? Address { get; set; }
  [DataMember(Name = "confidence")]
  public string Confidence { get; set; } = string.Empty;
}

[DataContract]
public class Point
{
  /// <summary>
  /// Latitude,Longitude
  /// </summary>
  [DataMember(Name = "coordinates")]
  public double[]? Coordinates { get; set; }
  //[DataMember(Name = "latitude")]
  //public double Latitude { get; set; }
  //[DataMember(Name = "longitude")]
  //public double Longitude { get; set; }
}

[DataContract]
[KnownType(typeof(Location))]
[KnownType(typeof(Route))]
[KnownType(typeof(DataflowJob))]
public class Resource
{
  [DataMember(Name = "name")]
  public string Name { get; set; } = string.Empty;
  [DataMember(Name = "id")]
  public string Id { get; set; } = string.Empty;
  [DataMember(Name = "link")]
  public Link[]? Link { get; set; }
  [DataMember(Name = "links")]
  public Link[]? Links { get; set; }
  [DataMember(Name = "point")]
  public Point? Point { get; set; }
  [DataMember(Name = "bbox")]
  public double[]? BoundingBox { get; set; }
}

[DataContract]
public class ResourceSet
{
  [DataMember(Name = "estimatedTotal")]
  public long EstimatedTotal { get; set; }
  [DataMember(Name = "resources")]
  public Resource[]? Resources { get; set; }
}

[DataContract]
public class Response
{
  [DataMember(Name = "copyright")]
  public string Copyright { get; set; } = string.Empty;
  [DataMember(Name = "brandLogoUri")]
  public string BrandLogoUri { get; set; } = string.Empty;
  [DataMember(Name = "statusCode")]
  public int StatusCode { get; set; }
  [DataMember(Name = "statusDescription")]
  public string StatusDescription { get; set; } = string.Empty;
  [DataMember(Name = "authenticationResultCode")]
  public string AuthenticationResultCode { get; set; } = string.Empty;
  [DataMember(Name = "errorDetails")]
  public string[]? errorDetails { get; set; }
  [DataMember(Name = "traceId")]
  public string TraceId { get; set; } = string.Empty;
  [DataMember(Name = "resourceSets")]
  public ResourceSet[]? ResourceSets { get; set; }
}

[DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
public class Route : Resource
{
  [DataMember(Name = "distanceUnit")]
  public string DistanceUnit { get; set; } = string.Empty;
  [DataMember(Name = "durationUnit")]
  public string DurationUnit { get; set; } = string.Empty;
  [DataMember(Name = "travelDistance")]
  public double TravelDistance { get; set; }
  [DataMember(Name = "travelDuration")]
  public long TravelDuration { get; set; }
  [DataMember(Name = "routeLegs")]
  public RouteLeg[]? RouteLegs { get; set; }
  [DataMember(Name = "routePath")]
  public RoutePath? RoutePath { get; set; }
}

[DataContract]
public class RouteLeg
{
  [DataMember(Name = "travelDistance")]
  public double TravelDistance { get; set; }
  [DataMember(Name = "travelDuration")]
  public long TravelDuration { get; set; }
  [DataMember(Name = "actualStart")]
  public Point? ActualStart { get; set; }
  [DataMember(Name = "actualEnd")]
  public Point? ActualEnd { get; set; }
  [DataMember(Name = "startLocation")]
  public Location? StartLocation { get; set; }
  [DataMember(Name = "endLocation")]
  public Location? EndLocation { get; set; }
  [DataMember(Name = "itineraryItems")]
  public ItineraryItem[]? ItineraryItems { get; set; }
}

[DataContract]
public class RoutePath
{
  [DataMember(Name = "line")]
  public Line? Line { get; set; }
}

[DataContract]
public class Warning
{
  [DataMember(Name = "warningType")]
  public string WarningType { get; set; } = string.Empty;
  [DataMember(Name = "severity")]
  public string Severity { get; set; } = string.Empty;
  [DataMember(Name = "value")]
  public string Value { get; set; } = string.Empty;
}
