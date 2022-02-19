// QebiUserRoleModels.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserUxm
{
  private IList<string>? userRoleList = new List<string>() { string.Empty };
  public IList<string>? UserRoleList
  {
    get {
      if (userRoleList == null)
      {
        if (!string.IsNullOrEmpty(UserRoleNames)) { userRoleList = UserRoleNames.SplitOrStringToList(); }
        else { userRoleList = new List<string>() { string.Empty }; }
      }
      return userRoleList;
    }
    set {
      if (value != null)
      {
        userRoleList = value;
        UserRoleNames = userRoleList.JoinListToOrString();
      }
    }
  }
}

public class QebiRoleUxm
{
  public Guid AppGuid { get; set; } = PdpSiteSettings.Values.AppSecureUiaaGuid;
  public Guid RoleGuid { get; set; } = Guid.Empty;
  public string RoleName { get; set; } = string.Empty;
  public string RoleDescription { get; set; } = string.Empty;
}

public class QebiUserRoleUxm
{
  public Guid AppGuid { get; set; } = PdpSiteSettings.Values.AppSecureUiaaGuid;
  public Guid UserGuid { get; set; } = Guid.Empty;
  public string UserName { get; set; } = string.Empty;
  public Guid RoleGuid { get; set; } = Guid.Empty;
  public string RoleName { get; set; } = string.Empty;
  public string RoleDescription { get; set; } = string.Empty;
}
