﻿using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class ActionJobVM : PaginationVM, IJobVM
    {
        public int Id { get; set; }
        public int? ActionId { get; set; }
        public int? OrderId { get; set; }
        public int? JobType { get; set; }
        public string? JobClass { get; set; }
        public string? JobParam { get; set; }
    }
}
