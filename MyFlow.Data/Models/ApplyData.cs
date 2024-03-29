﻿namespace MyFlow.Data.Models
{
    public partial class ApplyData : IDataModel
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public string? FlowName { get; set; }
        public int? StatusCode { get; set; }
        public int? ApplyYear { get; set; }
        public string? ApplyUser { get; set; }
        public string? ApplyName { get; set; }
        public string? DeptCode { get; set; }
        public string? DeptName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? Tag { get; set; }
        public string? Title { get; set; }
        public string? FormData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
