﻿using MyFlow.Domain.Models.Basic;
using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class ApproveDataVM : PaginationVM
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
        public int? StatusCode { get; set; }
        public string? FormData { get; set; }
        public dynamic? DynamicFormData
        {
            get { return this.FormData != null ? Newtonsoft.Json.JsonConvert.DeserializeObject(this.FormData) : null; }
        }
        public DateTime? CreatedDate { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
