using MyFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Service.Targets
{
    public interface ITarget
    {
        string Name { get; }
        Task<string> Invoke(StageVM stage, ApplyDataVM? applyData, ApproveDataVM? approveDataVM);
    }
}
