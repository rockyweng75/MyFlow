using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Flowchart 
    {
        public virtual IList<Stage>? StageList { get; set; }
    }
}
