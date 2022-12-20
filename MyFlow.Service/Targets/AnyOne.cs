using MyFlow.Domain.Models;

namespace MyFlow.Service.Targets
{
    public class AnyOne : ITarget
    {
        public string Name => "任何人";

        public Task<string> Invoke(StageVM stage, ApplyDataVM? applyData, ApproveDataVM? approveDataVM)
        {
            return Task.FromResult("*");
        }
    }
}
