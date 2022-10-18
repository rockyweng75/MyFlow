using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Stage : IDataModel
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? OrderId { get; set; }
        public string? StageName { get; set; }
        public string? StageEname { get; set; }
        public string? Deadline { get; set; }
        public string? Target { get; set; }
        public string? TargetParams { get; set; }
    }
}
