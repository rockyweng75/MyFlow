using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Security
{
    public static class AuthorizeExtensions
    {
        public static UserInfoVM ToUserInfo(this ClaimsPrincipal User)
        {
            var userId = User.Claims.Where(o => o.Type == ClaimTypes.NameIdentifier)
                .First()
                .Value;

            string? userName = null;

            if( User.Claims.Any(o => o.Type == CustomClaimTypes.UserName))
            {
                userName = User.Claims
                    .Where(o => o.Type == CustomClaimTypes.UserName)
                    .First()
                    .Value;
            }

            string? deptCode = null;

            if( User.Claims.Any(o => o.Type == CustomClaimTypes.DeptCode))
            {
                deptCode = User.Claims
                    .Where(o => o.Type == CustomClaimTypes.DeptCode)
                    .First()
                    .Value;
            }

            string? deptName = null;

            if( User.Claims.Any(o => o.Type == CustomClaimTypes.DeptName))
            {
                deptName = User.Claims
                    .Where(o => o.Type == CustomClaimTypes.DeptName)
                    .First()
                    .Value;
            }

            var user = new UserInfoVM() {
                UserId = userId,
                UserName = userName,
                DeptCode = deptCode,
                DeptName = deptName,
            };
            
            return user;
        }
    }
}
