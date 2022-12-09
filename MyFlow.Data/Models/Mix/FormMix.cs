using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Form
    {
        public virtual IList<FormItem>? FormItemList { get; set; }
    }
}
