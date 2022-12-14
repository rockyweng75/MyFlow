using MyFlow.Domain.Models;

namespace MyFlow.Service.Actions
{
    public interface IAction
    {
        string Name { get; }
        Task Invoke(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData);
    }
}
