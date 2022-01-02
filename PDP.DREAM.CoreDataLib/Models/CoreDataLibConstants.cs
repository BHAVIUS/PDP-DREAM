// CorePathConstants.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// CoreDLC = CoreDataLib Constants
public static class CoreDLC
{
  // initial slash assures path from web app root
  public const string PdpPathSiteIndex = "/NPDS/Site/Index";
  public const string PdpPathSiteInfo = "/NPDS/Site/Info";
  public const string PdpPathSiteHelp = "/NPDS/Site/Help";
  public const string PdpPathSiteTest = "/NPDS/CoreTestLib/PrcTest";
  public const string PdpPathSiteErrors = "/NPDS/CoreTestLib/MvcErrors";
  public const string PdpPathSiteRoutes = "/NPDS/CoreTestLib/MvcRoutes";

  // Anon/Auth for Core
  public const string PdpPathUserIndex = "/NPDS/UserCore/Index";
  public const string PdpPathAgentIndex = "/NPDS/AgentCore/Index";
  public const string PdpPathIdentRequired = "/NPDS/AnonCore/AccessDenied";
  public const string PdpPathIdentLogin = "/NPDS/AnonCore/LoginUser";
  public const string PdpPathIdentLogout = "/NPDS/AuthCore/LogoutUser";
  public const string PdpPathIdentProfile = "/NPDS/AuthCore/DisplayProfile";
  public const string PdpPathIdentManage = "/NPDS/AdminCore/Index";

  // used by attribute and convention routing for views and pages
  public const string RouteAppNameNpdsView = "NpdsApi";
  public const int RouteAppOrderNpdsView = -1;
  public const string RouteAppNamePdpPage = "PdpWeb";
  public const int RouteAppOrderPdpPage = 0;
}
