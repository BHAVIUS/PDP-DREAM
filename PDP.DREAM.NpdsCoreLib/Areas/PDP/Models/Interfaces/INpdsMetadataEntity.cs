// Interfaces for PDS data records; code uses acronyms PDSDR or Npdsdr in interface names;
// original design for PDS data records in 2008 IEEE TITB vol 12 no 2 pp 194-196;
// see Sections VII A and VII B of paper
//
// code also uses abbreviation ResRep for Resource Representation

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // TODO: refactor by making each of Core, PORTAL, DOORS independent
  // Core should be equivalent to Nexus min (min PORTAL + min DOORS)
  // PORTAL should be consistent with original design
  // DOORS should be consistent with original design
  // Nexus should remain as Nexus max (max PORTAL + max DOORS)

  public interface INpdsMetadataEntityCore
  {
    // required elements
    NpdsEntityCanonicalLabelItem EntityCanonicalLabel { get; set; }
    // permitted elements
    NpdsEntityNameItem EntityName { get; set; }
    NpdsEntityNatureItem EntityNature { get; set; }
  }

  public interface INpdsMetadataEntityPortal
  {
    // required elements
    NpdsEntityPrincipalTagItem EntityPrincipalTag { get; set; }
    NpdsEntityCanonicalLabelItem EntityCanonicalLabel { get; set; }

    // permitted elements
    NpdsEntityAliasLabelList EntityAliasLabelSet { get; set; }
    NpdsEntityNameItem EntityName { get; set; }
    NpdsEntityNatureItem EntityNature { get; set; }
    NpdsEntitySupportingTagList EntitySupportingTagSet { get; set; }
    NpdsEntitySupportingLabelList EntitySupportingLabelSet { get; set; }
    NpdsEntityOwnerItem EntityOwner { get; set; }
    // TODO: add NpdsEntityOwnerSignatureItem EntityOwnerSignature { get; set; }
    NpdsEntityContactItem EntityContact { get; set; }
    // TODO: add NpdsEntityContactSignatureItem EntityContactSignature { get; set; }
    NpdsEntityOtherEntityItem EntityOtherEntity { get; set; }

    // permitted attributes
  }

  public interface INpdsMetadataEntityDoors
  {
    // required elements
    NpdsEntityCanonicalLabelItem EntityCanonicalLabel { get; set; }
    NpdsEntityLocationList EntityLocationSet { get; set; }

    // permitted elements
    NpdsEntityDescriptionList EntityDescriptionSet { get; set; }
  }

  public interface INpdsMetadataEntityNexus : INpdsMetadataEntityPortal, INpdsMetadataEntityDoors
  {
  }

}
