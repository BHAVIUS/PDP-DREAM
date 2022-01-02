// ParseNpdsUrlSegments.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  public partial class PdpRestContext
  {
    // prioritize first the URL route segments then override only if QS values present
    public void ParseUrlSegments(string serviceType, string serviceTag,
      string entityTag = "", string entityVersion = "", string entityType = "", string infosetStatus = "")
    {
      PRC.ServiceTypeReqst = serviceType;
      PRC.ServiceTagReqst = serviceTag;

      if (string.IsNullOrEmpty(entityTag)) { PRC.EntityTag = string.Empty; }
      else { PRC.EntityTagReqst = entityTag; }

      if (string.IsNullOrEmpty(entityVersion)) { PRC.EntityVersion = string.Empty; }
      else { PRC.EntityVersionReqst = entityVersion; }

      if (string.IsNullOrEmpty(entityType)) { PRC.EntityType = NpdsConst.EntityType.AnyAndAll; }
      else { PRC.EntityTypeReqst = entityType; }

      if (string.IsNullOrEmpty(infosetStatus)) { PRC.InfosetStatus = NpdsConst.InfosetStatus.AnyAndAll; }
      else { PRC.InfosetStatusReqst = infosetStatus; }
    }

  }

}