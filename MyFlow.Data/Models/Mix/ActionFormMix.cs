using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class ActionForm
    {
        public virtual IList<ActionJob>? ActionJobList { get; set; }
    }
}
