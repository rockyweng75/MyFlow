using MyFlow.Domain.Models.Basic;
using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class ApplyDataVM : PaginationVM
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? StatusCode { get; set; }
        public string? ApplyUser { get; set; }
        public string? ApplyName { get; set; }
        public string? DeptCode { get; set; }
        public string? DeptName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? Tag { get; set; }
        public string? Title { get; set; }
        public string? FormData { get; set; }
        public dynamic? DynamicFormData
        {
            get { return this.FormData != null ? Newtonsoft.Json.JsonConvert.DeserializeObject(this.FormData) : null; }
        }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
