using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFlow.Data.Models
{
    public partial class StageJob : IDataModel
    {
        [Key]
        public int Id { get; set; }
        public int? StageId { get; set; }
        public int? OrderId { get; set; }
        public int? JobType { get; set; }
        public string? JobClass { get; set; }
        public string? JobParam { get; set; }
    }
}
