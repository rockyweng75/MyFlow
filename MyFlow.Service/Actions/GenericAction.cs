using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Service.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Service.Actions
{
    public class GenericAction
    {
        private IServiceProvider serviceProvider;
        private IJobLogService jobLogService;
        private ILogger<GenericAction> logger;

        public GenericAction(IServiceProvider serviceProvider, ILogger<GenericAction> logger) 
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }
        public async Task doJobsAsync(IList<JobVM> jobs, FlowchartVM flowchart, StageVM stage, ApplyDataVM applyData, ApproveDataVM approveData) 
        {
            var jobImpls = serviceProvider.GetServices<IJob>();
            foreach (var job in jobs)
            {
                var impl = jobImpls.Where(o => (int)o.JobType == job.JobType)
                    .Where(o => o.GetType().Name == job.JobClass)
                    .FirstOrDefault();

                var status = await impl.Invoke(flowchart, stage, applyData, approveData);

                try
                {
                    await jobLogService.Create(new JobLogVM()
                    {
                        JobId = job.Id,
                        JobClass = job.JobClass,
                        StageId = stage.Id,
                        FlowId = flowchart.Id,
                        ApplyId = applyData.Id,
                        ApprId = approveData.Id,
                        ReexecuteDate = null,
                        ApproveUser = approveData.UserId,
                        ApplyUser = applyData.ApplyUser,
                        CreatedDate = DateTime.Now,
                        FlowName = flowchart.FlowName,
                        StageName = stage.StageName,
                        Message = status.Message,
                        Success = status.Success ? 1 : -1,
                    });
                }
                catch (Exception e) 
                {
                    logger.LogError("insert JobLog throw exception", e);
                }
            }
        }
    }
}
