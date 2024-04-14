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
    /// There are no comments for CiaamNetAppConfiguration in the schema.
    /// </summary>
    public partial class CiaamNetAppConfiguration
    {
        partial void CustomizeConfiguration(EntityTypeBuilder<CiaamNetApp> builder)
        {
        }
    }
}
