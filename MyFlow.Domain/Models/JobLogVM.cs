using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class JobLogVM : IViewModel
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? StageId { get; set; }
        public int? JobId { get; set; }
        public int? ApplyId { get; set; }
        public int? ApprId { get; set; }
        public int? Success { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ReexecuteUser { get; set; }
        public DateTime? ReexecuteDate { get; set; }
        public string? FlowName { get; set; }
        public string? StageName { get; set; }
        public string? JobClass { get; set; }
        public string? ApplyUser { get; set; }
        public string? ApproveUser { get; set; }
    }
}
