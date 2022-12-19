using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Jobs
{
    public class JobStatus
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public string? Data { get; set; }
    }


    public interface IJob
    {
        public string Name { get; }

        public JobType JobType { get; }

        public Task<JobStatus> Invoke(
            FlowchartVM flowchart,
            StageVM stage,
            ApplyDataVM applyData,
            ApproveDataVM? approveData);
    }
}
