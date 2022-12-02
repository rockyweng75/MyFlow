
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Service.Impl;

namespace MyFlow.Service
{
	public static class DIServiceExtensions
	{

        public static void AddServices(this IServiceCollection services)
        {

			services.AddScoped<IActionFormService,ActionFormService>();
			services.AddScoped<IApplyDataService,ApplyDataService>();
			services.AddScoped<IApproveDataService,ApproveDataService>();
			services.AddScoped<IAttachmentService,AttachmentService>();
			services.AddScoped<IDeadlineService,DeadlineService>();
			services.AddScoped<IFlowchartService,FlowchartService>();
			services.AddScoped<IFormItemService,FormItemService>();
			services.AddScoped<IFormService,FormService>();
			services.AddScoped<IJobLogService,JobLogService>();
			services.AddScoped<IActionJobService,ActionJobService>();
			services.AddScoped<IStageJobService,StageJobService>();
			services.AddScoped<IStageService,StageService>();
			services.AddScoped<IStageValidationService,StageValidationService>();
			services.AddScoped<IStageRouteService,StageRouteService>();
			services.AddScoped<ITestService,TestService>();
		}
	}
}