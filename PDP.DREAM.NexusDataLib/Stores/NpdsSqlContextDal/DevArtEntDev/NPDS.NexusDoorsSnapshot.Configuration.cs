﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.NexusDataLib.Stores
{

    /// <summary>
    /// There are no comments for NexusDoorsSnapshotConfiguration in the schema.
    /// </summary>
    public partial class NexusDoorsSnapshotConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<NexusDoorsSnapshot> builder)
        {
        }
    }
}
