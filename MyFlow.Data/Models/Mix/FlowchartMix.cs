using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class FlowchartMix : Stage
    {
        public IList<StageMix>? StageList { get; set; }

    }
}
