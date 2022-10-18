using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class ApproveData : IDataModel
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? StageId { get; set; }
        public int? ApplyId { get; set; }
        public string? FlowName { get; set; }
        public string? StageName { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? DeptCode { get; set; }
        public string? DeptName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? StatusCode { get; set; }
        public string? FormData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
