using System;
using System.Reflection;

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // constraint is serviceTag = NpdsConstants.RegexTagToken

    // default values

    // requested values

    public string ServiceTagReqst
    {
      set
      {
        reqServiceTag = value;
        if (!string.IsNullOrEmpty(reqServiceTag)) { ServiceTag = reqServiceTag; }
      }
      get { return reqServiceTag; }
    }
    private string reqServiceTag = string.Empty;

    // validated values
    public string ServiceTag
    {
      set
      {
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
      get
      {
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
      string tag = reqTag;
      if (!string.IsNullOrEmpty(reqTag))
      {
        reqTag = reqTag.ToUpper();
        switch (reqTag)
        {
          // NPDS Registrar
          case "NPDS-ROOT":

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
            // TODO: set preferred service/database
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
          case "NLMMESH":
            DbConnectionString = NpdsServiceDefaults.GetValues.NpdsNlmmeshDbconstr;
            ServiceNote = PdpConst.NLMMESHNOTICE;
            break;
          case "NLMMICAD":
            DbConnectionString = NpdsServiceDefaults.GetValues.NpdsNlmmicadDbconstr;
            ServiceNote = PdpConst.NLMMICADNOTICE;
            break;
          default:
            break;
        }
        if (reqTag.Contains("SCRIBE") || reqTag.Contains("ROOT"))
        { RegistrarTag = tag; tag = RegistrarTag; }
        else if (reqTag.Contains("PORTAL"))
        { RegistryTag = tag; tag = RegistryTag; }
        else if (reqTag.Contains("DOORS"))
        { DirectoryTag = tag; tag = DirectoryTag; }
        else // if (reqTag.Contains("NEXUS")) // assume everything else is a Nexus
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

}
