using MyFlow.Domain.Models;

namespace MyFlow.Service.Targets
{
    public class PersonnelManagerTarget : ITarget
    {
        public string Name => "人事主管";

        public Task<string> Invoke(StageVM stage, ApplyDataVM applyData, ApproveDataVM? approveDataVM)
        {
            return Task.FromResult("PA");
        }
    }
}
