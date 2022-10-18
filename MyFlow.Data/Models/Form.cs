using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Form : IDataModel
    {
        public int Id { get; set; }
        public string? FormType { get; set; }
        public int? FormName { get; set; }
        public int? Close { get; set; }
    }
}
