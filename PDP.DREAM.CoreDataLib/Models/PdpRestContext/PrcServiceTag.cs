// PrcServiceTag.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Reflection;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpRestContext
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
          case NpdsConst.ServiceType.Nexus:
            DiristryTagReqst = serviceTag;
            break;
          case NpdsConst.ServiceType.PORTAL:
            RegistryTagReqst = serviceTag;
            break;
          case NpdsConst.ServiceType.DOORS:
            DirectoryTagReqst = serviceTag;
            break;
          case NpdsConst.ServiceType.Scribe:
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
          case NpdsConst.ServiceType.Nexus:
            serviceTag = DiristryTagDeflt;
            break;
          case NpdsConst.ServiceType.PORTAL:
            serviceTag = RegistryTagDeflt;
            break;
          case NpdsConst.ServiceType.DOORS:
            serviceTag = DirectoryTagDeflt;
            break;
          case NpdsConst.ServiceType.Scribe:
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
          DbConnectionString = NpdsServiceDefaults.Values.NpdsNlmmeshDbconstr;
          ServiceNote = PdpConst.NLMMESHNOTICE;
          break;
        case "NLMMICAD":
          DbConnectionString = NpdsServiceDefaults.Values.NpdsNlmmicadDbconstr;
          ServiceNote = PdpConst.NLMMICADNOTICE;
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
