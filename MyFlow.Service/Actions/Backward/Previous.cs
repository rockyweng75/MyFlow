using Microsoft.Extensions.Logging;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Domain.Enums;

namespace MyFlow.Service.Actions.Forward
{
    public class Previous : GenericBackward, IBackward
    {
        private IServiceProvider serviceProvider;
        public Previous(
            IServiceProvider serviceProvider,
            ILogger<GenericForward> logger,
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


        public override async Task PrevAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            ActionFormVM? actionform = await FindActionForm((int)ActionType.不同意, currentStage);
            
            var stages = await FindPrevStages(flowchart, currentStage, applyData, approveData);

            foreach(var stage in stages)
            {
                await DoAfterActionJob(actionform!, currentStage, stage, flowchart, applyData, approveData);

                var target = await FindStageTarget(stage, flowchart, applyData, approveData);

                var deadline = await GetStageDeadline(flowchart, stage, applyData, approveData);

                var nextStageData = await InitPrevStageData(flowchart, currentStage, stage, applyData);
                
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
