﻿// SIAA.QebIdentityAppUserRoleLink.Configuration.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.SiaaDataLib.Stores.PdpIdentity
{

  /// <summary>
  /// There are no comments for QebIdentityAppUserRoleLinkConfiguration in the schema.
  /// </summary>
  public partial class QebIdentityAppUserRoleLinkConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<QebIdentityAppUserRoleLink> builder)
        {
        }
    }
}
