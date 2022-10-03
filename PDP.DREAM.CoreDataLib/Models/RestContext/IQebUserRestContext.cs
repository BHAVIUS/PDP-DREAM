// IQebUserRestContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreDataLib.Models;

public interface IQebUserRestContext
{
  // NpdsRestServiceParameters PSP { get; }
  // NpdsRestRequestOptions PRO { get; }
  // NpdsRestResponseSettings PRS { get; }

  public NpdsDatabaseType DatabaseType { get; set; }
  public NpdsDatabaseAccess DatabaseAccess { get; set; }
  public NpdsEntityType EntityType { get; set; }
  // public NpdsFieldFormat FieldFormat { get; set; }
  // public NpdsFieldRule FieldRule { get; set; }
  public NpdsInfosetStatus InfosetStatus { get; set; }
  public NpdsNodeType NodeType { get; set; }
  public NpdsRecordAccess RecordAccess { get; set; }
  public NpdsResrepFormat ResrepFormat { get; set; }
  public NpdsSearchFilter SearchFilter { get; set; }
  public NpdsSearchScope SearchScope { get; set; }
  public NpdsServerType ServerType { get; set; }
  public NpdsServiceType ServiceType { get; set; }
  public NpdsMessageFormat MessageFormat { get; set; }

  bool ItemCanBeAccessed { get; }
  bool ItemCanBeVerbosed { get; }
  bool ItemDoesArchive { get; }
  bool ItemDoesVerbose { get; }
  bool ItemIsConcise { get; set; }
  bool ItemIsPrivate { get; set; }

  string? StatusCode { get; set; }
  string? StatusName { get; set; }
  string? StatusXhtml { get; set; }

 }

