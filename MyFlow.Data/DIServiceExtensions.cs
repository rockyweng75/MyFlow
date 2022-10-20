using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Data.Connection;
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;

namespace MyFlow.Data
{
    public static class DIServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) 
        {
            services.AddDbContext<TestDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }

        public static void AddDAOs(this IServiceCollection services)
        {
			services.AddScoped<IActionFormDao, ActionFormDao>(injectDbContext<ActionFormDao>);
			services.AddScoped<IApplyDataDao, ApplyDataDao>(injectDbContext<ApplyDataDao>);
			services.AddScoped<IApproveDataDao, ApproveDataDao>(injectDbContext<ApproveDataDao>);
			services.AddScoped<IAttachmentDao, AttachmentDao>(injectDbContext<AttachmentDao>);
			services.AddScoped<IDeadlineDao, DeadlineDao>(injectDbContext<DeadlineDao>);
			services.AddScoped<IFlowchartDao, FlowchartDao>(injectDbContext<FlowchartDao>);
			services.AddScoped<IFormDao, FormDao>(injectDbContext<FormDao>);
			services.AddScoped<IFormItemDao, FormItemDao>(injectDbContext<FormItemDao>);
			services.AddScoped<IJobLogDao, JobLogDao>(injectDbContext<JobLogDao>);
			services.AddScoped<IJobDao, JobDao>(injectDbContext<JobDao>);
			services.AddScoped<IStageDao, StageDao>(injectDbContext<StageDao>);
			services.AddScoped<ISwitchDao, SwitchDao>(injectDbContext<SwitchDao>);
			services.AddScoped<IStageValidationDao, StageValidationDao>(injectDbContext<StageValidationDao>);
			services.AddScoped<ITestDao, TestDao>(injectDbContext<TestDao>);
		}

		private static TDao injectDbContext<TDao>(IServiceProvider serviceProvider) where TDao : class
		{
			var type = typeof(TDao);
			var dbContext = serviceProvider.GetService<TestDbContext>();
			TDao resultClass = (TDao)Activator.CreateInstance(type, dbContext);
			return resultClass;
		}
    }
}