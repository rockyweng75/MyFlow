using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class JobVM : IViewModel
    {
        public int Id { get; set; }
        public int? StageId { get; set; }
        public int? OrderId { get; set; }
        public int? JobType { get; set; }
        public string? JobClass { get; set; }
        public string? JobParam { get; set; }
    }
}
