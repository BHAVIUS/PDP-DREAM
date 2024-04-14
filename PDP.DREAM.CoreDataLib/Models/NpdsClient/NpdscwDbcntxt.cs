// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// NPDS Client Wrace (Npdscw*)
public partial class NpdsClientWrace : INpdscwClient
{
  // QEBI User Data Context = QUDC
  public INpdsDbsqlContext? DbciQebi { get; set; } = null;

  // PDP Core Data Context = PCDC
  public INpdsDbsqlContext? DbciCore { get; set; } = null;

  // PDP Nexus Data Context = PNDC
  public INpdsDbsqlContext? DbciNexus { get; set; } = null;

  // PDP PORTAL Data Context = PPDC
  public INpdsDbsqlContext? DbciPortal { get; set; } = null;

  // PDP DOORS Data Context = PDDC
  public INpdsDbsqlContext? DbciDoors { get; set; } = null;

  // PDP Scribe Data Context = PSDC
  public INpdsDbsqlContext? DbciScribe { get; set; } = null;

  public INpdsDbsqlContext? DbciAcms { get; set; } = null;

  public INpdsDbsqlContext? DbciBridge { get; set; } = null;

  public INpdsDbsqlContext? DbciVocab { get; set; } = null;

  public INpdsDbsqlContext? DbciCache { get; set; } = null;


  // instance properties inherited by WRACE
  // enable changes from NPDSCD defaults
  public string? DbcnstrQebi { get; set; } = NPDSCD.CiaamDbconstr;
  public string? DbcnstrCore { get; set; } = NPDSCD.CoreDbconstr;
  public string? DbcnstrNexus { get; set; } = NPDSCD.NexusDbconstr;
  public string? DbcnstrPortal { get; set; } = NPDSCD.PortalDbconstr;
  public string? DbcnstrDoors { get; set; } = NPDSCD.DoorsDbconstr;
  public string? DbcnstrScribe { get; set; } = NPDSCD.ScribeDbconstr;
  public string? DbcnstrAcms { get; set; } = NPDSCD.AcmsDbconstr;
  public string? DbcnstrBridge { get; set; } = NPDSCD.BridgeDbconstr;
  public string? DbcnstrVocab { get; set; } = NPDSCD.VocabDbconstr;
  public string? DbcnstrCache { get; set; } = NPDSCD.CacheDbconstr;

} // end class

// end file