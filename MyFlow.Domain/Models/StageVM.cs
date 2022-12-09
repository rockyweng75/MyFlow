using MyFlow.Domain.Models.Basic;

namespace MyFlow.Domain.Models
{
    public partial class StageVM : PaginationVM
    {
        public int Id { get; set; }
        public int? FlowId { get; set; }
        public int? OrderId { get; set; }
        public string? StageName { get; set; }
        public string? StageEname { get; set; }
        public string? Deadline { get; set; }
        public string? Target { get; set; }
        public string? TargetParams { get; set; }

        public IList<ActionFormVM>? ActionFormList { get; set; }
        public IList<StageRouteVM>? StageRouteList { get; set; }
        public IList<StageValidationVM>? StageValidationList { get; set; }

    }
}
