﻿// INpdsMetadataEntity.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

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
