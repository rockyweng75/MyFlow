using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Service.Jobs;
using MyFlow.Service.Switchs;

namespace MyFlow.Service.Actions
{
    public class GenericAction
    {
        private IServiceProvider serviceProvider;
        private IJobLogService jobLogService;
        private ILogger<GenericAction> logger;
        private IApproveDataService approveDataService;
        private IApplyDataService applyDataService;

        public GenericAction(
            IServiceProvider serviceProvider, 
            ILogger<GenericAction> logger,
            IApplyDataService applyDataService,
            IApproveDataService approveDataService,
            IJobLogService jobLogService
            ) 
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.applyDataService = applyDataService;
            this.approveDataService = approveDataService;
            this.jobLogService = jobLogService;
        }
        public async Task DoJobsAsync(IList<ActionJobVM> jobs, FlowchartVM flowchart, StageVM stage, ApplyDataVM applyData, ApproveDataVM? approveData) 
        {
            var jobImpls = serviceProvider.GetServices<IJob>();
            foreach (var job in jobs)
            {
                var impl = jobImpls.Where(o => (int)o.JobType == job.JobType)
                    .Where(o => o.GetType().Name == job.JobClass)
                    .FirstOrDefault();

                var status = await impl!.Invoke(flowchart, stage, applyData, approveData);

                try
                {
                    await jobLogService.Create(new JobLogVM()
                    {
                        JobId = job.Id,
                        JobClass = job.JobClass,
                        StageId = stage.Id,
                        ActionId = job.Id,
                        FlowId = flowchart.Id,
                        ApplyId = applyData.Id,
                        ApprId = approveData is null ? null : approveData.Id,
                        ReexecuteDate = null,
                        ApproveUser = approveData is null ? null : approveData.UserId,
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
        public async Task DoJobsAsync(IList<StageJobVM> jobs, FlowchartVM flowchart, StageVM stage, ApplyDataVM applyData, ApproveDataVM? approveData) 
        {
            var jobImpls = serviceProvider.GetServices<IJob>();
            foreach (var job in jobs)
            {
                var impl = jobImpls.Where(o => (int)o.JobType == job.JobType)
                    .Where(o => o.GetType().Name == job.JobClass)
                    .FirstOrDefault();

                var status = await impl!.Invoke(flowchart, stage, applyData, approveData);

                try
                {
                    await jobLogService.Create(new JobLogVM()
                    {
                        JobId = job.Id,
                        JobClass = job.JobClass,
                        StageJobId = job.Id,
                        StageId = stage.Id,
                        FlowId = flowchart.Id,
                        ApplyId = applyData.Id,
                        ApprId = approveData is null ? null : approveData.Id,
                        ReexecuteDate = null,
                        ApproveUser = approveData is null ? null : approveData.UserId,
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

        public async Task<bool> InvokeSwitch(string switchClassName, FlowchartVM flowchart, StageVM stage, ApplyDataVM applyData, ApproveDataVM? approveData) 
        {
            var switchImpls = serviceProvider.GetServices<ISwitch>();

            var impl = switchImpls.Where(o => o.GetType().Name == switchClassName)
                    .FirstOrDefault();

            try 
            {
                return await impl!.Invoke(flowchart, stage, applyData, approveData);
            } 
            catch(Exception e)
            {
                logger.LogError("invoke switch throw exception", e);
                throw;
            }
        }

        public async Task DoAfterStageJob(StageVM currentStage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            var jobs = flowchart.StageJobList!.Where(o => o.StageId == currentStage.Id).ToList();
            await DoJobsAsync(jobs, flowchart, currentStage, applyData, approveData);
        }

        public async Task DoBeforeStageJob(StageVM nextStage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            var jobs = flowchart.StageJobList!.Where(o => o.StageId == nextStage.Id).ToList();
            await DoJobsAsync(jobs, flowchart, nextStage, applyData, approveData);
        }

        public Task<ActionFormVM?> FindActionForm(int ActionType, StageVM currentStage)
        {
            ActionFormVM? result = null;
            result = currentStage.ActionFormList!
                .Where(o => o.ActionType.HasValue && o.ActionType.Value == ActionType)
                .FirstOrDefault();

            return Task.FromResult(result);
        }

        public async Task DoAfterActionJob(
            ActionFormVM actionFormVM, 
            StageVM currentStage, 
            StageVM? nextStage,
            FlowchartVM flowchart, 
            ApplyDataVM applyData, 
            ApproveDataVM? approveData)
        {
            var jobs = flowchart.ActionJobList!.Where(o => o.ActionId == actionFormVM.Id).ToList();
            await DoJobsAsync(jobs, flowchart, currentStage, applyData, approveData);
        }

        public async Task DoBeforeActionJob(
                ActionFormVM actionFormVM, 
                StageVM nextStage, 
                FlowchartVM flowchart, 
                ApplyDataVM applyData, 
                ApproveDataVM? approveData)
        {
            var jobs = flowchart.ActionJobList!.Where(o => o.ActionId == actionFormVM.Id).ToList();
            await DoJobsAsync(jobs, flowchart, nextStage, applyData, approveData);
        }

        public async Task InsertApproveData(ApproveDataVM approveData)
        {
            await approveDataService.Create(approveData);
        }

        public async Task UpdateApplyData(ApplyDataVM applyDataVM)
        {
            await applyDataService.Update(applyDataVM);
        }
    }
}
