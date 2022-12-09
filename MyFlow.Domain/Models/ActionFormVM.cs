using MyFlow.Domain.Enums;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class ActionFormVM : PaginationVM, IViewModel
    {
        public int Id { get; set; }
        public int? StageId { get; set; }
        public int? OrderId { get; set; }
        public int? FormId { get; set; }
        public int? ActionType { get; set; }
        public string? ActionName { get; set; }
        public string? ActionClass { get; set; }
        public string? ButtonName { get; set; }

        public IList<ActionJobVM>? ActionJobList { get; set; }
    }
}
