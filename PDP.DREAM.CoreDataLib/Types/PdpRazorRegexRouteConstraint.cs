// PdpMvcRouteConstraint.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public abstract class PdpRazorRegexRouteConstraint : IRouteConstraint
{
  private Regex rgxPMRC;

  public PdpRazorRegexRouteConstraint(string rgxPattern)
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

public class NpdsPrincipalTagConstraint : PdpRazorRegexRouteConstraint
{
  public NpdsPrincipalTagConstraint() : base(RgxsPrincipalTag) { }
}
public class NpdsInfosetStatusConstraint : PdpRazorRegexRouteConstraint
{
  public NpdsInfosetStatusConstraint() : base(RgxsInfosetStatus) { }
}

public class DiristryServiceTypesConstraint : PdpRazorRegexRouteConstraint
{
  public DiristryServiceTypesConstraint() : base(RgxsDiristryServiceTypes) { }
}
public class RegistryServiceTypesConstraint : PdpRazorRegexRouteConstraint
{
  public RegistryServiceTypesConstraint() : base(RgxsRegistryServiceTypes) { }
}
public class DirectoryServiceTypesConstraint : PdpRazorRegexRouteConstraint
{
  public DirectoryServiceTypesConstraint() : base(RgxsDirectoryServiceTypes) { }
}
public class RegistrarServiceTypesConstraint : PdpRazorRegexRouteConstraint
{
  public RegistrarServiceTypesConstraint() : base(RgxsRegistrarServiceTypes) { }
}

public class NexusTkgridCntrlConstraint : PdpRazorRegexRouteConstraint
{
  public NexusTkgridCntrlConstraint() : base(RgxsNexusTKGAC) { }
}
public class ScribeTkgridCntrlConstraint : PdpRazorRegexRouteConstraint
{
  public ScribeTkgridCntrlConstraint() : base(RgxsScribeTKGAC) { }
}
