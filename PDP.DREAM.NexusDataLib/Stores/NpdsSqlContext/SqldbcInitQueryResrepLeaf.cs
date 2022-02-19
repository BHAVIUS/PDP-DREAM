// SqldbcInitQueryResrepLeaf.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores
{
  public partial class NexusDbsqlContext
  {
    public IQueryable<NexusResrepLeaf> DalQueryStorableResrepLeaf
    { get { return dalQueryStorableResrepLeaf; } set { dalQueryStorableResrepLeaf = value; } }
    private IQueryable<NexusResrepLeaf> dalQueryStorableResrepLeaf;

    // TODO: consider converting these filter arguments to PRC properties
    public IQueryable<NexusResrepLeaf> InitQueryStorableResrepLeaf(bool filterAuthoritativeOnly = false,
      bool filterPublicOnly = false, bool filterRecordAccess = true, bool filterNpdsServices = true)
    {
      // ATTN: method parameter boolean filter switches may conflict with each other
      //  if all turned on simultaneously for those filters that may be redundant with each other
      // TODO: eliminate the redundancies that occur in InitializeQuery and InitializePredicate

      // initialize taggedLabel to empty
      string taggedLabel = string.Empty;
      // initialize base query without any where clause filters
      dalQueryStorableResrepLeaf =
        from NexusResrepLeaf rr in NexusResrepLeafs
        orderby rr.EntityName, rr.RecordHandle
        select rr;

      // if any filters
      if (filterAuthoritativeOnly || filterPublicOnly || filterRecordAccess || filterNpdsServices)
      {
        // apply filter for authoritative records only (not a cached copy)
        if (filterAuthoritativeOnly)
        {
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.RecordIsCached == false)
            select rr;
        }

        // apply filter for public non-deleted records only
        if (filterPublicOnly)
        {
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
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
            // TODO: implement T-SQL Select statement (or equivalent variant)
            //      SELECT * FROM dbo.pds_NResourceAuthOnlyView AS R WHERE (R.EntityTypeEditedByEditor = 1)
            //		  AND (R.RecordRegistryGuidRef IN (SELECT E.RegistryInfosetGuidRef FROM pds_NAgentRegistry AS E WHERE E.AgentIidRef = @PdsAgentIid AND E.EditorAccessIsApproved = 1))
            //		  ORDER BY R.EntityName;
            dalQueryStorableResrepLeaf =
              from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
              where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByEditor == true))
              select rr;
          }
          else if (PRC.ClientHasAuthorAccess)
          {
            dalQueryStorableResrepLeaf =
              from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
              where ((rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAuthor == true)
              && (rr.RecordManagedByAgentGuidRef == PRC.AgentGuid)) // Agent must be current Author with control of record via "ManagedBy"
              select rr;
          }
          else if (PRC.ClientHasAgentAccess)
          {
            dalQueryStorableResrepLeaf =
              from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
              where (rr.RecordIsDeleted == false) && (rr.EntityTypeEditedByAuthor == true) // TODO: update for Agent instead of Author
              && ((rr.InfosetIsAuthorPrivate == false) || (rr.InfosetIsAgentShared == true))
              select rr;
          }
          else // only public non-deleted records for anonymous users
          {
            dalQueryStorableResrepLeaf =
              from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
              where (rr.InfosetIsAuthorPrivate == false) && (rr.RecordIsDeleted == false)
              select rr;
          }
        }

        // apply filters for NPDS services (diristry, registry, directory, registrar)
        if (filterNpdsServices)
        {
          switch (PRC.SearchFilter)
          {
            case NpdsConst.SearchFilter.Diristry:
              if (!string.IsNullOrEmpty(PRC.DiristryTag))
              {
                dalQueryStorableResrepLeaf =
                  from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                  where (EF.Functions.Like(rr.RecordDiristryTag, PRC.DiristryTag))
                  select rr;
              }
              break;

            case NpdsConst.SearchFilter.Registry:
              if (!string.IsNullOrEmpty(PRC.RegistryTag))
              {
                dalQueryStorableResrepLeaf =
                   from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                   where (EF.Functions.Like(rr.RecordRegistryTag, PRC.RegistryTag))
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.Directory:
              if (!string.IsNullOrEmpty(PRC.DirectoryTag))
              {
                dalQueryStorableResrepLeaf =
                   from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                   where (EF.Functions.Like(rr.RecordDirectoryTag, PRC.DirectoryTag))
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.Registrar:
              if (!string.IsNullOrEmpty(PRC.RegistrarTag))
              {
                dalQueryStorableResrepLeaf =
                   from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                   where EF.Functions.Like(rr.RecordRegistrarTag, PRC.RegistrarTag)
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.AllTags:
              if (!string.IsNullOrEmpty(PRC.DiristryTag))
              {
                dalQueryStorableResrepLeaf =
                  from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                  where (EF.Functions.Like(rr.RecordDiristryTag, PRC.DiristryTag))
                  select rr;
              }
              else
              {
                if (!string.IsNullOrEmpty(PRC.RegistryTag))
                {
                  dalQueryStorableResrepLeaf =
                     from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                     where (EF.Functions.Like(rr.RecordRegistryTag, PRC.RegistryTag))
                     select rr;
                }
                if (!string.IsNullOrEmpty(PRC.DirectoryTag))
                {
                  dalQueryStorableResrepLeaf =
                     from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                     where (EF.Functions.Like(rr.RecordDirectoryTag, PRC.DirectoryTag))
                     select rr;
                }
              }
              if (!string.IsNullOrEmpty(PRC.RegistrarTag))
              {
                dalQueryStorableResrepLeaf =
                   from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                   where EF.Functions.Like(rr.RecordRegistrarTag, PRC.RegistrarTag)
                   select rr;
              }
              break;

            case NpdsConst.SearchFilter.AllGuids:
              if (PRC.DiristryGuid.HasValue)
              {
                dalQueryStorableResrepLeaf =
                   from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                   where ((rr.RecordRegistryGuidRef == PRC.DiristryGuid.Value) && (rr.RecordDirectoryGuidRef == PRC.DiristryGuid.Value))
                   select rr;
              }
              else
              {
                if (PRC.RegistryGuid.HasValue)
                {
                  dalQueryStorableResrepLeaf =
                     from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                     where (rr.RecordRegistryGuidRef == PRC.RegistryGuid.Value)
                     select rr;
                }
                if (PRC.DirectoryGuid.HasValue)
                {
                  dalQueryStorableResrepLeaf =
                     from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
                     where (rr.RecordDirectoryGuidRef == PRC.DirectoryGuid.Value)
                     select rr;
                }
              }
              if (PRC.RegistrarGuid.HasValue)
              {
                dalQueryStorableResrepLeaf =
                   from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
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
        dalQueryStorableResrepLeaf =
          from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
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
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where ((rr.EntityPrincipalTag == entityTag) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasTag == entityTag))
            select rr;
        }
        else
        {
          string entityTag = PRC.EntityTag.ToLower(); // case-insensitive on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where ((rr.EntityPrincipalTag.ToLower() == entityTag) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasTag.ToLower() == entityTag))
            select rr;
        }
      }

      // apply filter for taggedLabel
      //
      // TODO: must also code a PrcEntityLabel (partial vs full?) in addition to PrcEntityTag
      //   and then rebuild a consolidated PRC system for AI on the properties
      // TODO: eliminate redundancies on QSK parameters
      if (!string.IsNullOrEmpty(taggedLabel))
      {
        dalQueryStorableResrepLeaf =
          from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
          where (rr.EntityCanonicalLabel.Contains(taggedLabel) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.Contains(taggedLabel)))
          select rr;
      }

      // apply filter for InfosetStatus
      if (PRC.InfosetStatus != NpdsConst.InfosetStatus.AnyAndAll)
      {
        Int16 cod = (Int16)PRC.InfosetStatus;
        switch (PRC.ServiceType)
        {
          case NpdsConst.ServiceType.Nexus:
          case NpdsConst.ServiceType.Scribe:
            dalQueryStorableResrepLeaf =
              from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
              where ((rr.InfosetPortalStatusCode == cod) || (rr.InfosetDoorsStatusCode == cod))
              select rr;
            break;
          case NpdsConst.ServiceType.PORTAL:
            dalQueryStorableResrepLeaf =
              from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
              where (rr.InfosetPortalStatusCode == cod)
              select rr;
            break;
          case NpdsConst.ServiceType.DOORS:
            dalQueryStorableResrepLeaf =
              from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
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
      if (!string.IsNullOrEmpty(PRC.EntityNameReqst))
      {
        if (PRC.QueryFormat)
        {
          string nam = PRC.EntityNameReqst; // case-sens on exact Find
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityName == nam)
            select rr;
        }
        else
        {
          string nam = PRC.EntityNameReqst.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
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
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityNature == nat)
            select rr;
        }
        else
        {
          string nat = PRC.EntityNatureReqst.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityNature.ToLower().Contains(nat))
            select rr;
        }
      }
      //
      // Any Labels (Canonical or Alias)
      if (!string.IsNullOrEmpty(PRC.QskLexLabAny))
      {
        if (PRC.QueryFormat)
        {
          string eLabel = PRC.QskLexLabAny; // case-sens on exact Find
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityCanonicalLabel == eLabel || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel == eLabel))
            select rr;
        }
        else
        {
          string eLabel = PRC.QskLexLabAny.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel.ToLower().Contains(eLabel)))
            select rr;
        }
      }
      //
      // EntityCanonicalLabel
      if (!string.IsNullOrEmpty(PRC.QskLexLabCan))
      {
        if (PRC.QueryFormat)
        {
          string eLabel = PRC.QskLexLabCan; // case-sens on exact Find
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityCanonicalLabel == eLabel)
            select rr;
        }
        else
        {
          string eLabel = PRC.QskLexLabCan.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityCanonicalLabel.ToLower().Contains(eLabel))
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
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasLabel == eLabel)
            select rr;
        }
        else
        {
          string eLabel = PRC.QskLexLabAls.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
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
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel == sLabel)
            select rr;
        }
        else
        {
          string sLabel = PRC.QskLexLabSup.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where rr.NexusSupportingLabels.Any((NexusSupportingLabel sl) => sl.SupportingLabel.ToLower().Contains(sLabel))
            select rr;
        }
      }
      //
      // Entity Any Tag (Principal or Alias)
      if (!string.IsNullOrEmpty(PRC.QskLexTagAny))
      {
        if (PRC.QueryFormat)
        {
          string entityTag = PRC.QskLexTagAny; // case-sens on exact Find
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityPrincipalTag == entityTag) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasTag == entityTag)
            select rr;
        }
        else
        {
          string entityTag = PRC.QskLexTagAny.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.EntityPrincipalTag.ToLower().Contains(entityTag)) || rr.NexusEntityAliasLabels.Any((NexusEntityAliasLabel al) => al.EntityAliasTag.ToLower().Contains(entityTag))
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
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag == sTag))
            select rr;
        }
        else
        {
          string sTag = PRC.QskLexTagSup.ToLower(); // case-insens on partial Search
          dalQueryStorableResrepLeaf =
            from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
            where (rr.NexusSupportingTags.Any((NexusSupportingTag st) => st.SupportingTag.ToLower().Contains(sTag)))
            select rr;
        }
      }
      //
      // EntityOtherText
      if (!string.IsNullOrEmpty(PRC.QskLexOText))
      {
        string otext = PRC.QskLexOText.ToLower(); // case-insens on partial Search
        dalQueryStorableResrepLeaf =
          from NexusResrepLeaf rr in dalQueryStorableResrepLeaf
          where (rr.NexusOtherTexts.Any((NexusOtherText ot) => ot.OtherText.ToLower().Contains(otext)))
          select rr;
      }

      // return query initialized with or without filters
      return dalQueryStorableResrepLeaf;
    } // expression method

  } // end partial class

} // end namespace
