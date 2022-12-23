using MyFlow.Domain.Models;

namespace MyFlow.Service.Targets
{
    public interface ITarget
    {
        string Name { get; }
        Task<string> Invoke(StageVM stage, ApplyDataVM? applyData, ApproveDataVM? approveDataVM);
    }
}
