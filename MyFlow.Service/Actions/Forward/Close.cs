using Microsoft.Extensions.Logging;
using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.Service.Actions.Forward
{
    public class Close : GenericForward, IForward
    {
        public Close(
            IServiceProvider serviceProvider, 
            ILogger<GenericForward> logger, 
            IApplyDataService applyDataService, 
            IApproveDataService approveDataService,
            IJobLogService jobLogService,
            IStageService stageService,
            IStageRouteService stageRouteService
        ) 
            : base(serviceProvider, logger, applyDataService, approveDataService, jobLogService, stageService, stageRouteService)
        {
        }

        public override async Task NextAction(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM? approveData)
        {
            ActionFormVM? actionform = await FindActionForm((int)ActionType.同意, currentStage);

            if(actionform != null) 
            {
                await DoAfterActionJob(actionform, currentStage, null, flowchart, applyData, approveData);
            }

            await DoAfterStageJob(currentStage, flowchart, applyData, approveData);

            applyData.StatusCode = (int)StatusCode.結案;
            applyData.UpdatedDate = DateTime.Today;
            await UpdateApplyData(applyData);
        }
    }
}
