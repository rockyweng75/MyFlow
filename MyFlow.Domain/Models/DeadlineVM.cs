using MyFlow.Domain.Models.Basic;
using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class DeadlineVM : PaginationVM
    {
        public int Id { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? DeadlineClass { get; set; }
        public string? DeadlineRemark { get; set; }
    }
}
