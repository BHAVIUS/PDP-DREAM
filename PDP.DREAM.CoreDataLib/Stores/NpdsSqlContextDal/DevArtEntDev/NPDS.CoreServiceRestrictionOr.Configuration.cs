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
    /// There are no comments for CoreServiceRestrictionOrConfiguration in the schema.
    /// </summary>
    public partial class CoreServiceRestrictionOrConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<CoreServiceRestrictionOr> builder)
        {
        }
    }
}
