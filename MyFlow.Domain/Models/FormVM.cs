using System;
using System.Collections.Generic;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class FormVM : PaginationVM
    {
        public int Id { get; set; }
        public int? FormType { get; set; }
        public string? FormName { get; set; }
        public int? Close { get; set; }
    }
}
