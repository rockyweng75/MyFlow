using MyFlow.Domain.Models;

namespace MyFlow.Service.Deadlines
{
    public interface IDeadline
    {
        string Name { get; }

        Task<DateTime?> GetStartDateTime(
              int? year,
              FlowchartVM flowchart,
              StageVM stage,
              ApplyDataVM? applyData,
              ApproveDataVM? approveData);

        Task<DateTime?> GetEndDateTime(
              int? year,
              FlowchartVM flowchart,
              StageVM stage,
              ApplyDataVM? applyData,
              ApproveDataVM? approveData);
    }
}
