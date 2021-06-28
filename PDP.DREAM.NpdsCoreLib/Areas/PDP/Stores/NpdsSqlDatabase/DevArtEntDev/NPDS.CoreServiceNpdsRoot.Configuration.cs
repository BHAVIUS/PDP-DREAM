using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{

    /// <summary>
    /// There are no comments for CoreServiceNpdsRootConfiguration in the schema.
    /// </summary>
    public partial class CoreServiceNpdsRootConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<CoreServiceNpdsRoot> builder)
        {
        }
    }
}
