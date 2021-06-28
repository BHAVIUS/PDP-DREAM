using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Stores.PdpIdentity
{
  public partial class QebIdentityAppRole
  {
    partial void OnCreated()
    {
      this.AppGuidRef = PdpGuid.ParseToNonNullable(this.AppGuidRef, PdpSiteSettings.GetValues.AppSecureUiaaGuid);
    }

  } // class

  public static partial class QebIdentityOperators
  {
    //public static SiaaRoleUxm ToEditable(this QebIdentityAppRole rec)
    //{
    //  var row = new SiaaRoleUxm()
    //  {
    //    AppGuid = rec.AppGuidRef,
    //    RoleGuid = rec.RoleGuidKey,
    //    RoleName = rec.RoleName,
    //    RoleDescription = rec.RoleDescription
    //  };
    //  return row;
    //}

    public static IQueryable<SiaaRoleUxm> ToEditable(this IQueryable<QebIdentityAppRole> query)
    {
      IQueryable<SiaaRoleUxm> rows =
        from rec in query
        select new SiaaRoleUxm
        {
          AppGuid = rec.AppGuidRef,
          RoleGuid = rec.RoleGuidKey,
          RoleName = rec.RoleName,
          RoleDescription = rec.RoleDescription
        };
      return rows;
    }

  } // class

  public partial class QebIdentityContext
  {
    public IEnumerable<QebIdentityAppRole> ListStorableAppRoles()
    {
      IEnumerable<QebIdentityAppRole> result;
      try
      {
        IQueryable<QebIdentityAppRole> query = this.QebIdentityAppRoles
          .Where(r => (r.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid));
        result = query.OrderBy(r => r.RoleName).ToList();
      }
      catch
      {
        result = Enumerable.Empty<QebIdentityAppRole>();
      }
      return result;
    }

    public IEnumerable<SiaaRoleUxm> ListEditableAppRoles()
    {
      IEnumerable<SiaaRoleUxm> result;
      try
      {
        IQueryable<SiaaRoleUxm> query = this.QebIdentityAppRoles
          .Where(r => (r.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid)).ToEditable();
        result = query.OrderBy(r => r.RoleName).ToList();
      }
      catch
      {
        result = Enumerable.Empty<SiaaRoleUxm>();
      }
      return result;
    }

    public Guid? GetAppRoleGuidByRoleName(string roleName)
    {
      Guid? roleGuid = this.QebIdentityAppRoles.Where(r =>
        (r.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid) &&
        (r.RoleName == roleName)).Select(n => n.RoleGuidKey).FirstOrDefault();
      return roleGuid;
    }

    public SiaaRoleUxm EditRole(SiaaRoleUxm editObj)
    {
      if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PdpSiteSettings.GetValues.AppSecureUiaaGuid; }
      if (editObj.RoleGuid.IsEmpty()) { editObj.RoleGuid = Guid.NewGuid(); }
      var errorCode = QebIdentityAppRoleEdit(editObj.AppGuid, editObj.RoleGuid,
        editObj.RoleName, editObj.RoleDescription);
      return editObj;
    }

    public SiaaRoleUxm DeleteRole(SiaaRoleUxm editObj)
    {
      if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PdpSiteSettings.GetValues.AppSecureUiaaGuid; }
      var errorCode = QebIdentityAppRoleDelete(editObj.AppGuid, editObj.RoleGuid);
      return editObj;
    }

  } // class

} // namespace
