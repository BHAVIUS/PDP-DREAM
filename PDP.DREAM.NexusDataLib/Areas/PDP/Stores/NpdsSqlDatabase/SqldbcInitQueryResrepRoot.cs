using System;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public partial class NexusDbsqlContext
  {
    public IQueryable<NexusResrepRoot> DalQueryStorableResrepRoot
    { get { return dalQueryStorableResrepRoot; } set { dalQueryStorableResrepRoot = value; } }
    private IQueryable<NexusResrepRoot> dalQueryStorableResrepRoot;

    // TODO: consider converting these filter arguments to PRC properties
    public IQueryable<NexusResrepRoot> InitQueryStorableResrepRoot(bool filterAuthoritativeOnly = false,
      bool filterPublicOnly = false, bool filterRecordAccess = true, bool filterNpdsServices = true)
    {
      // ATTN: method parameter boolean filter switches may conflict with each other
      //  if all turned on simultaneously for those filters that may be redundant with each other
      // TODO: eliminate the redundancies that occur in InitializeQuery and InitializePredicate

      // initialize taggedLabel to empty
      string taggedLabel = string.Empty;
      // initialize base query without any where clause filters
      dalQueryStorableResrepRoot =
        from NexusResrepRoot rr in NexusResrepRoots
        orderby rr.EntityName, rr.RecordHandle
        select rr;

      // if any filters
      if (filterAuthoritativeOnly || filterPublicOnly || filterRecordAccess || filterNpdsServices)
      {
        // apply filter for authoritative records only (not a cached copy)
        if (filterAuthoritativeOnly)
        {
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.RecordIsCached == false)
            select rr;
        }

        // apply filter for public non-deleted records only
        if (filterPublicOnly)
        {
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
            select rr;
        }

        // apply filter for RecordAccess by role privileges
        if (filterRecordAccess)
        {
          if (PRC.ClientHasAdminAccess)
          {
            // do nothing: no filter applied
          }
          else if (PRC.ClientHasEditorAccess)
          {
            dalQueryStorableResrepRoot =
              from NexusResrepRoot rr in dalQueryStorableResrepRoot
              where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByEditor == true))
              select rr;
          }
          else if (PRC.ClientHasAuthorAccess)
          {
            dalQueryStorableResrepRoot =
              from NexusResrepRoot rr in dalQueryStorableResrepRoot
              where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAuthor == true)
              && (rr.RecordManagedByAgentGuidRef == PRC.AgentGuid)) // Agent must be current Author with control of record via "ManagedBy"
              select rr;
          }
          else if (PRC.ClientHasAgentAccess)
          {
            dalQueryStorableResrepRoot =
              from NexusResrepRoot rr in dalQueryStorableResrepRoot
              where (rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAuthor == true) // TODO: update for Agent instead of Author
              && ((rr.InfosetIsAuthorPrivate == false) || (rr.InfosetIsAgentShared == true))
              select rr;
          }
          else // only public non-deleted records for anonymous users
          {
            dalQueryStorableResrepRoot =
              from NexusResrepRoot rr in dalQueryStorableResrepRoot
              where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
              select rr;
          }
        }

        // apply filters for NPDS services (diristry, registry, directory, registrar)
        var npdsAccess = false; var npdsGuid = Guid.Empty;
        if (filterNpdsServices)
        {
          switch (PRC.SearchFilter)
          {
            case NpdsConst.SearchFilter.Diristry:
              if (PRC.DiristryGuid.HasValue && (PRC.DiristryGuid.Value != Guid.Empty))
              {
                if (PRC.ClientHasEditorAccess)
                {
                  var diristryQry = from NexusResrepRoot ss in NexusResrepRoots
                                    where (ss.InfosetGuidKey == PRC.DiristryGuid.Value)
                                    select ss;
                  var diristryItem = diristryQry.SingleOrDefault();
                  var editorAccessQry = from NexusServiceEditorAudit aa in NexusServiceEditorAudits
                                        where (aa.RecordGuidRef == diristryItem.RecordGuidKey) &&
                                        (aa.AccessRequestedForAgentGuidRef == PRC.AgentGuid) &&
                                        (aa.EditorHasServiceAccess == true)
                                        select aa;
                  var editorAccessList = editorAccessQry.ToList();
                  npdsAccess = (editorAccessList.Count > 0);
                  if (npdsAccess) { npdsGuid = PRC.DiristryGuid.Value; }
                  dalQueryStorableResrepRoot =
                    from NexusResrepRoot rr in dalQueryStorableResrepRoot
                  where (rr.RecordDiristryGuidRef == npdsGuid)
                  select rr;
                }
                else
                {
                  dalQueryStorableResrepRoot =
                    from NexusResrepRoot rr in dalQueryStorableResrepRoot
                    where (rr.RecordDiristryGuidRef == PRC.DiristryGuid.Value)
                    select rr;
                }
              }
              else if (!string.IsNullOrEmpty(PRC.DiristryTag))
              {
                dalQueryStorableResrepRoot =
                  from NexusResrepRoot rr in dalQueryStorableResrepRoot
                  let recordDiristryTag = rr.RecordDiristryTag
                  where (EF.Functions.Like(recordDiristryTag, PRC.DiristryTag))
                  select rr;
              }
              break;

            case NpdsConst.SearchFilter.Registry:
              if (PRC.RegistryGuid.HasValue && (PRC.RegistryGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   where (rr.RecordRegistryGuidRef == PRC.RegistryGuid.Value)
                   select rr;
              }
              else if (!string.IsNullOrEmpty(PRC.RegistryTag))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   let recordRegistryTag = rr.RecordRegistryTag
                   where (EF.Functions.Like(recordRegistryTag, PRC.RegistryTag))
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.Directory:
              if (PRC.DirectoryGuid.HasValue && (PRC.DirectoryGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   where (rr.RecordDirectoryGuidRef == PRC.DirectoryGuid.Value)
                   select rr;
              }
              else if (!string.IsNullOrEmpty(PRC.DirectoryTag))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   let recordDirectoryTag = rr.RecordDirectoryTag
                   where (EF.Functions.Like(recordDirectoryTag, PRC.DirectoryTag))
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.Registrar:
              if (PRC.RegistrarGuid.HasValue && (PRC.RegistrarGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   where (rr.RecordRegistrarGuidRef == PRC.RegistrarGuid.Value)
                   select rr;
              }
              else if (!string.IsNullOrEmpty(PRC.RegistrarTag))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   let recordRegistrarTag = rr.RecordRegistrarTag
                   where EF.Functions.Like(recordRegistrarTag, PRC.RegistrarTag)
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.AllTags:
              if (!string.IsNullOrEmpty(PRC.DiristryTag))
              {
                dalQueryStorableResrepRoot =
                  from NexusResrepRoot rr in dalQueryStorableResrepRoot
                  let recordDiristryTag = rr.RecordDiristryTag
                  where (EF.Functions.Like(recordDiristryTag, PRC.DiristryTag))
                  select rr;
              }
              else
              {
                if (!string.IsNullOrEmpty(PRC.RegistryTag))
                {
                  dalQueryStorableResrepRoot =
                     from NexusResrepRoot rr in dalQueryStorableResrepRoot
                     let recordRegistryTag = rr.RecordRegistryTag
                     where (EF.Functions.Like(recordRegistryTag, PRC.RegistryTag))
                     select rr;
                }
                if (!string.IsNullOrEmpty(PRC.DirectoryTag))
                {
                  dalQueryStorableResrepRoot =
                     from NexusResrepRoot rr in dalQueryStorableResrepRoot
                     let recordDirectoryTag = rr.RecordDirectoryTag
                     where (EF.Functions.Like(recordDirectoryTag, PRC.DirectoryTag))
                     select rr;
                }
              }
              if (!string.IsNullOrEmpty(PRC.RegistrarTag))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   let recordRegistrarTag = rr.RecordRegistrarTag
                   where EF.Functions.Like(recordRegistrarTag, PRC.RegistrarTag)
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.AllGuids:
              if (PRC.DiristryGuid.HasValue && (PRC.DiristryGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   where (rr.RecordDiristryGuidRef == PRC.DiristryGuid.Value)
                   select rr;
              }
              else
              {
                if (PRC.RegistryGuid.HasValue && (PRC.RegistryGuid.Value != Guid.Empty))
                {
                  dalQueryStorableResrepRoot =
                     from NexusResrepRoot rr in dalQueryStorableResrepRoot
                     where (rr.RecordRegistryGuidRef == PRC.RegistryGuid.Value)
                     select rr;
                }
                if (PRC.DirectoryGuid.HasValue && (PRC.DirectoryGuid.Value != Guid.Empty))
                {
                  dalQueryStorableResrepRoot =
                     from NexusResrepRoot rr in dalQueryStorableResrepRoot
                     where (rr.RecordDirectoryGuidRef == PRC.DirectoryGuid.Value)
                     select rr;
                }
              }
              if (PRC.RegistrarGuid.HasValue && (PRC.RegistrarGuid.Value != Guid.Empty))
              {
                dalQueryStorableResrepRoot =
                   from NexusResrepRoot rr in dalQueryStorableResrepRoot
                   where (rr.RecordRegistrarGuidRef == PRC.RegistrarGuid.Value)
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.None:
              // do nothing
              break;

            default:
              throw new Exception($"case not implemented for SearchFilter value {PRC.SearchFilter.ToString()} in method {MethodBase.GetCurrentMethod().Name}");

          } // end switch on PRC.SearchFilter

        } // end filterNpdsServices with PRC.SearchFilter

      }

      // apply filter for EntityType
      if (PRC.EntityType != NpdsConst.EntityType.AnyAndAll)
      {
        Int16 cod = (Int16)PRC.EntityType;
        dalQueryStorableResrepRoot =
          from NexusResrepRoot rr in dalQueryStorableResrepRoot
          where (rr.EntityTypeCodeRef == cod)
          select rr;
      }

      // apply filter for EntityTag
      // match to NpdsResrepRecord.EntityPrincipalTag from CanonicalLabel OrElse TagTokens from AliasLabels
      if (!string.IsNullOrEmpty(PRC.EntityTag))
      {
        if (PRC.QueryFormat)
        {
          string entityTag = PRC.EntityTag; // case-sensitive on exact Find
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.NexusEntityLabels.Any((NexusEntityLabel el) => el.TagToken == entityTag))
            select rr;
        }
        else
        {
          string entityTag = PRC.EntityTag.ToLower(); // case-insensitive on partial Search
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.NexusEntityLabels.Any((NexusEntityLabel el) => el.TagToken == entityTag))
            select rr;
        }
      }


      // QueryString Key search parameters
      //
      // Entity
      if (!string.IsNullOrEmpty(PRC.EntityNameReqst))
      {
        if (PRC.QueryFormat)
        {
          string nam = PRC.EntityNameReqst; // case-sens on exact Find
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.EntityName == nam)
            select rr;
        }
        else
        {
          string nam = PRC.EntityNameReqst.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.EntityName.ToLower().Contains(nam))
            select rr;
        }
      }
      //
      // EntityNature
      if (!string.IsNullOrEmpty(PRC.EntityNatureReqst))
      {
        if (PRC.QueryFormat)
        {
          string nat = PRC.EntityNatureReqst; // case-sens on exact Find
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.EntityNature == nat)
            select rr;
        }
        else
        {
          string nat = PRC.EntityNatureReqst.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.EntityNature.ToLower().Contains(nat))
            select rr;
        }
      }

      //
      // EntityAliasLabel
      if (!string.IsNullOrEmpty(PRC.QskLexLabAls))
      {
        if (PRC.QueryFormat)
        {
          string eLabel = PRC.QskLexLabAls; // case-sens on exact Find
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel == eLabel)
            select rr;
        }
        else
        {
          string eLabel = PRC.QskLexLabAls.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel))
            select rr;
        }
      }
      //
      // EntitySupportingLabel
      if (!string.IsNullOrEmpty(PRC.QskLexLabSup))
      {
        if (PRC.QueryFormat)
        {
          string sLabel = PRC.QskLexLabSup; // case-sens on exact Find
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel == sLabel)
            select rr;
        }
        else
        {
          string sLabel = PRC.QskLexLabSup.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel.ToLower().Contains(sLabel))
            select rr;
        }
      }

      //
      // EntitySupportingTag
      if (!string.IsNullOrEmpty(PRC.QskLexTagSup))
      {
        if (PRC.QueryFormat)
        {
          string sTag = PRC.QskLexTagSup; // case-sens on exact Find
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag == sTag))
            select rr;
        }
        else
        {
          string sTag = PRC.QskLexTagSup.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepRoot =
            from NexusResrepRoot rr in dalQueryStorableResrepRoot
            where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag.ToLower().Contains(sTag)))
            select rr;
        }
      }
      //
      // EntityOtherText
      if (!string.IsNullOrEmpty(PRC.QskLexOText))
      {
        string otext = PRC.QskLexOText.ToLower(); // case-insens on partial Search
        dalQueryStorableResrepRoot =
          from NexusResrepRoot rr in dalQueryStorableResrepRoot
          where (rr.NexusOtherTexts.Any((NexusOtherText ot) => ot.OtherText.ToLower().Contains(otext)))
          select rr;
      }

      // return query initialized with or without filters
      return dalQueryStorableResrepRoot;
    } // expression method

  } // end partial class

} // end namespace
