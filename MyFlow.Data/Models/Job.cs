using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Job : IDataModel
    {
        public int Id { get; set; }
        public int? StageId { get; set; }
        public int? OrderId { get; set; }
        public int? JobType { get; set; }
        public string? JobClass { get; set; }
        public string? JobParam { get; set; }
    }
}
