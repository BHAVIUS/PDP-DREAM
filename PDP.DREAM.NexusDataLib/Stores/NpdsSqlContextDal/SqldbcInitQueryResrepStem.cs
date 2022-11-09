// SqldbcInitQueryResrepStem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  public IQueryable<INexusResrepStem> InitQueryStorableNexusStem(bool filterAuthoritativeOnly = false,
    bool filterPublicOnly = false, bool filterRecordAccess = true, bool filterNpdsServices = true)
  {
    // ATTN: method parameter boolean filter switches may conflict with each other
    //  if all turned on simultaneously for those filters that may be redundant with each other

    IQueryable<INexusResrepStem> dalQueryStorableResrep;

    // initialize taggedLabel to empty
    string taggedLabel = string.Empty;
    // initialize base query without any where clause filters
    dalQueryStorableResrep =
      from INexusResrepStem rr in NexusResrepStems
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
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.RecordIsCached == false)
          select rr;
      }

      // apply filter for public non-deleted records only
      if (filterPublicOnly)
      {
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
          select rr;
      }

      // apply filter for RecordAccess by role privileges
      if (filterRecordAccess)
      {
        if (QURC.ClientHasAdminAccess)
        {
          // do nothing: no filter applied
        }
        else if (QURC.ClientHasEditorAccess)
        {
          dalQueryStorableResrep =
            from INexusResrepStem rr in dalQueryStorableResrep
            where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByEditor == true))
            select rr;
        }
        else if (QURC.ClientHasAuthorAccess)
        {
          dalQueryStorableResrep =
            from INexusResrepStem rr in dalQueryStorableResrep
            where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAuthor == true)
            && (rr.RecordManagedByAgentGuidRef == QURC.QebAgentGuid)) // Agent must be current Author with control of record via "ManagedBy"
            select rr;
        }
        else if (QURC.ClientHasAgentAccess)
        {
          dalQueryStorableResrep =
            from INexusResrepStem rr in dalQueryStorableResrep
            where (rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAgent == true)
            && ((rr.InfosetIsAuthorPrivate == false) || (rr.InfosetIsAgentShared == true))
            select rr;
        }
        else // only public non-deleted records for anonymous users
        {
          dalQueryStorableResrep =
            from INexusResrepStem rr in dalQueryStorableResrep
            where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
            select rr;
        }
      }

      // apply filters for NPDS services (diristry, registry, directory, registrar)
      var npdsAccess = false; var npdsGuid = Guid.Empty;
      if (filterNpdsServices)
      {
        switch (QURC.SearchFilter)
        {
          case PdpAppConst.NpdsSearchFilter.Diristry:
            if (!string.IsNullOrEmpty(QURC.DiristryTag))
            {
              dalQueryStorableResrep =
                from INexusResrepStem rr in dalQueryStorableResrep
                let recordDiristryTag = rr.RecordDiristryTag
                where (EF.Functions.Like(recordDiristryTag, QURC.DiristryTag))
                select rr;
            }
            else if (QURC.DiristryGuid.HasValue && (QURC.DiristryGuid.Value != Guid.Empty))
            {
              if (QURC.ClientHasEditorAccess)
              {
                var diristryQry =
                  from INexusResrepStem ss in NexusResrepStems
                  where (ss.InfosetGuidKey == QURC.DiristryGuid.Value)
                  select ss;
                var diristryItem = diristryQry.SingleOrDefault();
                var editorAccessQry =
                  from NexusServiceEditorAudit aa in NexusServiceEditorAudits
                  where (aa.RecordGuidRef == diristryItem.RecordGuidKey) &&
                  (aa.AccessRequestedForAgentGuidRef == QURC.QebAgentGuid) &&
                  (aa.EditorHasServiceAccess == true)
                  select aa;
                var editorAccessList = editorAccessQry.ToList();
                npdsAccess = (editorAccessList.Count > 0);
                if (npdsAccess) { npdsGuid = QURC.DiristryGuid.Value; }
                dalQueryStorableResrep =
                  from INexusResrepStem rr in dalQueryStorableResrep
                  where (rr.RecordDiristryGuidRef == npdsGuid)
                  select rr;
              }
              else
              {
                dalQueryStorableResrep =
                  from INexusResrepStem rr in dalQueryStorableResrep
                  where (rr.RecordDiristryGuidRef == QURC.DiristryGuid.Value)
                  select rr;
              }
            }
            break;

          case PdpAppConst.NpdsSearchFilter.Registry:
            if (!string.IsNullOrEmpty(QURC.RegistryTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 let recordRegistryTag = rr.RecordRegistryTag
                 where (EF.Functions.Like(recordRegistryTag, QURC.RegistryTag))
                 select rr;
            }
            else if (QURC.RegistryGuid.HasValue && (QURC.RegistryGuid.Value != Guid.Empty))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 where (rr.RecordRegistryGuidRef == QURC.RegistryGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.Directory:
            if (!string.IsNullOrEmpty(QURC.DirectoryTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 let recordDirectoryTag = rr.RecordDirectoryTag
                 where (EF.Functions.Like(recordDirectoryTag, QURC.DirectoryTag))
                 select rr;
            }
            else if (QURC.DirectoryGuid.HasValue && (QURC.DirectoryGuid.Value != Guid.Empty))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 where (rr.RecordDirectoryGuidRef == QURC.DirectoryGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.Registrar:
            if (!string.IsNullOrEmpty(QURC.RegistrarTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 let recordRegistrarTag = rr.RecordRegistrarTag
                 where EF.Functions.Like(recordRegistrarTag, QURC.RegistrarTag)
                 select rr;
            }
            else if (QURC.RegistrarGuid.HasValue && (QURC.RegistrarGuid.Value != Guid.Empty))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 where (rr.RecordRegistrarGuidRef == QURC.RegistrarGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.AllTags:
            if (!string.IsNullOrEmpty(QURC.DiristryTag))
            {
              dalQueryStorableResrep =
                from INexusResrepStem rr in dalQueryStorableResrep
                let recordDiristryTag = rr.RecordDiristryTag
                where (EF.Functions.Like(recordDiristryTag, QURC.DiristryTag))
                select rr;
            }
            else
            {
              if (!string.IsNullOrEmpty(QURC.RegistryTag))
              {
                dalQueryStorableResrep =
                   from INexusResrepStem rr in dalQueryStorableResrep
                   let recordRegistryTag = rr.RecordRegistryTag
                   where (EF.Functions.Like(recordRegistryTag, QURC.RegistryTag))
                   select rr;
              }
              if (!string.IsNullOrEmpty(QURC.DirectoryTag))
              {
                dalQueryStorableResrep =
                   from INexusResrepStem rr in dalQueryStorableResrep
                   let recordDirectoryTag = rr.RecordDirectoryTag
                   where (EF.Functions.Like(recordDirectoryTag, QURC.DirectoryTag))
                   select rr;
              }
            }
            if (!string.IsNullOrEmpty(QURC.RegistrarTag))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 let recordRegistrarTag = rr.RecordRegistrarTag
                 where EF.Functions.Like(recordRegistrarTag, QURC.RegistrarTag)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.AllGuids:
            if (QURC.DiristryGuid.HasValue && (QURC.DiristryGuid.Value != Guid.Empty))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 where (rr.RecordDiristryGuidRef == QURC.DiristryGuid.Value)
                 select rr;
            }
            else
            {
              if (QURC.RegistryGuid.HasValue && (QURC.RegistryGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrep =
                   from INexusResrepStem rr in dalQueryStorableResrep
                   where (rr.RecordRegistryGuidRef == QURC.RegistryGuid.Value)
                   select rr;
              }
              if (QURC.DirectoryGuid.HasValue && (QURC.DirectoryGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrep =
                   from INexusResrepStem rr in dalQueryStorableResrep
                   where (rr.RecordDirectoryGuidRef == QURC.DirectoryGuid.Value)
                   select rr;
              }
            }
            if (QURC.RegistrarGuid.HasValue && (QURC.RegistrarGuid.Value != Guid.Empty))
            {
              dalQueryStorableResrep =
                 from INexusResrepStem rr in dalQueryStorableResrep
                 where (rr.RecordRegistrarGuidRef == QURC.RegistrarGuid.Value)
                 select rr;
            }
            break;

          case PdpAppConst.NpdsSearchFilter.None:
            // do nothing
            break;

          default:
            throw new Exception($"case not implemented for SearchFilter value {QURC.SearchFilter.ToString()} in method {MethodBase.GetCurrentMethod().Name}");

        } // end switch on PRC.SearchFilter

      } // end filterNpdsServices with PRC.SearchFilter

    }

    // apply filter for EntityType
    if (QURC.EntityType != PdpAppConst.NpdsEntityType.AnyAndAll)
    {
      var cod = (short)QURC.EntityType;
      dalQueryStorableResrep =
        from INexusResrepStem rr in dalQueryStorableResrep
        where (rr.EntityTypeCodeRef == cod)
        select rr;
    }

    // apply filter for EntityTag
    // match to NpdsResrepRecord.EntityPrincipalTag from CanonicalLabel OrElse TagTokens from AliasLabels
    if (!string.IsNullOrEmpty(QURC.EntityTag))
    {
      if (QURC.QueryFormat)
      {
        string entityTag = QURC.EntityTag; // case-sensitive on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.NexusEntityLabels.Any((NexusEntityLabel el) => el.TagToken == entityTag))
          select rr;
      }
      else
      {
        string entityTag = QURC.EntityTag.ToLower(); // case-insensitive on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.NexusEntityLabels.Any((NexusEntityLabel el) => el.TagToken == entityTag))
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
        from INexusResrepStem rr in dalQueryStorableResrep
        where (rr.EntityCanonicalLabel.Contains(taggedLabel) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.Contains(taggedLabel)))
        select rr;
    }

    // apply filter for InfosetStatus
    if (QURC.InfosetStatus != PdpAppConst.NpdsInfosetStatus.AnyAndAll)
    {
      var cod = (short)QURC.InfosetStatus;
      switch (QURC.ServiceType)
      {
        case PdpAppConst.NpdsServiceType.Nexus:
        case PdpAppConst.NpdsServiceType.Scribe:
          dalQueryStorableResrep =
            from INexusResrepStem rr in dalQueryStorableResrep
            where ((rr.InfosetPortalStatusCode == cod) || (rr.InfosetDoorsStatusCode == cod))
            select rr;
          break;
        case PdpAppConst.NpdsServiceType.PORTAL:
          dalQueryStorableResrep =
            from INexusResrepStem rr in dalQueryStorableResrep
            where (rr.InfosetPortalStatusCode == cod)
            select rr;
          break;
        case PdpAppConst.NpdsServiceType.DOORS:
          dalQueryStorableResrep =
            from INexusResrepStem rr in dalQueryStorableResrep
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
    if (!string.IsNullOrEmpty(QURC.EntityNameReqst))
    {
      if (QURC.QueryFormat)
      {
        string nam = QURC.EntityNameReqst; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityName == nam)
          select rr;
      }
      else
      {
        string nam = QURC.EntityNameReqst.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityName.ToLower().Contains(nam))
          select rr;
      }
    }
    //
    // EntityNature
    if (!string.IsNullOrEmpty(QURC.EntityNatureReqst))
    {
      if (QURC.QueryFormat)
      {
        string nat = QURC.EntityNatureReqst; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityNature == nat)
          select rr;
      }
      else
      {
        string nat = QURC.EntityNatureReqst.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityNature.ToLower().Contains(nat))
          select rr;
      }
    }
    //
    // Any Labels (Canonical or Alias)
    if (!string.IsNullOrEmpty(QURC.QskLexLabAny))
    {
      if (QURC.QueryFormat)
      {
        string eLabel = QURC.QskLexLabAny; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel == eLabel || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel == eLabel))
          select rr;
      }
      else
      {
        string eLabel = QURC.QskLexLabAny.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel)))
          select rr;
      }
    }
    //
    // EntityCanonicalLabel
    if (!string.IsNullOrEmpty(QURC.QskLexLabCan))
    {
      if (QURC.QueryFormat)
      {
        string eLabel = QURC.QskLexLabCan; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel == eLabel)
          select rr;
      }
      else
      {
        string eLabel = QURC.QskLexLabCan.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel))
          select rr;
      }
    }
    //
    // EntityAliasLabel
    if (!string.IsNullOrEmpty(QURC.QskLexLabAls))
    {
      if (QURC.QueryFormat)
      {
        string eLabel = QURC.QskLexLabAls; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel == eLabel)
          select rr;
      }
      else
      {
        string eLabel = QURC.QskLexLabAls.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel))
          select rr;
      }
    }
    //
    // EntitySupportingLabel
    if (!string.IsNullOrEmpty(QURC.QskLexLabSup))
    {
      if (QURC.QueryFormat)
      {
        string sLabel = QURC.QskLexLabSup; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel == sLabel)
          select rr;
      }
      else
      {
        string sLabel = QURC.QskLexLabSup.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel.ToLower().Contains(sLabel))
          select rr;
      }
    }
    //
    // Entity Any Tag (Canonical or Alias TagToken)
    if (!string.IsNullOrEmpty(QURC.QskLexTagAny))
    {
      if (QURC.QueryFormat)
      {
        string entityTag = QURC.QskLexTagAny; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where rr.NexusEntityCanonicalLabels.Any((NexusEntityCanonicalLabel cl) => (cl.EntityPrincipalTag == entityTag))
          || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => (al.EntityAliasTag == entityTag))
          select rr;
      }
      else
      {
        string entityTag = QURC.QskLexTagAny.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where rr.NexusEntityCanonicalLabels.Any((NexusEntityCanonicalLabel cl) => cl.EntityPrincipalTag.ToLower().Contains(entityTag))
          || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasTag.ToLower().Contains(entityTag))
          select rr;
      }
    }
    //
    // EntitySupportingTag
    if (!string.IsNullOrEmpty(QURC.QskLexTagSup))
    {
      if (QURC.QueryFormat)
      {
        string sTag = QURC.QskLexTagSup; // case-sens on exact Find
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag == sTag))
          select rr;
      }
      else
      {
        string sTag = QURC.QskLexTagSup.ToLower(); // case-insens on partial Search
        dalQueryStorableResrep =
          from INexusResrepStem rr in dalQueryStorableResrep
          where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag.ToLower().Contains(sTag)))
          select rr;
      }
    }
    //
    // EntityOtherText
    if (!string.IsNullOrEmpty(QURC.QskLexOText))
    {
      string otext = QURC.QskLexOText.ToLower(); // case-insens on partial Search
      dalQueryStorableResrep =
        from INexusResrepStem rr in dalQueryStorableResrep
        where (rr.NexusOtherTexts.Any((NexusOtherText ot) => ot.OtherText.ToLower().Contains(otext)))
        select rr;
    }

    // return query initialized with or without filters
    return dalQueryStorableResrep;

  } // end expression method

} // end partial class

// end file