﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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