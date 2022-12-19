using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;
using MyFlow.Service.Jobs;

namespace MyFlow.Service.Jobs.Mail
{

    public interface ISendToApplyUser : IJob
    {
    }

    public class SendToApplyUser : ISendToApplyUser
    {
        private ILogger<SendToApplyUser> logger;
        private IConfiguration configuration;

        public SendToApplyUser(
            ILogger<SendToApplyUser> logger,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        ///
        private const string formUser = "";

        private const string mailServer = "@test.test";


        public string Name => "通知使用者表單關閉";

        public JobType JobType => JobType.After;


        public Task<JobStatus> Invoke(
            FlowchartVM flowchart,
            StageVM stage,
            ApplyDataVM applyData,
            ApproveDataVM? approveData)
        {

            return Task.FromResult(new JobStatus(){
                Success = true,
                Message = "執行成功"
            });
        }
    }
}
