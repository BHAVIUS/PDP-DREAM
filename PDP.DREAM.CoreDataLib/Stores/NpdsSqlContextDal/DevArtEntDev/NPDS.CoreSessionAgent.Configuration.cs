using System;
using System.CodeDom.Compiler;
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
    /// There are no comments for CoreSessionAgentConfiguration in the schema.
    /// </summary>
    public partial class CoreSessionAgentConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<CoreSessionAgent> builder)
        {
        }
    }
}
