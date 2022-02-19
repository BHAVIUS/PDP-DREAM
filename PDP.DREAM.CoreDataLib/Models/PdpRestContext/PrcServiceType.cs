// PrcServiceType.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Reflection;

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class PdpRestContext
{
  // in file NpdsConstants.cs
  // public enum ServiceType { Nexus = 1, PORTAL, DOORS, Scribe };

  // default values

  public NpdsConst.ServiceType ServiceTypeDeflt { get; }
    = NpdsServiceDefaults.Values.NpdsDefaultServiceType;

  // requested values

  public string ServiceTypeReqst
  {
    set {
      reqServiceType = value;
      if (!string.IsNullOrEmpty(reqServiceType)) { serviceType = ValidateServiceType(reqServiceType); }
    }
    get { return reqServiceType; }
  }
  private string reqServiceType = string.Empty;

  // validated values

  public NpdsConst.ServiceType ServiceType
  {
    set { serviceType = ValidateServiceType(value); }
    get { return serviceType; }
  }
  private NpdsConst.ServiceType serviceType = NpdsConst.ServiceType.Nexus;

  // validators

  private NpdsConst.ServiceType ValidateServiceType(string strValue)
  {
    NpdsConst.ServiceType enmValue = PdpEnum<NpdsConst.ServiceType>.Parse(strValue, NpdsConst.ServiceType.Nexus);
    enmValue = ValidateServiceType(enmValue);
    return enmValue;
  }
  private NpdsConst.ServiceType ValidateServiceType(NpdsConst.ServiceType enmValue)
  {
    switch (enmValue)
    {
      case NpdsConst.ServiceType.Nexus:
        SearchFilterReqst = NpdsConst.SearchFilter.Diristry.ToString();
        break;
      case NpdsConst.ServiceType.PORTAL:
        SearchFilterReqst = NpdsConst.SearchFilter.Registry.ToString();
        break;
      case NpdsConst.ServiceType.DOORS:
        SearchFilterReqst = NpdsConst.SearchFilter.Directory.ToString();
        break;
      case NpdsConst.ServiceType.Scribe:
        SearchFilterReqst = NpdsConst.SearchFilter.Registrar.ToString();
        break;
      default:
        throw new Exception($"case not implemented for ServiceType value {enmValue.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
    }
    return enmValue;
  }

}
