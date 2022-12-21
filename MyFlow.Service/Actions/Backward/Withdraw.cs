using Microsoft.Extensions.Logging;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Domain.Enums;

namespace MyFlow.Service.Actions.Backward
{
    public class Withdraw : GenericBackward, IBackward
    {
        private IServiceProvider serviceProvider;
        public Withdraw(
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

        public override string Name => "撤回申請者";
        public override string Key => "Withdraw";

        public override async Task PrevAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            ActionFormVM? actionform = await FindActionForm(ActionType.不同意, currentStage);
            
            var stages = await FindPrevStages(flowchart, currentStage, applyData, approveData);
            if(stages.Count == 0) throw new Exception("找不到下一階段");

            // foreach(var stage in stages)
            // {
            //     await DoAfterActionJob(actionform!, currentStage, stage, flowchart, applyData, approveData);

            //     var target = await FindStageTarget(stage, flowchart, applyData, approveData);

            //     var deadline = await GetStageDeadline(flowchart, stage, applyData, approveData);

            //     var nextStageData = await InitPrevStageData(flowchart, currentStage, stage, applyData);
                
            //     await InsertApproveData(nextStageData);

            //     await DoBeforeStageJob(stage, flowchart, applyData, approveData);

            //     if(actionform != null) 
            //     {
            //         await DoAfterActionJob(actionform, currentStage, stage, flowchart, applyData, approveData);
            //     }
            // }
        }
    }
}
