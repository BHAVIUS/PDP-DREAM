// SqldbcInitQueryResrepRoot.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // TODO: convert filter parameters to NpdsClient properties
  public IQueryable<INexusResrepRoot> InitRestQueryStorableNexusRoot(bool filterAuthoritativeOnly = false,
    bool filterPublicOnly = false, bool filterRecordAccess = true, bool filterNpdsServices = true)
  {
    // ATTN: method parameter boolean filter switches may conflict with each other
    //  if all turned on simultaneously for those filters that may be redundant with each other

    IQueryable<INexusResrepRoot> dalQueryStorableResrep;

    // initialize taggedLabel to empty
    string taggedLabel = string.Empty;
    // initialize base query without any where clause filters
    dalQueryStorableResrep =
      from INexusResrepRoot rr in NexusResrepRoots
      // orderby rr.EntityName, rr.RecordHandle
      orderby rr.RecordUpdatedOn descending
      select rr;

    // if any filters
    if (filterAuthoritativeOnly || filterPublicOnly || filterRecordAccess || filterNpdsServices)
    {
      // apply filter for authoritative records only (not a cached copy)
      if (filterAuthoritativeOnly)
      {
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.RecordIsCached == false)
          select rr;
      }

      // apply filter for public non-deleted records only
      if (filterPublicOnly)
      {
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
          select rr;
      }

      // apply filter for RecordAccess by role privileges
      if (filterRecordAccess)
      {
        if (NPDSCP.ClientHasAdminAccess)
        {
          // do nothing: no filter applied
        }
        else if (NPDSCP.ClientHasEditorAccess)
        {
          dalQueryStorableResrep =
            from INexusResrepRoot rr in dalQueryStorableResrep
            where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByEditor == true))
            select rr;
        }
        else if (NPDSCP.ClientHasAuthorAccess)
        {
          dalQueryStorableResrep =
            from INexusResrepRoot rr in dalQueryStorableResrep
            where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAuthor == true)
            && (rr.RecordManagedByAgentGuidRef == NPDSCP.ClientAgentGuid)) // Agent must be current Author with control of record via "ManagedBy"
            select rr;
        }
        else if (NPDSCP.ClientHasAgentAccess)
        {
          dalQueryStorableResrep =
            from INexusResrepRoot rr in dalQueryStorableResrep
            where (rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAgent == true)
            && ((rr.InfosetIsAuthorPrivate == false) || (rr.InfosetIsAgentShared == true))
            select rr;
        }
        else // only public non-deleted records for anonymous users
        {
          dalQueryStorableResrep =
            from INexusResrepRoot rr in dalQueryStorableResrep
            where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
            select rr;
        }
      }

      // apply filters for NPDS services (diristry, registry, directory, registrar)
      bool npdsAccess = false; Guid npdsGuid = Guid.Empty;
      if (filterNpdsServices)
      {
        switch (NPDSCP.SearchFilter)
        {
          case PdpAppConst.NpdsSearchFilter.Diristry:
            if (!string.IsNullOrEmpty(NPDSCP.DiristryTag))
            {
              var diristryTag = NPDSCP.DiristryTag.ToLower();
              dalQueryStorableResrep =
                from INexusResrepRoot rr in dalQueryStorableResrep
                where (rr.RecordDiristryTag.ToLower() == diristryTag)
                select rr;
            }
            else if (!NPDSCP.DiristryGuid.IsNullOrEmpty())
            {
              if (NPDSCP.ClientHasEditorAccess)
              {
                var diristryQry =
                  from INexusResrepRoot ss in NexusResrepRoots
                  where (ss.InfosetGuidKey == NPDSCP.DiristryGuid.Value)
                  select ss;
                var diristryItem = diristryQry.SingleOrDefault();
                var editorAccessQry =
                  from NexusServiceEditorAudit aa in NexusServiceEditorAudits
                  where (aa.RecordGuidRef == diristryItem.RecordGuidKey) &&
                  (aa.AccessRequestedForAgentGuidRef == NPDSCP.ClientAgentGuid) &&
                  (aa.EditorHasServiceAccess == true)
                  select aa;
                var editorAccessList = editorAccessQry.ToList();
                npdsAccess = (editorAccessList.Count > 0);
                if (npdsAccess) { npdsGuid = NPDSCP.DiristryGuid.Value; }
                dalQueryStorableResrep =
                  from INexusResrepRoot rr in dalQueryStorableResrep
                  where (rr.RecordDiristryGuidRef == npdsGuid)
                  select rr;
              }
              else
              {
                dalQueryStorableResrep =
                  from INexusResrepRoot rr in dalQueryStorableResrep
                  where (rr.RecordDiristryGuidRef == NPDSCP.DiristryGuid.Value)
                  select rr;
              }
            }
            break;

          case PdpAppConst.NpdsSearchFilter.Registry:
            if (!string.IsNullOrEmpty(NPDSCP.RegistryTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 let recordRegistryTag = rr.RecordRegistryTag
                 where (EF.Functions.Like(recordRegistryTag, NPDSCP.RegistryTag))
                 select rr;
            }
            else if (!NPDSCP.RegistryGuid.IsNullOrEmpty())
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 where (rr.RecordRegistryGuidRef == NPDSCP.RegistryGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.Directory:
            if (!string.IsNullOrEmpty(NPDSCP.DirectoryTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 let recordDirectoryTag = rr.RecordDirectoryTag
                 where (EF.Functions.Like(recordDirectoryTag, NPDSCP.DirectoryTag))
                 select rr;
            }
            else if (!NPDSCP.DirectoryGuid.IsNullOrEmpty())
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 where (rr.RecordDirectoryGuidRef == NPDSCP.DirectoryGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.Registrar:
            if (!string.IsNullOrEmpty(NPDSCP.RegistrarTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 let recordRegistrarTag = rr.RecordRegistrarTag
                 where EF.Functions.Like(recordRegistrarTag, NPDSCP.RegistrarTag)
                 select rr;
            }
            else if (!NPDSCP.RegistrarGuid.IsNullOrEmpty())
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 where (rr.RecordRegistrarGuidRef == NPDSCP.RegistrarGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.AllTags:
            if (!string.IsNullOrEmpty(NPDSCP.DiristryTag))
            {
              dalQueryStorableResrep =
                from INexusResrepRoot rr in dalQueryStorableResrep
                let recordDiristryTag = rr.RecordDiristryTag
                where (EF.Functions.Like(recordDiristryTag, NPDSCP.DiristryTag))
                select rr;
            }
            else
            {
              if (!string.IsNullOrEmpty(NPDSCP.RegistryTag))
              {
                dalQueryStorableResrep =
                   from INexusResrepRoot rr in dalQueryStorableResrep
                   let recordRegistryTag = rr.RecordRegistryTag
                   where (EF.Functions.Like(recordRegistryTag, NPDSCP.RegistryTag))
                   select rr;
              }
              if (!string.IsNullOrEmpty(NPDSCP.DirectoryTag))
              {
                dalQueryStorableResrep =
                   from INexusResrepRoot rr in dalQueryStorableResrep
                   let recordDirectoryTag = rr.RecordDirectoryTag
                   where (EF.Functions.Like(recordDirectoryTag, NPDSCP.DirectoryTag))
                   select rr;
              }
            }
            if (!string.IsNullOrEmpty(NPDSCP.RegistrarTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 let recordRegistrarTag = rr.RecordRegistrarTag
                 where EF.Functions.Like(recordRegistrarTag, NPDSCP.RegistrarTag)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.AllGuids:
            if (NPDSCP.DiristryGuid.HasValue && (NPDSCP.DiristryGuid.Value != Guid.Empty))
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 where (rr.RecordDiristryGuidRef == NPDSCP.DiristryGuid.Value)
                 select rr;
            }
            else
            {
              if (NPDSCP.RegistryGuid.HasValue && (NPDSCP.RegistryGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrep =
                   from INexusResrepRoot rr in dalQueryStorableResrep
                   where (rr.RecordRegistryGuidRef == NPDSCP.RegistryGuid.Value)
                   select rr;
              }
              if (NPDSCP.DirectoryGuid.HasValue && (NPDSCP.DirectoryGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrep =
                   from INexusResrepRoot rr in dalQueryStorableResrep
                   where (rr.RecordDirectoryGuidRef == NPDSCP.DirectoryGuid.Value)
                   select rr;
              }
            }
            if (NPDSCP.RegistrarGuid.HasValue && (NPDSCP.RegistrarGuid.Value != Guid.Empty))
            {
              dalQueryStorableResrep =
                 from INexusResrepRoot rr in dalQueryStorableResrep
                 where (rr.RecordRegistrarGuidRef == NPDSCP.RegistrarGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.None:
            // do nothing
            break;

          default:
            throw new Exception($"case not implemented for SearchFilter value {NPDSCP.SearchFilter.ToString()} in method {MethodBase.GetCurrentMethod().Name}");

        } // end switch on PRC.SearchFilter

      } // end filterNpdsServices with PRC.SearchFilter

    }

    // apply filter for EntityType
    if (NPDSCP.EntityType != PdpAppConst.NpdsEntityType.AnyAndAll)
    {
      var cod = (short)NPDSCP.EntityType;
      dalQueryStorableResrep =
        from INexusResrepRoot rr in dalQueryStorableResrep
        where (rr.EntityTypeCodeRef == cod)
        select rr;
    }

    // apply filter for EntityTag
    // match to NpdsResrepRecord.EntityPrincipalTag from CanonicalLabel OrElse TagTokens from AliasLabels
    if (!string.IsNullOrEmpty(NPDSCP.EntityTag))
    {
      if (NPDSCP.QueryFormat)
      {
        string entityTag = NPDSCP.EntityTag; // case-sensitive on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.NexusEntityLabels.Any((NexusEntityLabel el) => el.TagToken == entityTag))
          select rr;
      }
      else
      {
        string entityTag = NPDSCP.EntityTag.ToLower(); // case-insensitive on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.NexusEntityLabels.Any((NexusEntityLabel el) => el.TagToken.ToLower() == entityTag))
          select rr;
      }
    }

    // apply filter for taggedLabel
    // TODO: must also code a PrcEntityLabel (partial vs full?) in addition to PrcEntityTag
    //   and then rebuild a consolidated PRC system for AI on the properties
    // TODO: eliminate redundancies on QSK parameters
    if (!string.IsNullOrEmpty(taggedLabel))
    {
      dalQueryStorableResrep =
        from INexusResrepRoot rr in dalQueryStorableResrep
        where (rr.EntityCanonicalLabel.Contains(taggedLabel) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.Contains(taggedLabel)))
        select rr;
    }

    // apply filter for InfosetStatus
    if (NPDSCP.InfosetStatus != PdpAppConst.NpdsInfosetStatus.AnyAndAll)
    {
      var cod = (short)NPDSCP.InfosetStatus;
      switch (NPDSCP.ServiceType)
      {
        case PdpAppConst.NpdsServiceType.Nexus:
        case PdpAppConst.NpdsServiceType.Scribe:
          dalQueryStorableResrep =
            from INexusResrepRoot rr in dalQueryStorableResrep
            where ((rr.InfosetPortalStatusCode == cod) || (rr.InfosetDoorsStatusCode == cod))
            select rr;
          break;
        case PdpAppConst.NpdsServiceType.PORTAL:
          dalQueryStorableResrep =
            from INexusResrepRoot rr in dalQueryStorableResrep
            where (rr.InfosetPortalStatusCode == cod)
            select rr;
          break;
        case PdpAppConst.NpdsServiceType.DOORS:
          dalQueryStorableResrep =
            from INexusResrepRoot rr in dalQueryStorableResrep
            where (rr.InfosetDoorsStatusCode == cod)
            select rr;
          break;
        default:
          break;
      }
    }

    // QueryString Key search parameters
    //
    // Entity
    if (!string.IsNullOrEmpty(NPDSCP.EntityName))
    {
      if (NPDSCP.QueryFormat)
      {
        string nam = NPDSCP.EntityName; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityName == nam)
          select rr;
      }
      else
      {
        string nam = NPDSCP.EntityName.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityName.ToLower().Contains(nam))
          select rr;
      }
    }
    //
    // EntityNature
    if (!string.IsNullOrEmpty(NPDSCP.EntityNature))
    {
      if (NPDSCP.QueryFormat)
      {
        string nat = NPDSCP.EntityNature; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityNature == nat)
          select rr;
      }
      else
      {
        string nat = NPDSCP.EntityNature.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityNature.ToLower().Contains(nat))
          select rr;
      }
    }
    //
    // Any Labels (Canonical or Alias)
    if (!string.IsNullOrEmpty(NPDSCP.QskLexLabAny))
    {
      if (NPDSCP.QueryFormat)
      {
        string eLabel = NPDSCP.QskLexLabAny; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel == eLabel || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel == eLabel))
          select rr;
      }
      else
      {
        string eLabel = NPDSCP.QskLexLabAny.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel)))
          select rr;
      }
    }
    //
    // EntityCanonicalLabel
    if (!string.IsNullOrEmpty(NPDSCP.QskLexLabCan))
    {
      if (NPDSCP.QueryFormat)
      {
        string eLabel = NPDSCP.QskLexLabCan; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel == eLabel)
          select rr;
      }
      else
      {
        string eLabel = NPDSCP.QskLexLabCan.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel))
          select rr;
      }
    }
    //
    // EntityAliasLabel
    if (!string.IsNullOrEmpty(NPDSCP.QskLexLabAls))
    {
      if (NPDSCP.QueryFormat)
      {
        string eLabel = NPDSCP.QskLexLabAls; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel == eLabel)
          select rr;
      }
      else
      {
        string eLabel = NPDSCP.QskLexLabAls.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel))
          select rr;
      }
    }
    //
    // EntitySupportingLabel
    if (!string.IsNullOrEmpty(NPDSCP.QskLexLabSup))
    {
      if (NPDSCP.QueryFormat)
      {
        string sLabel = NPDSCP.QskLexLabSup; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel == sLabel)
          select rr;
      }
      else
      {
        string sLabel = NPDSCP.QskLexLabSup.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel.ToLower().Contains(sLabel))
          select rr;
      }
    }
    //
    // Entity Any Tag (Canonical or Alias TagToken)
    if (!string.IsNullOrEmpty(NPDSCP.QskLexTagAny))
    {
      if (NPDSCP.QueryFormat)
      {
        string entityTag = NPDSCP.QskLexTagAny; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where rr.NexusEntityCanonicalLabels.Any((NexusEntityCanonicalLabel cl) => (cl.EntityPrincipalTag == entityTag))
          || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => (al.EntityAliasTag == entityTag))
          select rr;
      }
      else
      {
        string entityTag = NPDSCP.QskLexTagAny.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where rr.NexusEntityCanonicalLabels.Any((NexusEntityCanonicalLabel cl) => cl.EntityPrincipalTag.ToLower().Contains(entityTag))
          || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasTag.ToLower().Contains(entityTag))
          select rr;
      }
    }
    //
    // EntitySupportingTag
    if (!string.IsNullOrEmpty(NPDSCP.QskLexTagSup))
    {
      if (NPDSCP.QueryFormat)
      {
        string sTag = NPDSCP.QskLexTagSup; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag == sTag))
          select rr;
      }
      else
      {
        string sTag = NPDSCP.QskLexTagSup.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepRoot rr in dalQueryStorableResrep
          where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag.ToLower().Contains(sTag)))
          select rr;
      }
    }
    //
    // EntityOtherText
    if (!string.IsNullOrEmpty(NPDSCP.QskLexOText))
    {
      string otext = NPDSCP.QskLexOText.ToLower(); // case-insens on partial Search
      dalQueryStorableResrep =
        from INexusResrepRoot rr in dalQueryStorableResrep
        where (rr.NexusOtherTexts.Any((NexusOtherText ot) => ot.OtherText.ToLower().Contains(otext)))
        select rr;
    }

    // return query initialized with or without filters
    return dalQueryStorableResrep;

  } // end expression method

} // end partial class

// end file