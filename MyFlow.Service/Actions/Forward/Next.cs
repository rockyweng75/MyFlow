using Microsoft.Extensions.Logging;
using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.Service.Actions.Forward
{
    public class Next : GenericForward, IForward
    {
        public Next(
            IServiceProvider serviceProvider, 
            ILogger<GenericForward> logger, 
            IApplyDataService applyDataService,
            IApproveDataService approveDataService,
            IJobLogService jobLogService,
            IStageService stageService,
            IStageRouteService stageRouteService
        ) : base(serviceProvider, logger, applyDataService, approveDataService, jobLogService, stageService, stageRouteService)
        {
        }

        public override async Task NextAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            ActionFormVM? actionform = null;

            if(currentStage.OrderId == 1)
            {
                actionform = await FindActionForm((int)ActionType.送出, currentStage);
            } 
            else 
            {
                actionform = await FindActionForm((int)ActionType.同意, currentStage);
            }

            await DoAfterStageJob(currentStage, flowchart, applyData, approveData);

            var stages = await FindNextStages(flowchart, currentStage, applyData, approveData);

            foreach(var stage in stages)
            {
                var target = await FindStageTarget(stage, flowchart, applyData, approveData);

                var deadline = await GetStageDeadline(flowchart, stage, applyData, approveData);

                var nextStageData = await InitNextStageData(flowchart, currentStage, stage, applyData);
                
                await InsertApproveData(nextStageData);

                await DoBeforeStageJob(stage, flowchart, applyData, approveData);

                if(actionform != null) 
                {
                    await DoAfterActionJob(actionform, currentStage, stage, flowchart, applyData, approveData);
                }
            }
        }
    }
}
