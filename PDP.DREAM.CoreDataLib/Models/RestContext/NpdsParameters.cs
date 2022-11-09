// NpdsParams.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters : INpdsParameters
{
  // list item properties
  public bool ItemIsPrivate { get; set; } = false;
  public bool ItemIsConcise { get; set; } = false;
  public bool ItemCanBeAccessed
  { get { return (!ItemIsPrivate || ClientIsAuthorized); } }
  public bool ItemCanBeVerbosed // "verbosed" is coined term meaning "displayed verbosely" (CT 2011/10/15)
  { get { return (!ItemIsConcise || ClientIsAuthorized); } }
  public bool ItemDoesArchive
  { get { return (ItemCanBeAccessed && ArchiveFormat); } }
  public bool ItemDoesVerbose
  { get { return (ItemCanBeVerbosed && VerboseFormat); } }

  // list item status properties
  public string? StatusName { get; set; } = string.Empty;
  // short text for simple display
  public string? StatusNote { get; set; } = string.Empty;
  // long text for XHTML display of full message
  public string? StatusXhtml { get; set; } = string.Empty;

} // end class

// end file