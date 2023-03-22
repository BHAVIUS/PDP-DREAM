// QurcServiceTag.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  // TODO: rebuild key-value dictionary with type string on key and type guid on value
  // TODO: resetting the ServiceTag must also reset the ServiceGuid !!!
  //   else service filter search priority must search tag first then guid second
  //   where else does tag reset guid and guid resets tag ???

  private string reqServiceTag = string.Empty;
  public string ServiceTagReqst
  {
    set {
      reqServiceTag = value;
      if (!string.IsNullOrEmpty(reqServiceTag)) { ServiceTag = reqServiceTag; }
    }
    get { return reqServiceTag; }
  }

  // validated values
  private string? serviceTag = string.Empty;
  public string? ServiceTag
  {
    set {
      serviceTag = ValidateServiceTag(value);
      if (!string.IsNullOrEmpty(serviceTag))
      {
        switch (SearchFilter)
        {
          case NpdsSearchFilter.Diristry:
            DiristryTag = serviceTag;
            break;
          case NpdsSearchFilter.Registry:
            RegistryTag = serviceTag;
            break;
          case NpdsSearchFilter.Directory:
            DirectoryTag = serviceTag;
            break;
          case NpdsSearchFilter.Registrar:
            RegistrarTag = serviceTag;
            break;
          default:
            throw new Exception("invalid SearchFilter in ServiceTag set");
        }
      }
    }
    get { return serviceTag; }
  }

  // validators

  private string ValidateServiceTag(string reqTag)
  {
    // constraint is serviceTag = NpdsConstants.RegexTagToken
    //
    // TODO: this method must be converted/migrated to generalized dynamic lookup
    //   in either service cache and if necessary the full database repository
    //    of any/all properties associated with the selected ServiceTag
    // TODO: recode to eliminate switch case dependencies for assignment of any properties 
    //     whether assigned registrar, dbconnectionstring, servicenote, or any other
    // TODO: must redefine what it means for a ServiceTag to be validated against
    //    constraints for the corresponding ServerType and ServiceType
    // TODO: rework where/when/how DbConnectionString gets reset/updated on each request
    //   referrals to database connections should be part of implementation of forwarding/caching
    string tag = reqTag;
    if (!string.IsNullOrEmpty(reqTag))
    {
      reqTag = reqTag.ToUpper();
      switch (reqTag)
      {
        // NPDS Registrar
        case "NPDS-ROOT":
          // TODO: check preferred services ???
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
          // TODO: check preferred services ???
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
          // TODO: check preferred services ???
          break;

        // TODO: move to QurcContentFromNpdsService ???
        case "NLMMESH":
          // DatabaseConstr = NPDSSD.VocabDbconstr;
          NexusDbconstr = NPDSSD.VocabDbconstr;
          StatusNote = NLMMESHNOTICE;
          break;
        case "NLMMICAD":
          // DatabaseConstr = NPDSSD.VocabDbconstr;
          NexusDbconstr = NPDSSD.VocabDbconstr;
          StatusNote = NLMMICADNOTICE;
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

} // end class

// end file