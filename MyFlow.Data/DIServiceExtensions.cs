using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Data.Connection;
using MyFlow.Data.DAOs;

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
			services.AddScoped<IActionFormDao, ActionFormDao>();
			services.AddScoped<IApplyDataDao, ApplyDataDao>();
			services.AddScoped<IApproveDataDao, ApproveDataDao>();
			services.AddScoped<IAttachmentDao, AttachmentDao>();
			services.AddScoped<IDeadlineDao, DeadlineDao>();
			services.AddScoped<IFlowchartDao, FlowchartDao>();
			services.AddScoped<IFormDao, FormDao>();
			services.AddScoped<IFormItemDao, FormItemDao>();
			services.AddScoped<IJobLogDao, JobLogDao>();
			services.AddScoped<IJobDao, JobDao>();
			services.AddScoped<IStageDao, StageDao>();
			services.AddScoped<ISwitchDao, SwitchDao>();
			services.AddScoped<IStageValidationDao, StageValidationDao>();
			services.AddScoped<ITestDao, TestDao>();
		}
    }
}