using System;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Stores.PdpIdentity
{
  public partial class PdpAgentCmsContext
  {
    public PdpAgentCmsContext(string dbcs)
    {
      if (string.IsNullOrEmpty(dbcs)) { dbcs = NpdsServiceDefaults.GetValues.NpdsAgentDbconstr; }
      if (string.IsNullOrEmpty(dbcs)) { throw new NullReferenceException("dbcs null or empty in PdpAgentCmsContext"); }
      var optionsBuilder = new DbContextOptionsBuilder<PdpAgentCmsContext>();
      optionsBuilder.UseSqlServer(dbcs);
      base.OnConfiguring(optionsBuilder);
      OnCreated();
    }

    public string StoreChanges()
    {
      try
      {
        this.SaveChanges();
        return string.Empty;
      }
      catch (Exception ex)
      {
        var inMessage = ex.InnerException.Message;
        return ex.Message + inMessage;
      }
    }

  } // end class

} // end namespace
