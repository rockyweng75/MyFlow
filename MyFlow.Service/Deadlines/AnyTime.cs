using MyFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Service.Deadlines
{
    public class AnyTime : IDeadline
    {

        public string Name => "不限時間";

        public Task<DateTime?> Invoke(
              FlowchartVM flowchart,
              StageVM stage,
              ApplyDataVM applyData,
              ApproveDataVM? approveData){
            
            return Task.FromResult((DateTime?)DateTime.MaxValue);
        }
    }
}
