using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class RouteHistory : IDataModel
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? SourceStageId { get; set; }
        public int? NextStageId { get; set; }
        public int? ActionType { get; set; }
    }
}
