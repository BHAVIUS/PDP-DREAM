// IDbsqlContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDP.DREAM.CoreDataLib.Stores;

public interface IDbsqlContext
{
  public INpdsClient NPDSCP { get; set; }

  public SqlConnection? DbsqlConnect();

  public void DbsqlDisconnect();

}
