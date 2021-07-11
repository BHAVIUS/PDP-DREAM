﻿// SiaaUserExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public static class SiaaUserExtensions
  {
    // using System.Security.Principal.IPrincipal
    // private IPrincipal pdpPrincipal; // with readonly Identity (type IIdentity) and IsInRole (type bool) for current user
    // using System.Security.Principal.IIdentity
    // private IIdentity pdpIdentity; // with readonly AuthenticationType (string), IsAuthenticated (bool), Name (string) for current user
    public static string GetUserName(this ClaimsPrincipal principal)
    {
      string userName;
      if (principal == null) { throw new ArgumentNullException("PDP Identity ClaimsPrincipal is null for PDP UserName"); }
      userName = principal.FindFirstValue(SiaaClaimTypes.UserName);
      return userName;
    }

    public static Guid GetUserGuid(this ClaimsPrincipal principal)
    {
      Guid userGuid;
      if (principal == null) { throw new ArgumentNullException("PDP Identity ClaimsPrincipal is null for PDP UserGuid"); }
      var strGuid = principal.FindFirstValue(SiaaClaimTypes.UserGuid);
      Guid.TryParse(strGuid, out userGuid);
      return userGuid;
    }

    public static Guid GetAgentGuid(this ClaimsPrincipal principal)
    {
      Guid agentGuid;
      if (principal == null) { throw new ArgumentNullException("PDP Identity ClaimsPrincipal is null for PDP AgentGuid"); }
      var strGuid = principal.FindFirstValue(SiaaClaimTypes.AgentGuid);
      Guid.TryParse(strGuid, out agentGuid);
      return agentGuid;
    }

    public static Guid GetSessionGuid(this ClaimsPrincipal principal)
    {
      Guid sessionGuid;
      if (principal == null) { throw new ArgumentNullException("PDP Identity ClaimsPrincipal is null for PDP SessionGuid"); }
      var strGuid = principal.FindFirstValue(SiaaClaimTypes.SessionGuid);
      Guid.TryParse(strGuid, out sessionGuid);
      return sessionGuid;
    }

    public static async Task SignoutUserAsync(this HttpContext cntxt, bool useAwaiter = false)
    {
      var authMethod = PdpConst.PdpIdentityScheme;
      await cntxt.SignOutAsync(authMethod).ConfigureAwait(useAwaiter);
      return;
    }
    public static async Task SignoutUserAsync(this HttpContext cntxt, string authMethod, bool useAwaiter = false)
    {
      if (string.IsNullOrEmpty(authMethod)) { authMethod = PdpConst.PdpIdentityScheme; }
      await cntxt.SignOutAsync(authMethod).ConfigureAwait(useAwaiter);
      return;
    }

    public static async Task<PdpIdentityResult> SigninUserAsync(this HttpContext cntxt, string userName,
      Guid userGuid, Guid agentGuid, Guid sessionGuid, List<string> userRoles, bool useAwaiter = false)
    {
      var authProps = new AuthenticationProperties();
      var authMethod = PdpConst.PdpIdentityScheme;
      var principal = CreateUserPrincipal(userName, userGuid, agentGuid, sessionGuid, userRoles, authMethod);
      await cntxt.SignInAsync(PdpConst.PdpIdentityScheme, principal, authProps).ConfigureAwait(useAwaiter);
      var result = principal.CheckUserPrincipal(); // recheck current online user
      return result;
    }

    public static async Task<PdpIdentityResult> SigninUserAsync(this HttpContext cntxt, string userName,
      Guid userGuid, Guid agentGuid, Guid sessionGuid, List<string> userRoles,
      AuthenticationProperties authProps, string authMethod, bool useAwaiter = false)
    {
      if (authProps == null) { authProps = new AuthenticationProperties(); }
      if (string.IsNullOrEmpty(authMethod)) { authMethod = PdpConst.PdpIdentityScheme; }
      var principal = CreateUserPrincipal(userName, userGuid, agentGuid, sessionGuid, userRoles, authMethod);
      await cntxt.SignInAsync(PdpConst.PdpIdentityScheme, principal, authProps).ConfigureAwait(useAwaiter);
      var result = principal.CheckUserPrincipal(); // recheck current online user
      return result;
    }

    public static ClaimsPrincipal CreateUserPrincipal(string userName, Guid userGuid,
      Guid agentGuid, Guid sessionGuid, List<string> userRoles, string authMethod)
    {
      if (string.IsNullOrEmpty(userName) && userGuid.IsEmpty() &&
        agentGuid.IsEmpty() && sessionGuid.IsEmpty())
      { throw new ArgumentNullException("all identifiers are null or empty in CreateUserPrincipal"); }
      if (string.IsNullOrEmpty(authMethod)) { authMethod = PdpConst.PdpIdentityScheme; }
      var authMethodClaim = new Claim(ClaimTypes.AuthenticationMethod, authMethod);
      var principalClaim = new Claim(ClaimTypes.Name, userName); // required for use of User.Identity.Name
      var userNameClaim = new Claim(SiaaClaimTypes.UserName, userName);
      var userGuidClaim = new Claim(SiaaClaimTypes.UserGuid, userGuid.ToString());
      var agentGuidClaim = new Claim(SiaaClaimTypes.AgentGuid, agentGuid.ToString());
      var sessionGuidClaim = new Claim(SiaaClaimTypes.SessionGuid, sessionGuid.ToString());
      var allClaims = new List<Claim>
      {
        authMethodClaim, principalClaim, userNameClaim, userGuidClaim, agentGuidClaim, sessionGuidClaim
      };
      if ((userRoles != null) && (userRoles.Count > 0))
      {
        foreach (var userRole in userRoles)
        {
          var userRoleClaim = new Claim(ClaimTypes.Role, userRole);
          allClaims.Add(userRoleClaim);
        }
      }
      var userIdentity = new ClaimsIdentity(allClaims, authMethod);
      var userPrincipal = new ClaimsPrincipal(userIdentity);
      return userPrincipal;
    }

    public static PdpIdentityResult CheckUserPrincipal(this ClaimsPrincipal userPrincipal)
    {
      var result = new PdpIdentityResult();
      if (userPrincipal.Identity.IsAuthenticated) { result.Succeeded = true; }
      else { result.Failed = true; }
      return result;
    }

  }

}
