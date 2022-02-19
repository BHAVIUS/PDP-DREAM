using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Pages;

public class NpdsSiteInfo : PdpPrcWebPageControllerBase
{
  public void OnGet()
  {
    string npdsTag = string.Empty;
    if (string.IsNullOrWhiteSpace(PRC.SiteTitle))
    {
      if (PRC.ServerType == NpdsConst.ServerType.Diristry)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultDiristryTag;
        PRC.SiteTitle = string.Format("{0} Nexus Diristry", npdsTag);
      }
      else if (PRC.ServerType == NpdsConst.ServerType.Registry)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultRegistryTag;
        PRC.SiteTitle = string.Format("{0} PORTAL Registry", npdsTag);
      }
      else if (PRC.ServerType == NpdsConst.ServerType.Directory)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultDirectoryTag;
        PRC.SiteTitle = string.Format("{0} DOORS Directory", npdsTag);
      }
      else if (PRC.ServerType == NpdsConst.ServerType.Registrar)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultRegistrarTag;
        PRC.SiteTitle = string.Format("{0} Scribe Registrar", npdsTag);
      }
    }
    if (string.IsNullOrWhiteSpace(PRC.PageTitle))
    {
      if (PRC.ServerType != NpdsConst.ServerType.Registrar)
      {
        PRC.PageTitle = string.Format("{0} Resource Metadata Records", npdsTag);
      }
    }
  }

}
