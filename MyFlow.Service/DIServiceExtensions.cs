using AspectCore.Configuration;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Service.Aop;
using MyFlow.Service.Impl;

namespace MyFlow.Service
{
    public static class DIServiceExtensions
    {

        public static void AddServices(this IServiceCollection services)
        {
			services.AddScoped<ActionFormService>();
			services.AddScoped<ApplyDataService>();
			services.AddScoped<ApproveDataService>();
			services.AddScoped<AttachmentService>();
			services.AddScoped<DeadlineService>();
			services.AddScoped<FlowchartService>();
			services.AddScoped<FormItemService>();
			services.AddScoped<FormService>();
			services.AddScoped<JobLogService>();
			services.AddScoped<JobService>();
			services.AddScoped<StageService>();
			services.AddScoped<StageValidationService>();
			services.AddScoped<SwitchService>();
			services.AddScoped<TestService>();
		}

		public static void AddAop(this IServiceCollection services) 
		{
			services.AddSingleton<IInterceptorCollector, InterceptorCollector>();
			services.AddSingleton<IPropertyInjectorFactory, PropertyInjectorFactory>();
			services.AddSingleton<LoggerAttribute>();
			services.AddScoped<TransactionAttribute>();

			services.ConfigureDynamicProxy(config => { 
				config.Interceptors.AddTyped<LoggerAttribute>(Predicates.ForService("*Service"));
				//config.Interceptors.AddTyped<TransactionAttribute>(Predicates.ForService("*Service"));
			});

			services.BuildDynamicProxyProvider();
		}
	}
}