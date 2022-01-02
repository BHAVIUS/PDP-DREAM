// INpdsResourceRepresentation.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  // TODO: consider redesign for covariance and contravariance
  // interfaces covariant when output args marked with out
  // interfaces contravariant when input args marked with in

  public interface INpdsResrepBase
  {
    // permitted attributes
    public bool ResrepIsAuthorPrivate { get; set; }
    public bool ResrepIsAgentShared { get; set; }
    public bool ResrepIsUpdaterLimited { get; set; } 
    public bool ResrepIsManagerReleased { get; set; } 
  }

  //public interface IPdsdrResRepCore : IPdsdrResRepBase
  //{
  //    // required elements
  //    CoreEntityMetadataItem EntityMetadata { get; set; }
  //    CoreRecordMetadataItem RecordMetadata { get; set; }
  //    CoreInfosetMetadataItem InfosetMetadata { get; set; }
  //}

  //public interface IPdsdrResRepPortal : IPdsdrResRepBase
  //{
  //    // required elements
  //    PortalEntityMetadataItem EntityMetadata { get; set; }
  //    PortalRecordMetadataItem RecordMetadata { get; set; }
  //    PortalInfosetMetadataItem InfosetMetadata { get; set; }
  //}

  //public interface IPdsdrResRepDoors : IPdsdrResRepBase
  //{
  //    // required elements
  //    DoorsEntityMetadataItem EntityMetadata { get; set; }
  //    DoorsRecordMetadataItem RecordMetadata { get; set; }
  //    DoorsInfosetMetadataItem InfosetMetadata { get; set; }
  //}

  public interface INpdsResrep : INpdsResrepBase
  {
    // required elements
    NpdsMetadataEntityItem EntityMetadata { get; set; }
    NpdsMetadataRecordItem RecordMetadata { get; set; }
    NpdsMetadataInfosetItem InfosetMetadata { get; set; }
  }

}
