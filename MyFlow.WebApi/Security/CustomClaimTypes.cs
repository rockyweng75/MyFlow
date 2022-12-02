using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Security
{
    public static class CustomClaimTypes
    {
        public const string UserName = "UserName";
        public const string JobTitle = "jobTitle";
        public const string DeptCode = "DeptCode";
        public const string DeptName = "DeptName";
    }
}