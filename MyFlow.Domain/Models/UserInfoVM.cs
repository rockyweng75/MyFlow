using System;
using System.Collections.Generic;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public class UserInfoVM : PaginationVM
    {
        public int? Id { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? DeptCode { get; set; }

        public string? DeptName { get; set; }

        public IList<string?> Roles { get; set; } = new List<string?>();

    }
}
