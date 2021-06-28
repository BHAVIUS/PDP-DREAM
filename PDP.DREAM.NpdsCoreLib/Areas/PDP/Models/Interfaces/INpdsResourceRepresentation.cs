using System;

// Interfaces for PDS data records; code uses acronyms PDSDR or Pdsdr in interface names;
// original design for PDS data records in 2008 IEEE TITB vol 12 no 2 pp 194-196;
// see Sections VII A and VII B of paper
//
// code also uses abbreviation ResRep for Resource Representation

namespace PDP.DREAM.NpdsCoreLib.Models
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
