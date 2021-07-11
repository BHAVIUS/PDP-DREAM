// NPDS.NexusSupportingTag.Configuration.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{

  /// <summary>
  /// There are no comments for NexusSupportingTagConfiguration in the schema.
  /// </summary>
  public partial class NexusSupportingTagConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<NexusSupportingTag> builder)
        {
        }
    }
}
