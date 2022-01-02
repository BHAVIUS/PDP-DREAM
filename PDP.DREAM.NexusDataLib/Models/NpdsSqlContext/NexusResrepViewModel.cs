// NexusResrepViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models
{
  public class NexusResrepViewModel : NexusViewModelBase
  {
    public NexusResrepViewModel()
    {
      itemXnam = NpdsConst.NexusResrepItemXnam;
    }

    public string? RecordHandle { get; set; } = string.Empty;
    public bool RecordIsDeleted { get; set; } = false;
    public short EntityTypeCode { get; set; } = (short)default(NpdsConst.EntityType);
    public string? EntityTypeName { get; set; } = string.Empty;

    public string? EntityInitialTag { get; set; } = string.Empty;
    public string? EntityName { get; set; } = string.Empty;
    public string? EntityNature { get; set; } = string.Empty;

    public string? EntityOwnerLabel { get; set; } = string.Empty;
    public string? EntityOwnerHandle { get; set; } = string.Empty;
    public Guid? EntityOwnerGuid { get; set; } = null;
    public string? EntityContactLabel { get; set; } = string.Empty;
    public string? EntityContactHandle { get; set; } = string.Empty;
    public Guid? EntityContactGuid { get; set; } = null;
    public string? EntityOtherLabel { get; set; } = string.Empty;
    public string? EntityOtherHandle { get; set; } = string.Empty;
    public Guid? EntityOtherGuid { get; set; } = null;
    public bool InfosetIsConcise { get; set; } = false;

    public bool InfosetIsAuthorPrivate { get; set; } = false;

    public bool InfosetIsAgentShared { get; set; } = false;

    public bool InfosetIsUpdaterLimited { get; set; } = false;

    public bool InfosetIsManagerReleased { get; set; } = false;

    public short InfosetPortalStatusCode { get; set; } = (short)default(NpdsConst.InfosetStatus);
    public string? InfosetPortalStatusName { get; set; } = string.Empty;

    public short InfosetDoorsStatusCode { get; set; } = (short)default(NpdsConst.InfosetStatus);
    public string? InfosetDoorsStatusName { get; set; } = string.Empty;


    // Nexus DiristryGuid & DiristryName
    private Guid? diristryGuid = NpdsServiceDefaults.Values.NpdsDefaultDiristryGuid;
    public Guid? RecordDiristryGuid
    { get { return diristryGuid; } set { diristryGuid = value; } }
    public string? RecordDiristryTag { get; set; } = string.Empty;
    public string? RecordDiristryName { get; set; } = string.Empty;

    // PORTAL RegistryGuid & RegistryName
    private Guid? registryGuid = NpdsServiceDefaults.Values.NpdsDefaultRegistryGuid;
    public Guid? RecordRegistryGuid
    { get { return registryGuid; } set { registryGuid = value; } }
    public string? RecordRegistryTag { get; set; } = string.Empty;
    public string? RecordRegistryName { get; set; } = string.Empty;

    // DOORS DirectoryGuid & DirectoryName
    private Guid? directoryGuid = NpdsServiceDefaults.Values.NpdsDefaultDirectoryGuid;
    public Guid? RecordDirectoryGuid
    { get { return directoryGuid; } set { directoryGuid = value; } }
    public string? RecordDirectoryTag { get; set; } = string.Empty;
    public string? RecordDirectoryName { get; set; } = string.Empty;

    // Scribe RegistrarGuid & RegistrarName
    private Guid? registrarGuid = NpdsServiceDefaults.Values.NpdsDefaultRegistrarGuid;
    public Guid? RecordRegistrarGuid
    { get { return registrarGuid; } set { registrarGuid = value; } }
    public string? RecordRegistrarTag { get; set; } = string.Empty;
    public string? RecordRegistrarName { get; set; } = string.Empty;

    //  Infoset Status Codes, Names, and Counts

    public short ResrepEntityStatusCode { get; set; } = 0;
    public string ResrepEntityStatusName { get; set; } = string.Empty;

    public short ResrepRecordStatusCode { get; set; } = 0;
    public string ResrepRecordStatusName { get; set; } = string.Empty;

    public short ResrepInfosetStatusCode { get; set; } = 0;
    public string ResrepInfosetStatusName { get; set; } = string.Empty;


    public int EntityLabelsCount { get; set; } = 0;
    public short EntityLabelsStatusCode { get; set; } = 0;
    public string EntityLabelsStatusName { get; set; } = string.Empty;

    public int SupportingTagsCount { get; set; } = 0;
    public short SupportingTagsStatusCode { get; set; } = 0;
    public string SupportingTagsStatusName { get; set; } = string.Empty;
    public int SupportingLabelsCount { get; set; } = 0;
    public short SupportingLabelsStatusCode { get; set; } = 0;
    public string SupportingLabelsStatusName { get; set; } = string.Empty;
    public int CrossReferencesCount { get; set; } = 0;
    public short CrossReferencesStatusCode { get; set; } = 0;
    public string CrossReferencesStatusName { get; set; } = string.Empty;
    public int OtherTextsCount { get; set; } = 0;
    public short OtherTextsStatusCode { get; set; } = 0;
    public string OtherTextsStatusName { get; set; } = string.Empty;

    public int LocationsCount { get; set; } = 0;
    public short LocationsStatusCode { get; set; } = 0;
    public string LocationsStatusName { get; set; } = string.Empty;
    public int DescriptionsCount { get; set; } = 0;
    public short DescriptionsStatusCode { get; set; } = 0;
    public string DescriptionsStatusName { get; set; } = string.Empty;
    public int ProvenancesCount { get; set; } = 0;
    public short ProvenancesStatusCode { get; set; } = 0;
    public string ProvenancesStatusName { get; set; } = string.Empty;
    public int DistributionsCount { get; set; } = 0;
    public short DistributionsStatusCode { get; set; } = 0;
    public string DistributionsStatusName { get; set; } = string.Empty;
    public int FairMetricsCount { get; set; } = 0;
    public short FairMetricsStatusCode { get; set; } = 0;
    public string FairMetricsStatusName { get; set; } = string.Empty;
    public int NexusSnapshotsCount { get; set; } = 0;
    public short NexusSnapshotsStatusCode { get; set; } = 0;
    public string NexusSnapshotsStatusName { get; set; } = string.Empty;

  } // class

} // namespace
