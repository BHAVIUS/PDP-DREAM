// SiaaUserRoleModels.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
{
  public class SiaaUserUxm
  {
    public SiaaUserUxm() { }
    public SiaaUserUxm(Guid appGuid, Guid usrGuid, string firstName, string lastName,
      string userName, string emailAddress, bool isApproved, IList<string>? roleList)
    {
      AppGuid = appGuid; UserGuid = usrGuid; FirstName = firstName; LastName = lastName;
      UserName = userName; EmailAddress = emailAddress; UserIsApproved = isApproved; UserRoleList = roleList;
    }

    public Guid AppGuid { get; set; } = PdpSiteSettings.GetValues.AppSecureUiaaGuid;
    public Guid UserGuid { get; set; } = Guid.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public bool UserIsApproved { get; set; } = false;
    public IList<string>? UserRoleList
    {
      get
      {
        if (userRoleList == null) { userRoleList = new List<string>() { string.Empty }; }
        return userRoleList;
      }
      set
      {
        if (value != null)
        {
          userRoleList = value;
          UserRoleNames = JoinUserRoles();
        }
      }
    }
    // private string userRoleNames = string.Empty;
    private IList<string>? userRoleList = new List<string>() { string.Empty };

    // ATTN: does not update in Telerik controls unless use simple standard property
    public string UserRoleNames { get; set; } = string.Empty;
    //{
    //  get
    //  {
    //    if (userRoleList != null) { JoinUserRoles(); }
    //    return userRoleNames;
    //  }
    //  set { userRoleNames = value; }
    //}
    public string JoinUserRoles()
    {
      //userRoleNames = string.Join(" | ", UserRoleList);
      //return userRoleNames;
      return string.Join(" | ", UserRoleList);
    }
    public IList<string>? SplitUserRoles()
    {
      userRoleList = UserRoleNames.Split("|", StringSplitOptions.TrimEntries).ToList();
      return userRoleList;
    }

  }

  public class SiaaRoleUxm
  {
    public Guid AppGuid { get; set; } = PdpSiteSettings.GetValues.AppSecureUiaaGuid;
    public Guid RoleGuid { get; set; } = Guid.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string RoleDescription { get; set; } = string.Empty;
  }

  public class SiaaUserRoleUxm
  {
    public Guid AppGuid { get; set; } = PdpSiteSettings.GetValues.AppSecureUiaaGuid;
    public Guid UserGuid { get; set; } = Guid.Empty;
    public string UserName { get; set; } = string.Empty;
    public Guid RoleGuid { get; set; } = Guid.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string RoleDescription { get; set; } = string.Empty;
  }

}
