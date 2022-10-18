using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class TestVM : IViewModel
    {
        public int Id { get; set; }
        public string Column1 { get; set; }
        public int? Column2 { get; set; }
        public DateTime? Column3 { get; set; }
    }
}
