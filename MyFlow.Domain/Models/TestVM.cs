using System;
using System.Collections.Generic;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class TestVM : PaginationVM
    {
        public int Id { get; set; }
        public string? Column1 { get; set; }
        public int? Column2 { get; set; }
        public DateTime? Column3 { get; set; }
    }
}
