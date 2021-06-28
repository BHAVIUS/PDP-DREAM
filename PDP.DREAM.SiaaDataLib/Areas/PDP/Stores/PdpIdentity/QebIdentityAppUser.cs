using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Stores.PdpIdentity
{
  public partial class QebIdentityAppUser
  {
    partial void OnCreated()
    {
      this.AppGuidRef = PdpGuid.ParseToNonNullable(this.AppGuidRef, PdpSiteSettings.GetValues.AppSecureUiaaGuid);
    }

    public QebIdentityAppUser(IUserRegister m) : this()
    {
      RegisterProfileModel(m);
    }

    public void RegisterProfileModel(IUserRegister m)
    {
      this.AppGuidRef = PdpSiteSettings.GetValues.AppSecureUiaaGuid;
      this.UserGuidKey = Guid.NewGuid();
      this.UserName = m.UserName;
      if (string.IsNullOrEmpty(m.UserNameDisplayed)) { this.UserNameDisplayed = m.UserName; }
      else { this.UserNameDisplayed = m.UserNameDisplayed; }
      this.FirstName = m.FirstName;
      this.LastName = m.LastName;
      this.PhoneNumber = m.PhoneNumber;
      this.EmailAddress = m.EmailAddress;
      if (string.IsNullOrEmpty(m.EmailAlternate)) { this.EmailAlternate = m.EmailAddress; }
      else { this.EmailAlternate = m.EmailAlternate; }
      this.WebsiteAddress = m.WebsiteAddress;
      this.Organization = m.Organization;
      this.SecurityQuestion = m.SecurityQuestion;
      this.SecurityAnswer = m.SecurityAnswer;
      this.SecurityStamp = Guid.NewGuid().ToString();
      this.SecurityToken = PdpCryptoService.GenerateToken();
      this.PasswordHash = PdpCryptoService.HashToken(m.PassWord);
      this.DateUserCreated = DateTime.UtcNow;
      this.DateTokenExpired = DateTime.UtcNow.AddHours(24);
      // TODO: recode to check options on startup for confirmation requirements
      // else initialize true to simplify and handle case when email confirmation not required
      this.UserIsApproved = false;
    }

    public ChangeProfileUxm GetChangeProfileModel()
    {
      var m = new ChangeProfileUxm();
      m.DateLastEdit = this.DateLastEdit;
      m.DateProfileChanged = this.DateProfileChanged;
      m.UserNameDisplayed = this.UserNameDisplayed;
      m.FirstName = this.FirstName;
      m.LastName = this.LastName;
      m.Organization = this.Organization;
      m.PhoneNumber = this.PhoneNumber;
      m.SecurityAnswer = this.SecurityAnswer;
      m.SecurityQuestion = this.SecurityQuestion;
      m.WebsiteAddress = this.WebsiteAddress;
      return m;
    }

    public void SetChangeProfileModel(IUserProfileEdit m)
    {
      this.DateLastEdit = DateTime.UtcNow;
      this.DateProfileChanged = DateTime.UtcNow;
      this.UserNameDisplayed = m.UserNameDisplayed;
      this.FirstName = m.FirstName;
      this.LastName = m.LastName;
      this.Organization = m.Organization;
      this.PhoneNumber = m.PhoneNumber;
      this.SecurityAnswer = m.SecurityAnswer;
      this.SecurityQuestion = m.SecurityQuestion;
      this.WebsiteAddress = m.WebsiteAddress;
    }

  } // class

  public static partial class QebIdentityOperators
  {
    //public static SiaaUserUxm ToEditable(this QebIdentityAppUser rec)
    //{
    //  var row = new SiaaUserUxm()
    //  {
    //    AppGuid = rec.AppGuidRef,
    //    UserGuid = rec.UserGuidKey,
    //    UserNameDisplayed = (rec.UserNameDisplayed ?? string.Empty),
    //    FirstName = (rec.FirstName ?? string.Empty),
    //    LastName = (rec.LastName ?? string.Empty),
    //    UserName = rec.UserName,
    //    EmailAddress = rec.EmailAddress,
    //    UserIsApproved = rec.UserIsApproved
    //  };
    //  return row;
    //}

    public static IEnumerable<SiaaUserUxm> ToEditable(this IQueryable<QebIdentityAppUser> query)
    {
      IEnumerable<SiaaUserUxm>? rows = query
        .Include(rec => rec.QebIdentityAppUserRoles)
        .AsEnumerable().Select(rec =>
        {
          var uxmRoles = rec.QebIdentityAppUserRoles
          .OrderBy(p => p.RoleName).Select(p => p.RoleName).ToList();
          var uxmUser = new SiaaUserUxm(rec.AppGuidRef, rec.UserGuidKey,
            rec.FirstName, rec.LastName, rec.UserName, rec.EmailAddress, rec.UserIsApproved, uxmRoles);
          return uxmUser;
        }).ToList();
      return rows;
    }

  } // class

  public partial class QebIdentityContext
  {
    public IQueryable<QebIdentityAppUser> QueryStorableAppUsers()
    {
      IQueryable<QebIdentityAppUser> query = this.QebIdentityAppUsers
        .Where(u => (u.AppGuidRef == PdpSiteSettings.GetValues.AppSecureUiaaGuid))
        .OrderBy(r => r.UserName);
      return query;
    }
    public IEnumerable<QebIdentityAppUser> ListStorableAppUsers()
    {
      IEnumerable<QebIdentityAppUser> result;
      try
      {
        result = this.QueryStorableAppUsers().ToList();
      }
      catch
      {
        result = Enumerable.Empty<QebIdentityAppUser>();
      }
      return result;
    }

    public IEnumerable<SiaaUserUxm> ListEditableAppUsers()
    {
      IEnumerable<SiaaUserUxm> result;
      try
      {
        result = this.QueryStorableAppUsers().ToEditable();
      }
      catch
      {
        result = Enumerable.Empty<SiaaUserUxm>();
      }
      return result;
    }

    public RegisterUserUxm RegisterSiaaUser(RegisterUserUxm editObj)
    {
      if (editObj.AppGuidRef.IsEmpty()) { editObj.AppGuidRef = PdpSiteSettings.GetValues.AppSecureUiaaGuid; }
      if (editObj.UserGuidKey.IsEmpty()) { editObj.UserGuidKey = Guid.NewGuid(); }
      var qebUser = new QebIdentityAppUser(editObj);
      var errorCode = QebIdentityAppUserRegister(qebUser.AppGuidRef, qebUser.UserGuidKey,
        qebUser.UserName, qebUser.UserNameDisplayed, qebUser.FirstName, qebUser.LastName, qebUser.PhoneNumber,
        qebUser.EmailAddress, qebUser.EmailAlternate, qebUser.WebsiteAddress, qebUser.Organization,
        qebUser.SecurityQuestion, qebUser.SecurityAnswer, qebUser.SecurityStamp, qebUser.SecurityToken,
        qebUser.PasswordHash, qebUser.DateUserCreated, qebUser.DateTokenExpired);
      if (errorCode < 0) { editObj.Message = $"Error code = {errorCode} while registering user with index {editObj.UserName}"; }
      return editObj;
    }

    public SiaaUserUxm EditSiaaUser(SiaaUserUxm editObj)
    {
      if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PdpSiteSettings.GetValues.AppSecureUiaaGuid; }
      if (editObj.UserGuid.IsEmpty()) { editObj.UserGuid = Guid.NewGuid(); }

      if (!string.IsNullOrEmpty(editObj.UserRoleNames))
      {
        // database context roles
        var dbcRoles = GetAppUserRolesForUserGuid(editObj.UserGuid);
        // user interface model roles
        var uxmRoles = editObj.SplitUserRoles();
        // roles not deleted, roles only added if do not exist in database context
        foreach (var roleName in uxmRoles)
        {
          if (!dbcRoles.Contains(roleName))
          {
            var linkGuid = Guid.NewGuid();
            var roleGuid = GetAppRoleGuidByRoleName(roleName);
            if (roleGuid != null)
            {
              QebIdentityAppLinkEdit(linkGuid, editObj.UserGuid, roleGuid, editObj.AppGuid);
            }
          }
        }
      }

      var errorCode = QebIdentityAppUserEdit(editObj.AppGuid, editObj.UserGuid,
        editObj.FirstName, editObj.LastName, editObj.UserName, editObj.EmailAddress);
      return editObj;
    }

    public SiaaUserUxm DeleteSiaaUser(SiaaUserUxm editObj)
    {
      if (editObj.AppGuid.IsEmpty()) { editObj.AppGuid = PdpSiteSettings.GetValues.AppSecureUiaaGuid; }
      var errorCode = QebIdentityAppUserDelete(editObj.AppGuid, editObj.UserGuid);
      return editObj;
    }

  } // class

} // namespace
