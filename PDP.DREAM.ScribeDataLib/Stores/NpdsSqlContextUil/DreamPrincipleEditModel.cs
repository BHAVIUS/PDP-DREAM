// DreamPrincipleEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.ScribeDataLib.Models;

public class DreamPrincipleEditModel : NexusViewModelBase
{
  public DreamPrincipleEditModel() { }
  public Guid RegistrarGuid { get; } = NPDSSD.NpdsServiceCache.GetByTag("PDP-Scribe");
  public Guid DiristryGuid { get; } = NPDSSD.NpdsServiceCache.GetByTag("PDP-DREAM");
  public PdpAppConst.NpdsEntityType EntityType { get; } = PdpAppConst.NpdsEntityType.Principle;

  [DisplayName("Principle Word (for EntityTag)")]
  [StringLength(64, MinimumLength = 3, ErrorMessage = "String longer than 64 characters.")]
  [RegularExpression(PdpAppConst.RegexPrincipalTag, ErrorMessage = "String not a valid EntityTag.")]
  public string EntityTag { get; set; } = string.Empty;

  [DisplayName("Principle Phrase (for EntityName)")]
  [StringLength(256, MinimumLength = 0, ErrorMessage = "String longer than 128 characters.")]
  [RegularExpression(PdpAppConst.RegexSafeGeneric, ErrorMessage = "String not a valid EntityName.")]
  public override string? EntityName { get; set; } = string.Empty;

  [DisplayName("Principle Quote (for EntityNature)")]
  [StringLength(1024, MinimumLength = 0, ErrorMessage = "String longer than 256 characters.")]
  [RegularExpression(PdpAppConst.RegexSafeGeneric, ErrorMessage = "String not a valid EntityNature.")]
  public override string? EntityNature { get; set; } = string.Empty;

  [DisplayName("Group NPDS/HDMM/FAIR/LINKS/Other (for SupportingTag)")]
  [StringLength(256, MinimumLength = 0, ErrorMessage = "String longer than 256 characters.")]
  [RegularExpression(PdpAppConst.RegexSafeGeneric, ErrorMessage = "String not a valid SupportingTag.")]
  public string? SupportingTag { get; set; } = string.Empty;

  [DisplayName("Reference Citation as EntityLabel (for CrossReference)")]
  [StringLength(256, MinimumLength = 0, ErrorMessage = "String longer than 256 characters.")]
  [RegularExpression(PdpAppConst.RegexLocationUrl, ErrorMessage = "String not a valid CrossReference.")]
  public string? CrossReference { get; set; } = "https://npds.webdomain.net/nexus/serviceTag/entityTag";

  [DisplayName("Examples (for OtherText)")]
  public string? OtherText { get; set; } = "<anyTag> XML content </anyTag>";

  [DisplayName("Reference Location as E-Copy URL with Page QryString (for Location)")]
  [StringLength(256, MinimumLength = 0, ErrorMessage = "String longer than 256 characters.")]
  [RegularExpression(PdpAppConst.RegexLocationUrl, ErrorMessage = "String not a valid Location.")]
  public string? Location { get; set; } = "https://www.webdomain.org/edocs/pubid?page=1234";

  [DisplayName("Subject-Verb-Object (for Description)")]
  public string? Description { get; set; } = "<desc><sub>the subject</sub><vrb>the verb</vrb><obj>the object</obj></desc>";
}

