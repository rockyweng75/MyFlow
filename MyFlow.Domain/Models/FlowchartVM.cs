using MyFlow.Domain.Models.Basic;
using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class FlowchartVM : PaginationVM
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

        public IList<StageVM>? StageList { get; set; }

        public IList<StageRouteVM>? StageRouteList { get; set; }

        public IList<ActionJobVM>? ActionJobList { get; set; }

        public IList<StageJobVM>? StageJobList { get; set; }

        public IList<ActionFormVM>? ActionFormList { get; set; }

    }
}
