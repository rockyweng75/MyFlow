using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Switch : IDataModel
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? OrderId { get; set; }
        public int? StageId { get; set; }
        public int? NextStageId { get; set; }
        public int? ActionType { get; set; }
        public string? ActionClass { get; set; }
        public string? DecisionClass { get; set; }
    }
}
