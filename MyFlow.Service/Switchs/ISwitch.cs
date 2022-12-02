using MyFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Service.Switchs
{
    public interface ISwitch
    {
        string Name { get; }
        Task<bool> Invoke(FlowchartVM flowchart, StageVM stage, ApplyDataVM applyData, ApproveDataVM? approveDataVM);
    }
}
