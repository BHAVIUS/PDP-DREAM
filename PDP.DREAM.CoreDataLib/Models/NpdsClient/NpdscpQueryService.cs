// QurcContentFromNpdsService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // content for NPDS service
  // list item status properties
  public string? StatusName { get; set; } = string.Empty;
  // short text for simple display
  public string? StatusNote { get; set; } = string.Empty;
  // long text for XHTML display of full message
  public string? StatusXhtml { get; set; } = string.Empty;
  public string? ServiceError { set; get; } = string.Empty;
  public string? ServiceNote { set; get; }  = string.Empty;


  // content for NPDS request
  public string? RequestQuestion { get; set; }= string.Empty;
  public string? RequestNote { get; set; }= string.Empty;


  // content for NPDS response

  private string? respNote = string.Empty;
  public string? ResponseNote
  {
    set {
      var sb = new StringBuilder(ResponseNote);
      sb.AppendLine(value);
      respNote = sb.ToString();
    }
    get {
      if (string.IsNullOrEmpty(respNote))
      {
        if (!string.IsNullOrEmpty(ServiceNote)) { respNote = ServiceNote; };
      }
      return respNote;
    }
  }
  public string? ResponseStatus { get; set; }= string.Empty;
  public string? ResponseHeader { get; set; }= string.Empty;

  public NpdsResrepList? ResponseAnswer { set; get; }
  public NpdsResrepList? ResponseRelated { set; get; }
  public NpdsResrepList? ResponseReferred { set; get; }

  public NpdsResrepList? CoreRecords { set; get; }
  public NpdsResrepList? PortalRecords { set; get; }
  public NpdsResrepList? DoorsRecords { set; get; }
  public NpdsResrepList? NexusRecords { set; get; }

} // end class

// end file