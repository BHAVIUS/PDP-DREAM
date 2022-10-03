// PrcServiceTag.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Reflection;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // constraint is serviceTag = NpdsConstants.RegexTagToken

  // default values

  // requested values

  public string ServiceTagReqst
  {
    set {
      reqServiceTag = value;
      if (!string.IsNullOrEmpty(reqServiceTag)) { ServiceTag = reqServiceTag; }
    }
    get { return reqServiceTag; }
  }
  private string reqServiceTag = string.Empty;

  // validated values
  public string ServiceTag
  {
    set {
      serviceTag = ValidateServiceTag(value);
      if (!string.IsNullOrEmpty(serviceTag))
      {
        switch (ServiceType)
        {
          case NpdsServiceType.Nexus:
            DiristryTagReqst = serviceTag;
            break;
          case NpdsServiceType.PORTAL:
            RegistryTagReqst = serviceTag;
            break;
          case NpdsServiceType.DOORS:
            DirectoryTagReqst = serviceTag;
            break;
          case NpdsServiceType.Scribe:
            RegistrarTagReqst = serviceTag;
            break;
          default:
            throw new Exception("invalid ServiceType in ServiceTag set");
        }
      }
    }
    get {
      if (string.IsNullOrEmpty(serviceTag))
      {
        switch (ServiceType)
        {
          case NpdsServiceType.Nexus:
            serviceTag = DiristryTagDeflt;
            break;
          case NpdsServiceType.PORTAL:
            serviceTag = RegistryTagDeflt;
            break;
          case NpdsServiceType.DOORS:
            serviceTag = DirectoryTagDeflt;
            break;
          case NpdsServiceType.Scribe:
            serviceTag = RegistrarTagDeflt;
            break;
          default:
            throw new Exception($"case not implemented for ServiceType value {ServiceType.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
        }
      }
      return serviceTag;
    }
  }
  private string serviceTag = string.Empty;

  // validators

  private string ValidateServiceTag(string reqTag)
  {
    // TODO: this method must be converted/migrated to generalized dynamic database lookup
    //    of any/all properties associated with the selected ServiceTag
    // TODO: recode to eliminate switch case dependencies for assignment of any properties 
    //     whether assigned registrar, dbconnectionstring, servicenote, or any other
    // TODO: must redefine what it means for a ServiceTag to be validated against
    //    constraints for the corresponding ServerType and ServiceType
    string tag = reqTag;
    if (!string.IsNullOrEmpty(reqTag))
    {
      reqTag = reqTag.ToUpper();
      switch (reqTag)
      {
        // NPDS Registrar
        case "NPDS-ROOT":
          // TODO: set preferred service/database
          break;

        // BHA Registrar
        case "BHA-NEXUS":
        case "BHA-PORTAL":
        case "BHA-DOORS":
        case "BHA-SCRIBE":
        case "BRAINWATCH":
        case "EYWA":
        case "GAIA":
        case "HELPME":
        case "SOLOMON":
          // DbConnectionString = NpdsServiceDefaults.GetValues.NpdsBhaRegistrarDbconstr;
          break;

        // GTG Registrar
        case "GTG-NEXUS":
        case "GTG-PORTAL":
        case "GTG-DOORS":
        case "GTG-SCRIBE":
        case "BIOPORT":
        case "CTGAMING":
        case "GENESCENE":
        case "MANRAY":
        case "OSLER":
          // DbConnectionString = NpdsServiceDefaults.GetValues.NpdsGtgRegistrarDbconstr;
          break;

        // PDP Registrar
        case "PDP-NEXUS":
        case "PDP-PORTAL":
        case "PDP-DOORS":
        case "PDP-SCRIBE":
        case "PDP-DREAM":
        case "AVICENNA":
        case "BEACON":
        case "DAVINCI":
        case "FIDENTINUS":
        case "KNUTH":
        case "MARTIALIS":
        case "SOCRATES":
          // DbConnectionString = NpdsServiceDefaults.GetValues.NpdsPdpRegistrarDbconstr;
          break;

        // TODO: move these to NPDSLINKS
        case "NLMMESH":
          DbConnectionString = NPDSSD.NpdsNlmmeshDbconstr;
          ServiceNote = NLMMESHNOTICE;
          break;
        case "NLMMICAD":
          DbConnectionString = NPDSSD.NpdsNlmmicadDbconstr;
          ServiceNote = NLMMICADNOTICE;
          break;
        default:
          break;
      }
      if (reqTag.Contains("NEXUS"))
      { DiristryTag = tag; tag = DiristryTag; }
      else if (reqTag.Contains("PORTAL"))
      { RegistryTag = tag; tag = RegistryTag; }
      else if (reqTag.Contains("DOORS"))
      { DirectoryTag = tag; tag = DirectoryTag; }
      else if (reqTag.Contains("SCRIBE"))
      { RegistrarTag = tag; tag = RegistrarTag; }
      else // assume everything else defaults to Nexus
      { DiristryTag = tag; tag = DiristryTag; }
    }
    return tag;
  }

  private string ValidateServiceTag(string tagRequested, string tagConstraints)
  {
    string tag = string.Empty;
    if (!string.IsNullOrEmpty(tagRequested))
    {
      if (string.IsNullOrEmpty(tagConstraints) || (tagConstraints.ToUpper().Contains(tagRequested.ToUpper()))) { tag = tagRequested; }
      else { tag = tagConstraints.Split(new char[] { '|' })[0]; }
    }
    return tag;
  }

}
