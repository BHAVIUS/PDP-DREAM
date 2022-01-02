// PdpMvcRouteConstraint.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Globalization;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Types;

public abstract class PdpMvcRouteRegexConstraint : IRouteConstraint
{
  private Regex rgxPMRC;

  public PdpMvcRouteRegexConstraint(string rgxPattern)
  {
    rgxPMRC = new Regex(rgxPattern, // example regex @"^[1-9]*$"
                        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
                        TimeSpan.FromMilliseconds(111));
  }

  bool IRouteConstraint.Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
  {
    if (values.TryGetValue(routeKey, out object value))
    {
      var routeKeyValue = Convert.ToString(value, CultureInfo.InvariantCulture);
      if (routeKeyValue == null) { return false; }
      return rgxPMRC.IsMatch(routeKeyValue);
    }
    return false;
  }

} // class

public class NpdsPrincipalTagConstraint : PdpMvcRouteRegexConstraint
{
  public NpdsPrincipalTagConstraint() : base(NpdsConst.RegexPrincipalTag) { }
}
public class NpdsInfosetStatusConstraint : PdpMvcRouteRegexConstraint
{
  public NpdsInfosetStatusConstraint() : base(NpdsConst.RegexInfosetStatus) { }
}

public class DiristryServiceTypesConstraint : PdpMvcRouteRegexConstraint
{
  public DiristryServiceTypesConstraint() : base(NpdsConst.RegexDiristryServiceTypes) { }
}
public class RegistryServiceTypesConstraint : PdpMvcRouteRegexConstraint
{
  public RegistryServiceTypesConstraint() : base(NpdsConst.RegexRegistryServiceTypes) { }
}
public class DirectoryServiceTypesConstraint : PdpMvcRouteRegexConstraint
{
  public DirectoryServiceTypesConstraint() : base(NpdsConst.RegexDirectoryServiceTypes) { }
}
public class RegistrarServiceTypesConstraint : PdpMvcRouteRegexConstraint
{
  public RegistrarServiceTypesConstraint() : base(NpdsConst.RegexRegistrarServiceTypes) { }
}

public class NexusTkgridCntrlConstraint : PdpMvcRouteRegexConstraint
{
  public NexusTkgridCntrlConstraint() : base(NpdsConst.RegexNexusTKGAC) { }
}
public class ScribeTkgridCntrlConstraint : PdpMvcRouteRegexConstraint
{
  public ScribeTkgridCntrlConstraint() : base(NpdsConst.RegexScribeTKGAC) { }
}
