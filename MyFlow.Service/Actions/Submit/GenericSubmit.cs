using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFlow.Domain.Models;
using MyFlow.Service.Deadlines;
using MyFlow.Service.Targets;
using MyFlow.Service.Impl;
using MyFlow.Domain.Enums;

namespace MyFlow.Service.Actions.Submit
{
    public abstract class GenericSubmit : GenericAction, ISubmit
    {
        private IServiceProvider serviceProvider;
        private IStageRouteService stageRouteService;
        private IStageService stageService;

        public GenericSubmit(
            IServiceProvider serviceProvider,
            ILogger<GenericSubmit> logger,
            IApplyDataService applyDataService,
            IApproveDataService approveDataService,
            IJobLogService jobLogService,
            IStageService stageService,
            IStageRouteService stageRouteService
        ) : base(
                serviceProvider, 
                logger, 
                applyDataService, 
                approveDataService,
                jobLogService
            )
        {
            this.serviceProvider = serviceProvider;
            this.stageService = stageService;
            this.stageRouteService = stageRouteService;
        }

        public abstract string Name { get; }
        public abstract string Key { get; }

        public async Task<IList<StageVM>> FindNextStages(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            return await FindNextStages(ActionType.送出, flowchart, currentStage, applyData, approveData);
        }

        public async Task<string> FindStageTarget(StageVM stage, FlowchartVM flowchart, ApplyDataVM applyData, ApproveDataVM? approveData) 
        {
            var targetServices = serviceProvider.GetServices<ITarget>();
            if(targetServices == null || targetServices.Count() == 0) throw new Exception($"找不到對應的Target: {stage.Target}");

            var service = targetServices.Where(o => o.GetType().Name == stage.Target).FirstOrDefault();
            var result = service != null ?
                            await service.Invoke(stage, applyData, approveData) :
                            throw new Exception($"找不到對應的Target: {stage.Target}");
            return result;
        }

        public async Task<DateTime?> GetStageDeadline(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            var className = currentStage.Deadline;
            if(string.IsNullOrEmpty(currentStage.Deadline)) className = "AnyTime";
            var deadlines = serviceProvider.GetServices<IDeadline>();
            if(deadlines == null || deadlines.Count() == 0) throw new Exception($"找不到對應的Deadline: {currentStage.Deadline}");
            var deadline = deadlines.Where(o => o.GetType().Name == className).FirstOrDefault();
            var result = deadline != null ?
                            await deadline.GetEndDateTime(DateTime.Now.Year, flowchart, currentStage, applyData, approveData) :
                            throw new Exception($"找不到對應的Deadline: {currentStage.Deadline}");
            return result;
        }

        public async Task<ApproveDataVM> InitNextStageData(FlowchartVM flowchart, StageVM currentStage, StageVM nextStage, ApplyDataVM applyData, ApproveDataVM? approveData = null)
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

        public async Task Invoke(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData = null)
        {
            await NextAction(flowchart, currentStage, applyData, approveData);
        }

        public abstract Task NextAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData);
    }
}
