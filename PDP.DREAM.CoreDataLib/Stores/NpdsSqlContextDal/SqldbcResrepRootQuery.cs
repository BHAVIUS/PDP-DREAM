// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // ATTN: method boolean filter switches may conflict with each other
  //  if all turned on simultaneously for those filters redundant with each other
  // TODO: build full test suite for all filter switches

  public IQueryable<ICoreResrepRoot> QueryStorableResrepRoot()
  {
    // initialize base query without any where clause filters
    IQueryable<ICoreResrepRoot> dalQueryStorableResrep =
      from ICoreResrepRoot rr in CoreResrepRoots
        // orderby rr.EntityName, rr.RecordHandle
      orderby rr.RecordUpdatedOn descending
      select rr;

    // apply filter for authoritative records only (not a cached copy)
    if (NPDSDC.LnqFltrAuthoritativeOnly)
    {
      dalQueryStorableResrep =
        from ICoreResrepRoot rr in dalQueryStorableResrep
        where (rr.RecordIsCached == false)
        select rr;
    }

    // apply filter for public non-deleted records only
    if (NPDSDC.LnqFltrPublicOnly)
    {
      dalQueryStorableResrep =
        from ICoreResrepRoot rr in dalQueryStorableResrep
        where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
        select rr;
    }

    // apply filters for NPDS services (diristry, registry, directory, registrar)
    bool npdsAccess = false; Guid npdsGuid = EGS;
    if (NPDSDC.LnqFltrNpdsService)
    {
      switch (NPDSDC.SearchFilter.EName)
      {
        case DdeSearchFilter.Diristry:
          if (!string.IsNullOrEmpty(NPDSDC.DiristryTag))
          {
            var diristryTag = NPDSDC.DiristryTag.ToLower();
            dalQueryStorableResrep =
              from ICoreResrepRoot rr in dalQueryStorableResrep
              where (rr.RecordDiristryTag.ToLower() == diristryTag)
              select rr;
          }
          else if (!NPDSDC.DiristryGuid.IsNullOrEmpty())
          {
            if (NPDSDC.ClientHasEditorMode)
            {
              var diristryQry =
                from ICoreResrepRoot ss in CoreResrepRoots
                where (ss.InfosetGuid == NPDSDC.DiristryGuid.Value)
                select ss;
              var diristryItem = diristryQry.SingleOrDefault();
              var editorAccessQry =
                from CoreAccessConnect aa in CoreAccessConnects
                where (aa.RecordGuid == diristryItem.RecordGuid) &&
                (aa.AgentGuid == NPDSDC.NpdsAgentGuid) &&
                (aa.EditorHasServiceAccess == true)
                select aa;
              var editorAccessList = editorAccessQry.ToList();
              npdsAccess = (editorAccessList.Count > 0);
              if (npdsAccess) { npdsGuid = NPDSDC.DiristryGuid.Value; }
              dalQueryStorableResrep =
                from ICoreResrepRoot rr in dalQueryStorableResrep
                where (rr.RecordDiristryGuid == npdsGuid)
                select rr;
            }
            else
            {
              dalQueryStorableResrep =
                from ICoreResrepRoot rr in dalQueryStorableResrep
                where (rr.RecordDiristryGuid == NPDSDC.DiristryGuid.Value)
                select rr;
            }
          }
          break;

        case DdeSearchFilter.Registry:
          if (!string.IsNullOrEmpty(NPDSDC.RegistryTag))
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               let recordRegistryTag = rr.RecordRegistryTag
               where (EF.Functions.Like(recordRegistryTag, NPDSDC.RegistryTag))
               select rr;
          }
          else if (!NPDSDC.RegistryGuid.IsNullOrEmpty())
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               where (rr.RecordRegistryGuid == NPDSDC.RegistryGuid.Value)
               select rr;
          }
          break;

        case DdeSearchFilter.Directory:
          if (!string.IsNullOrEmpty(NPDSDC.DirectoryTag))
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               let recordDirectoryTag = rr.RecordDirectoryTag
               where (EF.Functions.Like(recordDirectoryTag, NPDSDC.DirectoryTag))
               select rr;
          }
          else if (!NPDSDC.DirectoryGuid.IsNullOrEmpty())
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               where (rr.RecordDirectoryGuid == NPDSDC.DirectoryGuid.Value)
               select rr;
          }
          break;

        case DdeSearchFilter.Registrar:
          if (!string.IsNullOrEmpty(NPDSDC.RegistrarTag))
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               let recordRegistrarTag = rr.RecordRegistrarTag
               where EF.Functions.Like(recordRegistrarTag, NPDSDC.RegistrarTag)
               select rr;
          }
          else if (!NPDSDC.RegistrarGuid.IsNullOrEmpty())
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               where (rr.RecordRegistrarGuid == NPDSDC.RegistrarGuid.Value)
               select rr;
          }
          break;

        case DdeSearchFilter.AllTags:
          if (!string.IsNullOrEmpty(NPDSDC.DiristryTag))
          {
            dalQueryStorableResrep =
              from ICoreResrepRoot rr in dalQueryStorableResrep
              let recordDiristryTag = rr.RecordDiristryTag
              where (EF.Functions.Like(recordDiristryTag, NPDSDC.DiristryTag))
              select rr;
          }
          else
          {
            if (!string.IsNullOrEmpty(NPDSDC.RegistryTag))
            {
              dalQueryStorableResrep =
                 from ICoreResrepRoot rr in dalQueryStorableResrep
                 let recordRegistryTag = rr.RecordRegistryTag
                 where (EF.Functions.Like(recordRegistryTag, NPDSDC.RegistryTag))
                 select rr;
            }
            if (!string.IsNullOrEmpty(NPDSDC.DirectoryTag))
            {
              dalQueryStorableResrep =
                 from ICoreResrepRoot rr in dalQueryStorableResrep
                 let recordDirectoryTag = rr.RecordDirectoryTag
                 where (EF.Functions.Like(recordDirectoryTag, NPDSDC.DirectoryTag))
                 select rr;
            }
          }
          if (!string.IsNullOrEmpty(NPDSDC.RegistrarTag))
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               let recordRegistrarTag = rr.RecordRegistrarTag
               where EF.Functions.Like(recordRegistrarTag, NPDSDC.RegistrarTag)
               select rr;
          }
          break;

        case DdeSearchFilter.AllGuids:
          if (NPDSDC.DiristryGuid.HasValue && (NPDSDC.DiristryGuid.Value != EGS))
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               where (rr.RecordDiristryGuid == NPDSDC.DiristryGuid.Value)
               select rr;
          }
          else
          {
            if (NPDSDC.RegistryGuid.HasValue && (NPDSDC.RegistryGuid.Value != EGS))
            {
              dalQueryStorableResrep =
                 from ICoreResrepRoot rr in dalQueryStorableResrep
                 where (rr.RecordRegistryGuid == NPDSDC.RegistryGuid.Value)
                 select rr;
            }
            if (NPDSDC.DirectoryGuid.HasValue && (NPDSDC.DirectoryGuid.Value != EGS))
            {
              dalQueryStorableResrep =
                 from ICoreResrepRoot rr in dalQueryStorableResrep
                 where (rr.RecordDirectoryGuid == NPDSDC.DirectoryGuid.Value)
                 select rr;
            }
          }
          if (NPDSDC.RegistrarGuid.HasValue && (NPDSDC.RegistrarGuid.Value != EGS))
          {
            dalQueryStorableResrep =
               from ICoreResrepRoot rr in dalQueryStorableResrep
               where (rr.RecordRegistrarGuid == NPDSDC.RegistrarGuid.Value)
               select rr;
          }
          break;

        case DdeSearchFilter.None:
          // do nothing
          break;

        default:
          throw new Exception($"case not implemented for SearchFilter = {NPDSDC.SearchFilter}");

      } // end switch on PRC.SearchFilter

    } // end LnqFltrNpdsService with PRC.SearchFilter

    // apply filter for RecordAccess by role privileges
    if (NPDSDC.LnqFltrRecordAccess)
    {
      IQueryable<Guid>? resrepAccessQuery = null;
      IList<Guid>? resrepAccessList = null;

      if (NPDSDC.ClientHasAdminMode)
      {
        // do nothing: no filter applied
      }
      else if (NPDSDC.ClientHasEditorMode)
      {
        resrepAccessQuery =
          from CoreAccessConnect aa in CoreAccessConnects
          where (aa.AgentGuid == NPDSDC.NpdsAgentGuid) && (aa.EditorHasResrepAccess == true)
          select aa.RecordGuid;
        resrepAccessList = resrepAccessQuery.ToList();
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where ((rr.RecordDeletedOn == null) && (rr.EntityTypeEditedByEditor == true) &&
          // Agent must be Editor with access to record via "EditedBy" or "HasResrepAccess"
          ((rr.RecordEditedByAgentGuid == NPDSDC.NpdsAgentGuid) || resrepAccessList.Contains(rr.RecordGuid)))
          select rr;
      }
      else if (NPDSDC.ClientHasReviewerMode)
      {
        resrepAccessQuery =
          from CoreAccessConnect aa in CoreAccessConnects
          where (aa.AgentGuid == NPDSDC.NpdsAgentGuid) && (aa.ReviewerHasResrepAccess == true)
          select aa.RecordGuid;
        resrepAccessList = resrepAccessQuery.ToList();
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where ((rr.RecordDeletedOn == null) && (rr.EntityTypeEditedByReviewer == true) &&
          // Agent must be Reviewer with access to record via "ReviewedBy" or "HasResrepAccess"
          ((rr.RecordReviewedByAgentGuid == NPDSDC.NpdsAgentGuid) || resrepAccessList.Contains(rr.RecordGuid)))
          select rr;
      }
      else if (NPDSDC.ClientHasAuthorMode)
      {
        resrepAccessQuery =
          from CoreAccessConnect aa in CoreAccessConnects
          where (aa.AgentGuid == NPDSDC.NpdsAgentGuid) && (aa.AuthorHasResrepAccess == true)
          select aa.RecordGuid;
        resrepAccessList = resrepAccessQuery.ToList();
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where ((rr.RecordDeletedOn == null) && (rr.EntityTypeEditedByAuthor == true) &&
          // Agent must be Author with access to record via "AuthoredBy" or "HasResrepAccess"
          ((rr.RecordAuthoredByAgentGuid == NPDSDC.NpdsAgentGuid) || resrepAccessList.Contains(rr.RecordGuid)))
          select rr;
      }
      else if (NPDSDC.ClientHasAgentMode)
      {
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where (rr.RecordDeletedOn == null) && (rr.EntityTypeEditedByAgent == true)
          && ((rr.InfosetIsAuthorPrivate == false) || (rr.InfosetIsAgentShared == true))
          select rr;
      }
      else // only public non-deleted records for anonymous users
      {
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordDeletedOn == null)
          select rr;
      }
    }


    // apply filter for EntityType
    if (NPDSDC.EntityType != NPDSCD.EntityTypeAnyAndAll)
    {
      var cod = NPDSDC.EntityType.ECode;
      dalQueryStorableResrep =
        from ICoreResrepRoot rr in dalQueryStorableResrep
        where (rr.EntityTypeCode == cod)
        select rr;
    }

    // apply filter for EntityTag
    // match to NpdsResrepRecord.EntityPrincipalTag from CanonicalLabel OrElse TagTokens from AliasLabels
    if (!string.IsNullOrEmpty(NPDSDC.EntityTag))
    {
      if (NPDSDC.QueryFormat)
      {
        string entityTag = NPDSDC.EntityTag; // case-sensitive on exact Find
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where (rr.CoreEntityLabels.Any((CoreEntityLabel el) => el.TagToken == entityTag))
          select rr;
      }
      else
      {
        string entityTag = NPDSDC.EntityTag.ToLower(); // case-insensitive on partial Search
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where (rr.CoreEntityLabels.Any((CoreEntityLabel el) => el.TagToken.ToLower() == entityTag))
          select rr;
      }
    }

    // apply filter for InfosetStatus
    if (NPDSDC.InfosetStatus != NPDSCD.InfosetStatusAnyAndAll)
    {
      var cod = NPDSDC.InfosetStatus.ECode;
      switch (NPDSDC.ServiceType.EName)
      {
        case DdeServiceType.None:
        case DdeServiceType.Scribe:
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where ((rr.InfosetPortalStatusCode == cod) || (rr.InfosetDoorsStatusCode == cod))
            select rr;
          break;
        case DdeServiceType.PORTAL:
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.InfosetPortalStatusCode == cod)
            select rr;
          break;
        case DdeServiceType.DOORS:
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.InfosetDoorsStatusCode == cod)
            select rr;
          break;
        default:
          break;
      }
    }

    // QueryString Key-Value search parameters
    if (NPDSDC.LnqFltrQryStrValues)
    {

      // EntityName
      if (!string.IsNullOrEmpty(NPDSDC.EntityName))
      {
        if (NPDSDC.QueryFormat)
        {
          string nam = NPDSDC.EntityName; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityName == nam)
            select rr;
        }
        else
        {
          string nam = NPDSDC.EntityName.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityName.ToLower().Contains(nam))
            select rr;
        }
      }

      // EntityNature
      if (!string.IsNullOrEmpty(NPDSDC.EntityNature))
      {
        if (NPDSDC.QueryFormat)
        {
          string nat = NPDSDC.EntityNature; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityNature == nat)
            select rr;
        }
        else
        {
          string nat = NPDSDC.EntityNature.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityNature.ToLower().Contains(nat))
            select rr;
        }
      }

      //
      // Any Labels (Canonical or Alias)
      if (!string.IsNullOrEmpty(NPDSDC.LnqLexLabAny))
      {
        if (NPDSDC.QueryFormat)
        {
          string eLabel = NPDSDC.LnqLexLabAny; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityCanonicalLabel == eLabel || rr.CoreEntityAliasLabels.Any((CoreEntityAliasLabel al) => al.EntityAliasLabel == eLabel))
            select rr;
        }
        else
        {
          string eLabel = NPDSDC.LnqLexLabAny.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel) || rr.CoreEntityAliasLabels.Any((CoreEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel)))
            select rr;
        }
      }
      //
      // EntityCanonicalLabel
      if (!string.IsNullOrEmpty(NPDSDC.LnqLexLabCan))
      {
        if (NPDSDC.QueryFormat)
        {
          string eLabel = NPDSDC.LnqLexLabCan; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityCanonicalLabel == eLabel)
            select rr;
        }
        else
        {
          string eLabel = NPDSDC.LnqLexLabCan.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel))
            select rr;
        }
      }
      //
      // EntityAliasLabel
      if (!string.IsNullOrEmpty(NPDSDC.LnqLexLabAls))
      {
        if (NPDSDC.QueryFormat)
        {
          string eLabel = NPDSDC.LnqLexLabAls; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where rr.CoreEntityAliasLabels.Any((CoreEntityAliasLabel al) => al.EntityAliasLabel == eLabel)
            select rr;
        }
        else
        {
          string eLabel = NPDSDC.LnqLexLabAls.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where rr.CoreEntityAliasLabels.Any((CoreEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel))
            select rr;
        }
      }
      //
      // EntitySupportingLabel
      if (!string.IsNullOrEmpty(NPDSDC.LnqLexLabSup))
      {
        if (NPDSDC.QueryFormat)
        {
          string sLabel = NPDSDC.LnqLexLabSup; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where rr.PortalSupportingLabels.Any((PortalSupportingLabel sl) => sl.SupportingLabel == sLabel)
            select rr;
        }
        else
        {
          string sLabel = NPDSDC.LnqLexLabSup.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where rr.PortalSupportingLabels.Any((PortalSupportingLabel sl) => sl.SupportingLabel.ToLower().Contains(sLabel))
            select rr;
        }
      }
      //
      // Entity Any Tag (Canonical or Alias TagToken)
      if (!string.IsNullOrEmpty(NPDSDC.LnqLexTagAny))
      {
        if (NPDSDC.QueryFormat)
        {
          string entityTag = NPDSDC.LnqLexTagAny; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where rr.CoreEntityCanonicalLabels.Any((CoreEntityCanonicalLabel cl) => (cl.EntityPrincipalTag == entityTag))
            || rr.CoreEntityAliasLabels.Any((CoreEntityAliasLabel al) => (al.EntityAliasTag == entityTag))
            select rr;
        }
        else
        {
          string entityTag = NPDSDC.LnqLexTagAny.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where rr.CoreEntityCanonicalLabels.Any((CoreEntityCanonicalLabel cl) => cl.EntityPrincipalTag.ToLower().Contains(entityTag))
            || rr.CoreEntityAliasLabels.Any((CoreEntityAliasLabel al) => al.EntityAliasTag.ToLower().Contains(entityTag))
            select rr;
        }
      }
      //
      // EntitySupportingTag
      if (!string.IsNullOrEmpty(NPDSDC.LnqLexTagSup))
      {
        if (NPDSDC.QueryFormat)
        {
          string sTag = NPDSDC.LnqLexTagSup; // case-sens on exact Find
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.PortalSupportingTags.Any((PortalSupportingTag st) => st.SupportingTag == sTag))
            select rr;
        }
        else
        {
          string sTag = NPDSDC.LnqLexTagSup.ToLower(); // case-insens on partial Search
          dalQueryStorableResrep =
            from ICoreResrepRoot rr in dalQueryStorableResrep
            where (rr.PortalSupportingTags.Any((PortalSupportingTag st) => st.SupportingTag.ToLower().Contains(sTag)))
            select rr;
        }
      }
      //
      // EntityOtherText
      if (!string.IsNullOrEmpty(NPDSDC.LnqLexOText))
      {
        string otext = NPDSDC.LnqLexOText.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from ICoreResrepRoot rr in dalQueryStorableResrep
          where (rr.PortalOtherTexts.Any((PortalOtherText ot) => ot.OtherText.ToLower().Contains(otext)))
          select rr;
      }

    } // end if (LnqFltrQryStrValues)

    // return query initialized with or without filters
    return dalQueryStorableResrep;

  } // end expression method

} // end partial class

// end file