using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Stage 
    {
        public virtual IList<ActionForm>? ActionFormList { get; set; }

        public virtual IList<StageRoute>? StageRouteList { get; set; }

        public virtual IList<StageValidation>? StageValidationList { get; set; }

        public virtual IList<StageJob>? StageJobList { get; set; }
    }
}
