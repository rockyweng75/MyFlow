using MyFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Service.Deadlines
{
    public interface IDeadline
    {

        public Task<DateTime?> Invoke(
              FlowchartVM flowchart,
              StageVM stage,
              ApplyDataVM applyData,
              ApproveDataVM approveData);
    }
}
