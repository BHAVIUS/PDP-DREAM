// NexusDataLibConstants.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

// NexusDLC = NexusDataLib Constants
public static class NexusDLC
{
  // initial slash assures path from web app root
  public const string PdpPathUserIndex = "/NPDS/UserNexus/Index";
  public const string PdpPathAgentIndex = "/NPDS/AgentNexus/Index";
  public const string PdpPathIdentRequired = "/NPDS/AnonNexus/AccessDenied";
  public const string PdpPathIdentLogin = "/NPDS/AnonNexus/LoginUser";
  public const string PdpPathIdentLogout = "/NPDS/AuthNexus/LogoutUser";
  public const string PdpPathIdentProfile = "/NPDS/AuthNexus/DisplayProfile";
  public const string PdpPathIdentManage = "/NPDS/AdminNexus/Index";
}
