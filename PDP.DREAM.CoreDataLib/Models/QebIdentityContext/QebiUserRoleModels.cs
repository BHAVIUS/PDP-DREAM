// QebiUserRoleModels.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserUxm
{
  public QebiUserUxm(Guid appGuid, Guid usrGuid, string firstName, string lastName,
    string userName, string emailAddress, bool isApproved, IList<string>? roleList)
  {
    AppGuid = appGuid; UserGuid = usrGuid; FirstName = firstName; LastName = lastName;
    UserName = userName; EmailAddress = emailAddress; UserIsApproved = isApproved; UserRoleList = roleList;
  }

  public Guid AppGuid { get; set; } = PdpSiteSettings.Values.AppSecureUiaaGuid;
  public Guid UserGuid { get; set; } = Guid.Empty;

  // ATTN: does not update in Telerik controls unless use simple standard property
  public string UserRoleNames { get; set; } = string.Empty;

  private IList<string>? userRoleList = new List<string>() { string.Empty };
  public IList<string>? UserRoleList
  {
    get {
      if (userRoleList == null) { userRoleList = new List<string>() { string.Empty }; }
      return userRoleList;
    }
    set {
      if (value != null)
      {
        userRoleList = value;
        UserRoleNames = JoinUserRoles();
      }
    }
  }

  public string JoinUserRoles()
  {
    return string.Join(" | ", UserRoleList);
  }
  public IList<string>? SplitUserRoles()
  {
    userRoleList = UserRoleNames.Split("|", StringSplitOptions.TrimEntries).ToList();
    return userRoleList;
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
