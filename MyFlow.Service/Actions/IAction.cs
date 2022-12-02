using MyFlow.Domain.Models;

namespace MyFlow.Service.Actions
{
    public interface IAction
    {
        Task Invoke(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData);
    }
}
