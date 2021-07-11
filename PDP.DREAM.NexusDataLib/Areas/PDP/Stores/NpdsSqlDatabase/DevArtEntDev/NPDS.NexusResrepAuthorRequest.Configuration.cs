// NPDS.NexusResrepAuthorRequest.Configuration.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{

  /// <summary>
  /// There are no comments for NexusResrepAuthorRequestConfiguration in the schema.
  /// </summary>
  public partial class NexusResrepAuthorRequestConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<NexusResrepAuthorRequest> builder)
        {
        }
    }
}
