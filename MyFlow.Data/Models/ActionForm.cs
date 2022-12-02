using System;
using System.Collections.Generic;

namespace MyFlow.Data.Models
{
    public partial class ActionForm : IDataModel
    {
        public int Id { get; set; }
        public int? StageId { get; set; }
        public int? OrderId { get; set; }
        public int? FormId { get; set; }
        public int? ActionType { get; set; }
        public string? ActionName { get; set; }
        public string? ButtonName { get; set; }
    }
}
