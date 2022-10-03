using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace PDP.DREAM.CoreDataLib.Migrations;

public partial class CreateQebIdentityApp : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.EnsureSchema(
        name: "dbo");

    migrationBuilder.CreateTable(
        name: "QebIdentityApp",
        schema: "dbo",
        columns: table => new {
          AppGuidKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          AppName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          AppDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
          ConcurrencyStamp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
        },
        constraints: table => {
          table.PrimaryKey("PK_QebIdentityApp", x => x.AppGuidKey);
        });

    migrationBuilder.CreateTable(
        name: "QebIdentityAppUserRoleLink",
        schema: "dbo",
        columns: table => new {
          LinkGuidKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          UserGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          RoleGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          AppGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ConcurrencyStamp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
        },
        constraints: table => {
          table.PrimaryKey("PK_QebIdentityAppUserRoleLink", x => x.LinkGuidKey);
        });

    migrationBuilder.CreateTable(
        name: "QebIdentityAppRole",
        schema: "dbo",
        columns: table => new {
          RoleGuidKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          AppGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          RoleName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          RoleDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
        },
        constraints: table => {
          table.PrimaryKey("PK_QebIdentityAppRole", x => x.RoleGuidKey);
          table.ForeignKey(
                      name: "FK_QebIdentityAppRole_QebIdentityApp_AppGuidRef",
                      column: x => x.AppGuidRef,
                      principalSchema: "dbo",
                      principalTable: "QebIdentityApp",
                      principalColumn: "AppGuidKey",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "QebIdentityAppUser",
        schema: "dbo",
        columns: table => new {
          UserGuidKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          SessionGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
          AppGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          AccessFailedCount = table.Column<int>(type: "int", precision: 10, scale: 0, nullable: false),
          ConcurrencyStamp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
          DateEmailConfirmed = table.Column<DateTime>(type: "datetime2", nullable: true),
          DateLastEdit = table.Column<DateTime>(type: "datetime2", nullable: true),
          DateLastLockout = table.Column<DateTime>(type: "datetime2", nullable: true),
          DateLastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
          DatePasswordChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
          DateProfileChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
          DateTokenExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
          DateUserCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
          DateUserNameChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
          EmailAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          EmailAlternate = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
          FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
          LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
          LockoutEndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
          Organization = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
          PasswordHash = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
          PhoneNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
          SecurityAnswer = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          SecurityQuestion = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          SecurityStamp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          SecurityToken = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
          UserIsApproved = table.Column<bool>(type: "bit", nullable: false),
          UserName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          UserNameDisplayed = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          WebsiteAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
        },
        constraints: table => {
          table.PrimaryKey("PK_QebIdentityAppUser", x => x.UserGuidKey);
          table.ForeignKey(
                      name: "FK_QebIdentityAppUser_QebIdentityApp_AppGuidRef",
                      column: x => x.AppGuidRef,
                      principalSchema: "dbo",
                      principalTable: "QebIdentityApp",
                      principalColumn: "AppGuidKey",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "QebIdentityAppUserRole",
        schema: "dbo",
        columns: table => new {
          LinkGuidKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          UserGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          UserName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          UserNameDisplayed = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          RoleGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          RoleName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          RoleDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
          AppGuidRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          AppName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
          AppDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
          ConcurrencyStamp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
        },
        constraints: table => {
          table.PrimaryKey("PK_QebIdentityAppUserRole", x => x.LinkGuidKey);
          table.ForeignKey(
                      name: "FK_QebIdentityAppUserRole_QebIdentityApp_AppGuidRef",
                      column: x => x.AppGuidRef,
                      principalSchema: "dbo",
                      principalTable: "QebIdentityApp",
                      principalColumn: "AppGuidKey",
                      onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                      name: "FK_QebIdentityAppUserRole_QebIdentityAppRole_RoleGuidRef",
                      column: x => x.RoleGuidRef,
                      principalSchema: "dbo",
                      principalTable: "QebIdentityAppRole",
                      principalColumn: "RoleGuidKey",
                      onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                      name: "FK_QebIdentityAppUserRole_QebIdentityAppUser_UserGuidRef",
                      column: x => x.UserGuidRef,
                      principalSchema: "dbo",
                      principalTable: "QebIdentityAppUser",
                      principalColumn: "UserGuidKey",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_QebIdentityAppRole_AppGuidRef",
        schema: "dbo",
        table: "QebIdentityAppRole",
        column: "AppGuidRef");

    migrationBuilder.CreateIndex(
        name: "IX_QebIdentityAppUser_AppGuidRef",
        schema: "dbo",
        table: "QebIdentityAppUser",
        column: "AppGuidRef");

    migrationBuilder.CreateIndex(
        name: "IX_QebIdentityAppUserRole_AppGuidRef",
        schema: "dbo",
        table: "QebIdentityAppUserRole",
        column: "AppGuidRef");

    migrationBuilder.CreateIndex(
        name: "IX_QebIdentityAppUserRole_RoleGuidRef",
        schema: "dbo",
        table: "QebIdentityAppUserRole",
        column: "RoleGuidRef");

    migrationBuilder.CreateIndex(
        name: "IX_QebIdentityAppUserRole_UserGuidRef",
        schema: "dbo",
        table: "QebIdentityAppUserRole",
        column: "UserGuidRef");
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "QebIdentityAppUserRole",
        schema: "dbo");

    migrationBuilder.DropTable(
        name: "QebIdentityAppUserRoleLink",
        schema: "dbo");

    migrationBuilder.DropTable(
        name: "QebIdentityAppRole",
        schema: "dbo");

    migrationBuilder.DropTable(
        name: "QebIdentityAppUser",
        schema: "dbo");

    migrationBuilder.DropTable(
        name: "QebIdentityApp",
        schema: "dbo");
  }
}
