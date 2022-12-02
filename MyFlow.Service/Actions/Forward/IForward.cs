using MyFlow.Domain.Models;

namespace MyFlow.Service.Actions.Forward
{
    public interface IForward : IAction
    {
        Task<IList<StageVM>> FindNextStages(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData);

        Task<string> FindStageTarget(StageVM stage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM? approveData);

        Task<DateTime?> GetStageDeadline(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData);

        Task<ApproveDataVM> InitNextStageData(FlowchartVM flowchart, StageVM currentStage, StageVM nextStage, ApplyDataVM applyData, ApproveDataVM? approveData = null);

        Task NextAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData);

    }
}
