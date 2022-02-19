// PrcServiceTitle.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models
{
  public partial class PdpRestContext
  {
    // default values

    // requested values

    // validated values
    public string ServiceTitle
    {
      get
      {
        switch (ServiceType)
        {
          case NpdsConst.ServiceType.Nexus:
            serviceTitle = $"{DiristryTag} Diristry";
            break;
          case NpdsConst.ServiceType.PORTAL:
            serviceTitle = $"{RegistryTag} Registry";
            break;
          case NpdsConst.ServiceType.DOORS:
            serviceTitle = $"{DirectoryTag} Directory";
            break;
          case NpdsConst.ServiceType.Scribe:
            serviceTitle = $"{RegistrarTag} Registrar";
            break;
          case NpdsConst.ServiceType.Core:
            serviceTitle = "NPDS Core Service";
            break;
          default:
            throw new Exception("invalid ServiceType in ServiceTitle get");
        }
        return serviceTitle;
      }
    }
    private string serviceTitle = string.Empty;

    // validators

  }

}
