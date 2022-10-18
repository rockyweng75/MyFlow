using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class FormVM : IViewModel
    {
        public int Id { get; set; }
        public string? FormType { get; set; }
        public int? FormName { get; set; }
        public int? Close { get; set; }
    }
}
