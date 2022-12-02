using MyFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
