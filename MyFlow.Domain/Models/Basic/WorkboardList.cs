using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models.Basic
{  
    [Serializable]
    public class WorkboardList 
    {
        public int FlowId { get; set; }

        public int ApplyId { get; set; }

        public int? DataId { get; set; }

        public string? FlowName { get; set; }

        public string? StageName { get; set; }

        public string? ApplyUser { get; set; }

        public string? Title { get; set; }

        public DateTime? ApplyDate { get; set; }

        public DateTime? Deadline { get; set; }

        public string? StageStatus { get; set; }

        public string? ApplyStatus { get; set; }

        public DateTime? SubmitDate { get; set; }

        public int? CurrentStage { get; set; }
        public string? DeptName { get; set; }

    }
}