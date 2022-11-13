using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFlow.Domain.Models;
using MyFlow.Service.Deadlines;
using MyFlow.Service.Jobs;
using MyFlow.Service.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Service.Actions.Forward
{
    public class GenericForward : GenericAction, IForward
    {
        private IServiceProvider serviceProvider;
        public GenericForward(
            IServiceProvider serviceProvider,
            ILogger<GenericForward> logger
        ) : base(serviceProvider, logger)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<IList<StageVM>> FindNextStages(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM approveData) 
        {
            if (flowchart.StageList.Count == 0) throw new Exception("找不到階段資料");
            if (flowchart.SwitchList.Count == 0) throw new Exception("找不到路由資料");

            var stageList = flowchart.StageList;
            var result = new List<StageVM>();
            foreach (var @switch in flowchart.SwitchList.Where(o => o.StageId > currentStage.Id).OrderBy(o => o.OrderId)) 
            {
                if (!string.IsNullOrEmpty(@switch.DecisionClass))
                {
                    // TODO 
                    await Task.FromResult("");
                }
                else 
                {
                    var stage = stageList.Where(o => o.Id == @switch.NextStageId).FirstOrDefault();
                    if (stage != null) result.Add(stage);
                }
            }
            return result;
        }

        public async Task<string> FindStageTarget(StageVM stage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM approveData) 
        {
            var targetServices = serviceProvider.GetServices<ITarget>();

            var service = targetServices.Where(o => o.GetType().Name == stage.Target).FirstOrDefault();
            var result = service != null ?
                            await service.Invoke(stage, applyData, approveData) :
                            throw new Exception($"找不到對應的Target: {stage.Target}");
            return result;
        }

        public async Task DoAfterJob(StageVM currentStage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM approveData)
        {
            var jobs = flowchart.JobList.Where(o => o.StageId == currentStage.Id).ToList();
            await doJobsAsync(jobs, flowchart, currentStage, applyData, approveData);
        }

        public async Task DoBeforeJob(StageVM nextStage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM approveData)
        {
            var jobs = flowchart.JobList.Where(o => o.StageId == nextStage.Id).ToList();
            await doJobsAsync(jobs, flowchart, nextStage, applyData, approveData);
        }

        public async Task<DateTime?> GetStageDeadline(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM approveData)
        {

            var deadlines = serviceProvider.GetServices<IDeadline>();

            var deadline = deadlines.Where(o => o.GetType().Name == currentStage.Deadline).FirstOrDefault();
            var result = deadline != null ?
                            await deadline.Invoke(flowchart, currentStage, applyData, approveData) :
                            throw new Exception($"找不到對應的Deadline: {currentStage.Deadline}");

            return result;
        }

        public async Task<ApproveDataVM> InitNextStageData(FlowchartVM flowchart, StageVM currentStage, StageVM nextStage, ApplyDataVM applyData, ApproveDataVM approveData = null)
        {
            var closeDate = await GetStageDeadline(flowchart, currentStage, applyData, approveData);

            var nextUser = await FindStageTarget(nextStage, flowchart, applyData, approveData);

            if (approveData == null) 
            {
                approveData = new ApproveDataVM()
                {
                    ApplyId = applyData.Id,
                    StageId = nextStage.Id,
                    FlowId = flowchart.Id,
                    StageName = nextStage.StageName,
                    CloseDate = closeDate,
                    FlowName = flowchart.FlowName,
                    UserId = nextUser,
                    StatusCode = "Y"
                };
            };

            return approveData;
        }

        public StageVM NextAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM approveData)
        {
            return null;
        }
    }
}
