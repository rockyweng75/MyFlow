using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class StageMix : Stage
    {
        public IList<ActionForm>? ActionFormList { get; set; }

        public IList<StageRoute>? StageRouteList { get; set; }

        public IList<StageValidation>? StageValidationList { get; set; }

        public IList<StageJob>? StageJobList { get; set; }

    }
}
