using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Stores.PdpIdentity
{
  public partial class QebIdentityAppUserRole
  {
    partial void OnCreated()
    {
      this.AppGuidRef = PdpGuid.ParseToNonNullable(this.AppGuidRef, PdpSiteSettings.GetValues.AppSecureUiaaGuid);
    }

  } // class

  public static partial class QebIdentityOperators
  {
    //public static SiaaUserRoleUxm ToEditable(this QebIdentityAppUserRole rec)
    //{
    //  var row = new SiaaUserRoleUxm()
    //  {
    //    AppGuid = rec.AppGuidRef,
    //    UserGuid = rec.UserGuidRef,
    //    UserName = rec.UserName,
    //    RoleGuid = rec.RoleGuidRef,
    //    RoleName = rec.RoleName,
    //    RoleDescription = rec.RoleDescription
    //  };
    //  return row;
    //}

    public static IQueryable<SiaaUserRoleUxm> ToEditable(this IQueryable<QebIdentityAppUserRole> query)
    {
      IQueryable<SiaaUserRoleUxm> rows =
        from rec in query
        select new SiaaUserRoleUxm
        {
          AppGuid = rec.AppGuidRef,
          UserGuid = rec.UserGuidRef,
          UserName = rec.UserName,
          RoleGuid = rec.RoleGuidRef,
          RoleName = rec.RoleName,
          RoleDescription = rec.RoleDescription
        };
      return rows;
    }

  } // class

  public partial class QebIdentityContext
  {
    public IEnumerable<QebIdentityAppUserRole> ListStorableAppUserRoles(Guid userGuid)
    {
      IEnumerable<QebIdentityAppUserRole> result;
      try
      {
        IQueryable<QebIdentityAppUserRole> query = this.QebIdentityAppUserRoles
          .Where(r =>
          (r.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
          (r.UserGuidRef == userGuid));
        result = query.OrderBy(r => r.RoleName).ToList();
      }
      catch
      {
        result = Enumerable.Empty<QebIdentityAppUserRole>();
      }
      return result;
    }

    public IEnumerable<SiaaUserRoleUxm> ListEditableAppUserRoles(Guid userGuid)
    {
      IEnumerable<SiaaUserRoleUxm> result;
      try
      {
        IQueryable<SiaaUserRoleUxm> query = this.QebIdentityAppUserRoles
          .Where(r =>
          (r.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
          (r.UserGuidRef == userGuid)).ToEditable();
        result = query.OrderBy(r => r.RoleName).ToList();
      }
      catch
      {
        result = Enumerable.Empty<SiaaUserRoleUxm>();
      }
      return result;
    }

    public List<string>? GetAppUserRolesForUserGuid(Guid userGuid)
    {
      List<string>? userRoles = this.QebIdentityAppUserRoles
        .Where(r =>
        (r.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
        (r.UserGuidRef == userGuid)).Select(n => n.RoleName).ToList();
      return userRoles;
    }

  } // class

} // namespace
