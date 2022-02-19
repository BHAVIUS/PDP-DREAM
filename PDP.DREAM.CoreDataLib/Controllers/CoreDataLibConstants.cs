// CoreDataLibConstants.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Controllers;

// CoreDLC = CoreDataLib Controller Constants
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
  // prioritize views over pages over project during migration
  // 
  // raord = Route App ORDer
  public const int raordView = -1; // by MVC view
  public const int raordPage = 0; // by Razor page
  // ranp = Route App NamePrefix
  public const string ranpView = "CoreDataLibView"; // by MVC view
  public const string ranpPage = "CoreDataLibPage"; // by Razor page
  // rans = Route App NameSuffix,
  public const string ransGet = "Get"; // may include post for Get/Post
  public const string ransPut = "Put"; // may include post for Put/Post
  public const string ransDel = "Del"; // may include post for Delete/Post
  public const string ransPost = "Post"; // for post only
  // rats = Route App TemplateSuffix
  public const string ratsRg = "{recordGuid}";
  public const string ratsRgil = "{recordGuid}/{isLimited?}";
  public const string ratsStstet = "{serviceType}/{serviceTag}/{entityType?}";

}
