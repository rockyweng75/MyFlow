using System;
using System.Collections.Generic;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class StageRouteVM : PaginationVM
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
