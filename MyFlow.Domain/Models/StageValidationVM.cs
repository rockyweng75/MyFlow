using System;
using System.Collections.Generic;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class StageValidationVM : PaginationVM
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? StageId { get; set; }
        public int? OrderId { get; set; }
        public string? ValidateName { get; set; }
        public string? ValidateClass { get; set; }
    }
}
