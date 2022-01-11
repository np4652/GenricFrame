using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenricFrame.AppCode.Migrations
{
    [Migration(202106280001)]
    public class InitialTables_202106280001 : Migration
    {
        public override void Down()
        {
            Delete.Table("Employees");
            Delete.Table("Companies");
        }

        public override void Up()
        {
            /* Role */
            Create.Table("IdentityRole")
               .WithColumn("Id").AsInt64().NotNullable().Identity().PrimaryKey()
               .WithColumn("ConcurrencyStamp").AsString(1000).NotNullable()
               .WithColumn("Name").AsString(256).NotNullable()
               .WithColumn("NormalizedName").AsString(256).NotNullable()
               .WithColumn("Country").AsString(50).NotNullable();
            /* Role Claim */
            Create.Table("RoleClaims")
               .WithColumn("Id").AsInt64().NotNullable().Identity()
               .WithColumn("ClaimType").AsString(1000).NotNullable()
               .WithColumn("ClaimValue").AsString(1000).NotNullable()
               .WithColumn("RoleId").AsInt64().NotNullable()
               .WithColumn("Country").AsString(50).NotNullable();

            /* Users */
            Create.Table("Users")
               .WithColumn("Id").AsString(1000).NotNullable()
               .WithColumn("AccessFailedCount").AsInt64()
               .WithColumn("ConcurrencyStamp").AsString(1000)
               .WithColumn("Email").AsString(256).NotNullable()
               .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
               .WithColumn("LockoutEnabled").AsBoolean()
               .WithColumn("LockoutEnd").AsDateTime()
               .WithColumn("NormalizedEmail").AsString(256).NotNullable()
               .WithColumn("NormalizedUserName").AsString(256).NotNullable()
               .WithColumn("PasswordHash").AsString(256).NotNullable()
               .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
               .WithColumn("SecurityStamp").AsString(1000).NotNullable()
               .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
               .WithColumn("UserName").AsString(256).NotNullable();

            /* UserClaims */
            Create.Table("UserClaims")
               .WithColumn("Id").AsInt64().Identity().NotNullable()
               .WithColumn("ClaimType").AsString(1000)
               .WithColumn("ClaimValue").AsString(1000)
               .WithColumn("UserId").AsString(256).NotNullable();

            /* UserLogins */
            Create.Table("UserLogins")
               .WithColumn("Id").AsInt64().Identity().NotNullable()
               .WithColumn("LoginProvider").AsString(128)
               .WithColumn("ProviderKey").AsString(128)
               .WithColumn("ProviderDisplayName").AsString(1000)
               .WithColumn("UserId").AsString(256).NotNullable();

            /* UserTokens */
            Create.Table("UserTokens")
               .WithColumn("Id").AsInt64().Identity().NotNullable()
               .WithColumn("UserId").AsString(256).NotNullable()
               .WithColumn("LoginProvider").AsString(128).NotNullable()
               .WithColumn("Name").AsString(128).NotNullable()
               .WithColumn("Value").AsString(128).NotNullable();

            /* UserRoles */
            Create.Table("UserRoles")
               .WithColumn("Id").AsInt64().Identity().NotNullable()
               .WithColumn("UserId").AsString(256).NotNullable()
               .WithColumn("RoleId").AsString(128).NotNullable();


            Create.Table("Companies")
                .WithColumn("Id").AsInt64().NotNullable().Identity()
                .WithColumn("CompanyId").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Address").AsString(60).NotNullable()
                .WithColumn("Country").AsString(50).NotNullable();
            Create.Table("Employees")
                .WithColumn("Id").AsInt64().NotNullable().Identity()
                .WithColumn("EmployeeId").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Age").AsInt32().NotNullable()
                .WithColumn("Position").AsString(50).NotNullable()
                .WithColumn("CompanyId").AsGuid().NotNullable().ForeignKey("Companies", "CompanyId");
        }
    }
}
