using System;
using System.Collections.Generic;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class SwitchVM : PaginationVM
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? OrderId { get; set; }
        public int? StageId { get; set; }
        public int? NextStageId { get; set; }
        public int? ActionType { get; set; }
        public string? ActionClass { get; set; }
        //if
        public string? DecisionClass { get; set; }
    }
}
