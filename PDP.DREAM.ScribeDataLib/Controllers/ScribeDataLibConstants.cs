// ScribeDataLibConstants.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Controllers;

// ScribeDLC = ScribeDataLib Controller Constants
public static class ScribeDLC
{
  // initial slash assures path from web app root
  public const string PdpPathUserIndex = "/NPDS/UserScribe/Index";
  public const string PdpPathAgentIndex = "/NPDS/AgentScribe/Index";
  public const string PdpPathIdentRequired = "/NPDS/AnonScribe/AccessDenied";
  public const string PdpPathIdentLogin = "/NPDS/AnonScribe/LoginUser";
  public const string PdpPathIdentLogout = "/NPDS/AuthScribe/LogoutUser";
  public const string PdpPathIdentProfile = "/NPDS/AuthScribe/DisplayProfile";
  public const string PdpPathIdentManage = "/NPDS/AdminScribe/Index";

  // ranp = Route App NamePrefix
  public const string ranpView = "ScribeDataLibView"; // by MVC view
  public const string ranpPage = "ScribeDataLibPage"; // by Razor page

}
