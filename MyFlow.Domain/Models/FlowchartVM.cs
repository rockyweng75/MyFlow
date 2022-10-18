using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class FlowchartVM : IViewModel
    {
        public int Id { get; set; }
        public string? FlowName { get; set; }
        public string? FlowEname { get; set; }
        public int? FlowType { get; set; }
        public string? AdminUser { get; set; }
        public string? Target { get; set; }
        public int? Close { get; set; }
        public string? TagFormat { get; set; }
        public string? TitleFormat { get; set; }
    }
}
