using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.CoreDataLib.Stores
{

    /// <summary>
    /// There are no comments for DoorsDistributionConfiguration in the schema.
    /// </summary>
    public partial class DoorsDistributionConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<DoorsDistribution> builder)
        {
        }
    }
}
