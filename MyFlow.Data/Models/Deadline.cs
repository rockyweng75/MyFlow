using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class Deadline : IDataModel
    {
        public int Id { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? DeadlineClass { get; set; }
    }
}
