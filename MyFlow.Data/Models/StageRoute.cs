using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class StageRoute : IDataModel
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? StageId { get; set; }
        public int? NextStageId { get; set; }
        public int? ActionType { get; set; }
        public string? RouteName { get; set; }
        public string? SwitchClass { get; set; }
    }
}
