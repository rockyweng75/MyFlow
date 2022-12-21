
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Service.Actions.Backward;
using MyFlow.Service.Actions.Forward;
using MyFlow.Service.Actions.Submit;
using MyFlow.Service.Deadlines;
using MyFlow.Service.Impl;
using MyFlow.Service.Jobs;
using MyFlow.Service.Jobs.Mail;
using MyFlow.Service.Switchs;
using MyFlow.Service.Tags;
using MyFlow.Service.Targets;
using MyFlow.Service.Titles;

namespace MyFlow.Service
{
	public static class DIServiceExtensions
	{

        public static void AddServices(this IServiceCollection services)
        {
			services.AddScoped<IProcessService,ProcessService>();

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

		public static void AddActions(this IServiceCollection services)
		{
			services.AddScoped<IForward, Actions.Forward.Next>();
			services.AddScoped<IForward, Close>();

			services.AddScoped<IBackward, Previous>();
			services.AddScoped<IBackward, Withdraw>();


			services.AddScoped<ISubmit, Actions.Submit.Next>();

		}

		public static void AddJob(this IServiceCollection services)
		{
			services.AddScoped<IJob, SendToApplyUser>();
		}

		public static void AddSwitch(this IServiceCollection services)
		{
			services.AddScoped<ISwitch, AllPase>();
		}

		public static void AddTag(this IServiceCollection services)
		{
			services.AddScoped<ITag, Tags.A000>();
		}

		public static void AddTitle(this IServiceCollection services)
		{
			services.AddScoped<ITitle, Titles.A000>();
		}

		public static void AddTarget(this IServiceCollection services)
		{
			services.AddScoped<ITarget, PersonnelManager>();
			services.AddScoped<ITarget, CustomTarget>();
			services.AddScoped<ITarget, AnyOne>();
		}

		public static void AddDeadline(this IServiceCollection services)
		{
			services.AddScoped<IDeadline, AnyTime>();
		}

		public static void AddValidation(this IServiceCollection services)
		{
			// services.AddScoped<IValdateion, AnyTime>();
		}
	}
}