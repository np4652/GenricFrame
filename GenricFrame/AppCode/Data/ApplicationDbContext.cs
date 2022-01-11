﻿using Dapper;
using GenricFrame.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GenricFrame.AppCode.Data
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _dbConnection;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public IQueryable<ApplicationRole> Roles()
        {
            return _dbConnection.Query<ApplicationRole>("select * from AspnetRoles", commandType: CommandType.Text).AsQueryable();
        }

        public IQueryable<AppicationUser> Users()
        {
            return _dbConnection.Query<AppicationUser>("select * from AspNetUsers", commandType: CommandType.Text).AsQueryable();
        }

        public Task<IdentityResult> CreateRole(ApplicationRole role)
        {
            if (_dbConnection.Execute("AddRole", role, commandType: CommandType.StoredProcedure) > 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed());
            }
        }

        public void AddToRoleAsync(int id, int roleid)
        {

            _dbConnection.Execute("insert into AspNetUserRoles values('" + id + "','" + roleid + "')", null, commandType: CommandType.Text);
        }

        public Task<ApplicationRole> FindRoleByIdAsync(string roleId)
        {
            var role = _dbConnection.Query<ApplicationRole>("select * from AspnetRoles where Id='" + roleId + "'", null, commandType: CommandType.Text).FirstOrDefault();
            return Task.FromResult(role);
        }

        public Task<ApplicationRole> FindRoleByNameAsync(string normalizedRoleName)
        {
            var role = _dbConnection.Query<ApplicationRole>("select * from AspnetRoles where NormalizedName='" + normalizedRoleName + "'", null, commandType: CommandType.Text).FirstOrDefault();
            return Task.FromResult(role);
        }

        public IdentityResult AddUser(AppicationUser user)
        {
            if (_dbConnection.Execute("AddUser", user, commandType: CommandType.StoredProcedure) > 0)
            {
                return IdentityResult.Success;
            }
            else
            {

                return IdentityResult.Failed();
            }
        }

        public string GetUserName(AppicationUser user)
        {
            return _dbConnection.Query<string>("select UserName from AspNetUsers where UserName='" + user.UserName + "'", null, commandType: CommandType.Text).FirstOrDefault();
        }

        public Task<AppicationUser> FindByEmailAsync(string normalizedEmail)
        {
            return Task.FromResult(_dbConnection.Query<AppicationUser>("select * from AspNetUsers where NormalizedEmail='" + normalizedEmail + "'", null, commandType: CommandType.Text).FirstOrDefault());
        }

        public Task<AppicationUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(_dbConnection.Query<AppicationUser>("select * from AspNetUsers where Id='" + userId + "'", null, commandType: CommandType.Text).FirstOrDefault());
        }

        public Task<AppicationUser> FindByNameAsync(string normalizedUserName)
        {
            return Task.FromResult(_dbConnection.Query<AppicationUser>("select * from AspNetUsers where NormalizedUserName='" + normalizedUserName + "'", null, commandType: CommandType.Text).FirstOrDefault());
        }

        public Task<List<string>> GetRolesAsync(AppicationUser user)
        {
            return Task.FromResult(_dbConnection.Query<string>("GetUserRoles", new { UserId = user.Id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public string GetNormalizedUserName(AppicationUser user)
        {
            return _dbConnection.Query<string>("select NormalizedUserName from AspNetUsers where Email='" + user.Email + "'", null, commandType: CommandType.Text).FirstOrDefault();
        }

        public Task<string> GetEmailAsync(string email)
        {
            return Task.FromResult(_dbConnection.Query<string>("select Email from AspNetUsers where UserName='" + email + "'", null, commandType: CommandType.Text).FirstOrDefault());
        }

        public string GetUserIdAsync(string userName)
        {
            return _dbConnection.Query<string>("select Id from AspNetUsers where UserName='" + userName + "'", null, commandType: CommandType.Text).FirstOrDefault();
        }

        public Task<bool> IsInRoleAsync(AppicationUser user, int roleId)
        {
            return Task.FromResult(_dbConnection.Query("select UserId from AspNetUserRoles where RoleId='" + roleId + "' and UserId='" + user.Id + "'", null, commandType: CommandType.Text).Any());
        }

        public Task<IdentityResult> UpdateUser(AppicationUser user)
        {
            if (_dbConnection.Execute("UpdateUser", user, commandType: CommandType.StoredProcedure) > 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {

                return Task.FromResult(IdentityResult.Failed());
            }
        }
    }
}