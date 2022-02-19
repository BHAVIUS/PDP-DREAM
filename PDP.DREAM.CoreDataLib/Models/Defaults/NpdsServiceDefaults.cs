// NpdsServiceDefaults.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models;

public class NpdsServiceDefaults
{
  public static NpdsServiceDefaults Values { get; } = new NpdsServiceDefaults();

  // private constructor prevents instantiation outside the class
  private NpdsServiceDefaults()
  {
    // parse default settings from web.config file
    defSearchScope = PdpEnum<NpdsConst.SearchScope>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefSearchScope));
    defSearchFilter = PdpEnum<NpdsConst.SearchFilter>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefSearchFilter));
    defNodeType = PdpEnum<NpdsConst.NodeType>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefNodeType));
    sitDefServerType = PdpEnum<NpdsConst.ServerType>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefServerType));
    defServiceType = PdpEnum<NpdsConst.ServiceType>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefServiceType));
    defDatabaseType = PdpEnum<NpdsConst.DatabaseType>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefDatabaseType));
    defDatabaseAccess = PdpEnum<NpdsConst.DatabaseAccess>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefDatabaseAccess));
    defRecordAccess = PdpEnum<NpdsConst.RecordAccess>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefRecordAccess));
    var repFmt = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefResrepFormat);
    defResrepFormat = PdpEnum<NpdsConst.ResrepFormat>.Parse(repFmt);
    var msgFmt = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefMessageFormat);
    defMessageFormat = PdpEnum<NpdsConst.MessageFormat>.Parse(msgFmt);
    defEntityType = PdpEnum<NpdsConst.EntityType>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefEntityType));
    defNewEntityType = PdpEnum<NpdsConst.EntityType>.Parse(PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefNewEntityType));

    // intended for use as service initial defaults for new records
    defDiristryTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefDiristryTag);
    defRegistryTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefRegistryTag);
    defDirectoryTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefDirectoryTag);
    defRegistrarTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceDefaults.NpdsDefRegistrarTag);

    // intended for use as service restriction constraints on scope of the authority of an authoritative server
    conRegistrarTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceConstraints.NpdsConRegistrarTag);
    conRegistryTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceConstraints.NpdsConRegistryTag);
    conDirectoryTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceConstraints.NpdsConDirectoryTag);
    conDiristryTag = PdpConfigManager.ParseAppStringSetting(PdpConst.NamesForServiceConstraints.NpdsConDiristryTag);

    // required database connection strings
    userDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForRequiredDbConnStrings.NpdsUserDbserver);
    agentDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForRequiredDbConnStrings.NpdsAgentDbserver);
    coreDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForRequiredDbConnStrings.NpdsCoreDbserver);
    diristryDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForRequiredDbConnStrings.NpdsNexusDiristry);
    registryDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForRequiredDbConnStrings.NpdsPortalRegistry);
    directoryDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForRequiredDbConnStrings.NpdsDoorsDirectory);
    registrarDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForRequiredDbConnStrings.NpdsScribeRegistrar);

    // permitted datatbased connection strings
    portalAuth1Dbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForPermittedDbConnStrings.NpdsRegistryAuth1Dbserver);
    doorsAuth1Dbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForPermittedDbConnStrings.NpdsDirectoryAuth1Dbserver);
    nexusAuth1Dbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForPermittedDbConnStrings.NpdsDiristryAuth1Dbserver);
    portalCacheDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForPermittedDbConnStrings.NpdsRegistryCacheDbserver);
    doorsCacheDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForPermittedDbConnStrings.NpdsDirectoryCacheDbserver);
    nexusCacheDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForPermittedDbConnStrings.NpdsDiristryCacheDbserver);

    // optional database connection strings
    nlmmeshDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForOptionalDbConnStrings.NpdsNlmmeshDbserver);
    nlmmicadDbcs = PdpConfigManager.ParseAppDbConnString(PdpConst.NamesForOptionalDbConnStrings.NpdsNlmmicadDbserver);
  }

  // NPDS service connection strings
  public string NpdsUserDbconstr
  { get { return userDbcs; } }
  private string userDbcs;
  public string NpdsAgentDbconstr
  { get { return agentDbcs; } }
  private string agentDbcs;
  public string NpdsCoreDbconstr
  { get { return coreDbcs; } }
  private string coreDbcs;
  public string NpdsDiristryDbconstr
  { get { return diristryDbcs; } }
  private string diristryDbcs;
  public string NpdsRegistryDbconstr
  { get { return registryDbcs; } }
  private string registryDbcs;
  public string NpdsDirectoryDbconstr
  { get { return directoryDbcs; } }
  private string directoryDbcs;
  public string NpdsRegistrarDbconstr
  { get { return registrarDbcs; } }
  private string registrarDbcs;

  // NPDS PORTAL DataBase Connection String

  public string NpdsPortalAuth1Dbconstr
  { get { return portalAuth1Dbcs; } }
  private string portalAuth1Dbcs;

  public string NpdsPortalCacheDbconstr
  { get { return portalCacheDbcs; } }
  private string portalCacheDbcs;

  // NPDS DOORS DataBase Connection String

  public string NpdsDoorsAuth1Dbconstr
  { get { return doorsAuth1Dbcs; } }
  private string doorsAuth1Dbcs;

  public string NpdsDoorsCacheDbconstr
  { get { return doorsCacheDbcs; } }
  private string doorsCacheDbcs;

  // NPDS Nexus DataBase Connection Strings
  public string NpdsNexusAuth1Dbconstr
  { get { return nexusAuth1Dbcs; } }
  private string nexusAuth1Dbcs;

  public string NpdsNexusCacheDbconstr
  { get { return nexusCacheDbcs; } }
  private string nexusCacheDbcs;

  // NPDS NLM MeSH and MICAD DataBase Connection Strings
  public string NpdsNlmmeshDbconstr
  { get { return nlmmeshDbcs; } }
  private string nlmmeshDbcs;
  public string NpdsNlmmicadDbconstr
  { get { return nlmmicadDbcs; } }
  private string nlmmicadDbcs;


  public NpdsConst.SearchFilter NpdsDefaultSearchFilter
  { get { return defSearchFilter; } }
  private NpdsConst.SearchFilter defSearchFilter;

  public NpdsConst.SearchScope NpdsDefaultSearchScope
  { get { return defSearchScope; } }
  private NpdsConst.SearchScope defSearchScope;

  public NpdsConst.NodeType NpdsDefaultNodeType
  { get { return defNodeType; } }
  private NpdsConst.NodeType defNodeType;

  public NpdsConst.ServiceType NpdsDefaultServiceType
  { get { return defServiceType; } }
  private NpdsConst.ServiceType defServiceType;

  public NpdsConst.ServerType NpdsDefaultServerType
  { get { return sitDefServerType; } }
  private NpdsConst.ServerType sitDefServerType;

  public NpdsConst.DatabaseType NpdsDefaultDatabaseType
  { get { return defDatabaseType; } }
  private NpdsConst.DatabaseType defDatabaseType;

  public NpdsConst.DatabaseAccess NpdsDefaultDatabaseAccess
  { get { return defDatabaseAccess; } }
  private NpdsConst.DatabaseAccess defDatabaseAccess;

  public NpdsConst.RecordAccess NpdsDefaultRecordAccess
  { get { return defRecordAccess; } }
  private NpdsConst.RecordAccess defRecordAccess;

  public NpdsConst.ResrepFormat NpdsDefaultResrepFormat
  { get { return defResrepFormat; } }
  private NpdsConst.ResrepFormat defResrepFormat;

  public NpdsConst.MessageFormat NpdsDefaultMessageFormat
  { get { return defMessageFormat; } }
  private NpdsConst.MessageFormat defMessageFormat;

  public bool NpdsDefaultQueryFormat
  { get { return defQueryFormat; } }
  private bool defQueryFormat = false;

  // for use as filter when searching/selecting records
  public NpdsConst.EntityType NpdsDefaultEntityType
  { get { return defEntityType; } }
  private NpdsConst.EntityType defEntityType;

  // for use as default when creating new records
  public NpdsConst.EntityType NpdsDefaultNewEntityType
  { get { return defNewEntityType; } }
  private NpdsConst.EntityType defNewEntityType;

  // NPDS cyberinfrastructure root service tag
  public string NpdsRootServiceTag { get { return NpdsConst.NpdsRoot; } }

  // cache for tag-guid conversion
  public PdpTagGuidDictionary NpdsServiceCache { set; get; } = new PdpTagGuidDictionary();

  // diristry/registry/directory/registrar defaults with tags and guids

  public string NpdsDefaultDiristryTag
  { get { return defDiristryTag; } }
  private string defDiristryTag;

  public Guid NpdsDefaultDiristryGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultDiristryTag); } }

  public string NpdsDefaultRegistryTag
  { get { return defRegistryTag; } }
  private string defRegistryTag;

  public Guid NpdsDefaultRegistryGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultRegistryTag); } }

  public string NpdsDefaultDirectoryTag
  { get { return defDirectoryTag; } }
  private string defDirectoryTag;

  public Guid NpdsDefaultDirectoryGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultDirectoryTag); } }

  public string NpdsDefaultRegistrarTag
  { get { return defRegistrarTag; } }
  private string defRegistrarTag;

  public Guid NpdsDefaultRegistrarGuid
  { get { return NpdsServiceCache.GetByTag(NpdsDefaultRegistrarTag); } }

  // diristry/registry/directory/registrar constraints with tags only

  public string NpdsConstraintDiristryTag
  { get { return conDiristryTag; } }
  private string conDiristryTag;

  public string NpdsConstraintRegistryTag
  { get { return conRegistryTag; } }
  private string conRegistryTag;

  public string NpdsConstraintDirectoryTag
  { get { return conDirectoryTag; } }
  private string conDirectoryTag;

  public string NpdsConstraintRegistrarTag
  { get { return conRegistrarTag; } }
  private string conRegistrarTag;

}
