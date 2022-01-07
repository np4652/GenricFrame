using GenricFrame.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace GenricFrame.AppCode.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomeAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            var header = ReadHeader(context);
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        private HeaderInfo ReadHeader(AuthorizationFilterContext context)
        {
            var headerInfo = new HeaderInfo();
            try
            {
                context.HttpContext.Request.Headers.TryGetValue("Authorization", out var Token);
                //context.HttpContext.Request.Headers.TryGetValue(nameof(headerInfo.Version), out var Version);
                //context.HttpContext.Request.Headers.TryGetValue(nameof(headerInfo.UserID), out var UserId);
                //context.HttpContext.Request.Headers.TryGetValue(nameof(headerInfo.Token), out var Token);
                //context.HttpContext.Request.Headers.TryGetValue(nameof(headerInfo.Domain), out var Domain);
                //headerInfo.AppID = AppId.Count > 0 ? AppId[0] : string.Empty;
                //headerInfo.Version = Version.Count > 0 ? Version[0] : string.Empty;
                //headerInfo.UserID = UserId.Count > 0 ? (Validate.O.IsNumeric(UserId[0] ?? string.Empty) ? Convert.ToInt32(UserId[0]) : 0) : 0;
                headerInfo.Token = Token.Count > 0 ? (Validate.O.IsNumeric(Token[0] ?? string.Empty) ? Convert.ToInt32(Token[0]) : 0) : 0;
                //headerInfo.Domain = Domain.Count > 0 ? Domain[0] : string.Empty;
            }
            catch (Exception)
            {
            }
            return headerInfo;
        }
    }
}