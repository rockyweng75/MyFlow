using MyFlow.Domain.Models;

namespace MyFlow.Service.Switchs
{
    public interface ISwitch
    {
        string Name { get; }
        Task<bool> Invoke(FlowchartVM flowchart, StageVM stage, ApplyDataVM applyData, ApproveDataVM? approveDataVM);
    }
}
