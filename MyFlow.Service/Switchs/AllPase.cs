using MyFlow.Domain.Models;

namespace MyFlow.Service.Switchs
{
    public class AllPase : ISwitch
    {
        public string Name => "全開放"; 
        public Task<bool> Invoke(FlowchartVM flowchart, StageVM stage, ApplyDataVM applyData, ApproveDataVM? approveDataVM){

            return Task.FromResult(true);
        }
    }
}
