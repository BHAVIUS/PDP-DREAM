// NPDS.NexusCrossReference.Configuration.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{

  /// <summary>
  /// There are no comments for NexusCrossReferenceConfiguration in the schema.
  /// </summary>
  public partial class NexusCrossReferenceConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<NexusCrossReference> builder)
        {
        }
    }
}
