using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFlow.Domain.Models;
using MyFlow.Service.Deadlines;
using MyFlow.Service.Targets;
using MyFlow.Service.Impl;
using MyFlow.Domain.Enums;

namespace MyFlow.Service.Actions.Backward
{
    public abstract class GenericBackward : GenericAction, IBackward
    {
        private IServiceProvider serviceProvider;
        public GenericBackward(
            IServiceProvider serviceProvider,
            ILogger<GenericBackward> logger,
            IApplyDataService applyDataService,
            IApproveDataService approveDataService,
            IJobLogService jobLogService
        ) : base(
                serviceProvider, 
                logger, 
                applyDataService, 
                approveDataService,
                jobLogService
            )
        {
            this.serviceProvider = serviceProvider;
        }

        public abstract string Name { get; }
        public abstract string Key { get; }

        public async Task<IList<StageVM>> FindPrevStages(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData) 
        {
            if (flowchart.StageList is null || flowchart.StageList.Count == 0) throw new Exception("找不到階段資料");
            if (flowchart.StageRouteList is null || flowchart.StageRouteList.Count == 0) throw new Exception("找不到路由資料");

            var stageList = flowchart.StageList;
            var result = new List<StageVM>();

            var currentStageRoute = flowchart.StageRouteList
                                        .Where(o => o.StageId == currentStage.Id)
                                        .Where(o => o.ActionType == (int)ActionType.不同意)
                                        .OrderBy(o => o.NextStageId)
                                        .AsEnumerable();
                                        
            foreach (var @switch in currentStageRoute) 
            {
                if (!string.IsNullOrEmpty(@switch.SwitchClass))
                {
                    if(await InvokeSwitch(@switch.SwitchClass, flowchart, currentStage, applyData, approveData))
                    {
                        var stage = stageList.Where(o => o.Id == @switch.NextStageId).FirstOrDefault();
                        if (stage != null) result.Add(stage);
                    }
                }
                else 
                {
                    var stage = stageList.Where(o => o.Id == @switch.NextStageId).FirstOrDefault();
                    if (stage != null) result.Add(stage);
                }
            }
            return result;
        }

        public async Task<string> FindStageTarget(StageVM stage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM? approveData) 
        {
            var targetServices = serviceProvider.GetServices<ITarget>();

            var service = targetServices.Where(o => o.GetType().Name == stage.Target).FirstOrDefault();
            var result = service != null ?
                            await service.Invoke(stage, applyData, approveData) :
                            throw new Exception($"找不到對應的Target: {stage.Target}");
            return result;
        }

        public async Task<DateTime?> GetStageDeadline(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {

            var deadlines = serviceProvider.GetServices<IDeadline>();

            var deadline = deadlines.Where(o => o.GetType().Name == currentStage.Deadline).FirstOrDefault();
            var result = deadline != null ?
                            await deadline.GetEndDateTime(DateTime.Now.Year, flowchart, currentStage, applyData, approveData) :
                            throw new Exception($"找不到對應的Deadline: {currentStage.Deadline}");
            return result;
        }

        public async Task<ApproveDataVM> InitPrevStageData(FlowchartVM flowchart, StageVM currentStage, StageVM nextStage, ApplyDataVM applyData, ApproveDataVM? approveData = null)
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
                    StatusCode = null
                };
            };

            return approveData;
        }

        public async Task Invoke(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            await PrevAction(flowchart, currentStage, applyData, approveData);
        }
        public abstract Task PrevAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData);
    }
}
