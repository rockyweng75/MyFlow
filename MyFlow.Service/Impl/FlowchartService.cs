
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;
using MyFlow.Domain.Tools;
using MyFlow.Service.Deadlines;
using MyFlow.Service.Targets;

namespace MyFlow.Service.Impl
{
    public interface IFlowchartService : IBasicCRUDService<FlowchartVM> 
    { 
        Task<FlowchartVM?> GetMix(int Id);
        Task<IList<FlowchartVM>> GetAll();
        Task<IList<FlowchartVM>> GetOpenFlowchart(
            IEnumerable<string>? roles = null,
            DateTime? queryDate = null);
    }

    public class FlowchartService : BasicCRUDService<FlowchartDao, Flowchart, FlowchartVM>, IFlowchartService
    {
        private ILogger<FlowchartService> logger;
        private IServiceProvider serviceProvider;
        private IFlowchartDao flowchartDao;
        private IStageDao stageDao;

        public override BasicDao<Flowchart> dao 
        {
            get{
                return (BasicDao<Flowchart>)flowchartDao;
            }
        }

        public FlowchartService(
            ILogger<FlowchartService> logger,
            IServiceProvider serviceProvider,
            IFlowchartDao flowchartDao,
            IStageDao stageDao
            )
        {
            this.flowchartDao = flowchartDao;
            this.stageDao = stageDao;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public async Task<FlowchartVM?> GetMix(int Id)
        {
            var flowchart = await flowchartDao.GetMix(Id);
            var result = flowchart!.ToViewModel<FlowchartVM>();
            result.StageList = flowchart!.StageList!.Select(o => o.ToViewModel<StageVM>()).ToList();
            return result;
        } 

        public async Task<IList<FlowchartVM>> GetAll()
        {
            var flowchart = await flowchartDao.GetAll();
            return flowchart.Select(o => o.ToViewModel<FlowchartVM>())
                            .ToList();
        } 

        public async Task<IList<FlowchartVM>> GetOpenFlowchart(
            IEnumerable<string>? roles = null,
            DateTime? queryDate = null)
        {
            var task = await flowchartDao.GetAll();

            logger.LogInformation("{flowchart}", 
                Newtonsoft.Json.JsonConvert.SerializeObject(task.Select(o => o.FlowName)));

            var list = task.Select(o =>{
                var result = o.ToViewModel<FlowchartVM>();
                result.StageList = new List<StageVM>(){ stageDao.GetFirst(o).Result!.ToViewModel<StageVM>() };
                return result;
            }).ToList();

            if (roles != null)
            {
                list = list.Where(o => filterByTarget(o, roles)).ToList();
            }

            list = list.Where(o => filterByDeadline(DateTime.Now.Year, o)).ToList();

            return list.ToList();
        }


        private bool filterByTarget(FlowchartVM flowchart, IEnumerable<string> roles)
        {
            if (flowchart.Target == null) return true;
            var target = serviceProvider
                            .GetServices(typeof(ITarget))
                            .Where(o => flowchart.Target == o!.GetType().Name)
                            .Cast<ITarget>()
                            .FirstOrDefault();

            string result = "";
            try
            {
                var stage = flowchart.StageList!.First();
                result = target!.Invoke(stage, null, null).Result;
            }
            catch (Exception er) 
            {
                logger.LogError(er, $"流程目標設定錯誤:{flowchart.Id}");
            }
            // anyone
            if (result == "*")
            {
                return true;
            }
            else
            {
                return roles.Any(r => r == result);
            }
        }

        private bool filterByDeadline(int year, FlowchartVM flowchart)
        {
            var today = DateTime.Now;
            try 
            {
                if (flowchart.StageList == null)
                {
                    return true;
                }
                else
                {
                    var firstStage = flowchart.StageList.First();
                    var deadline = firstStage.Deadline;
                    var deadlineClass = serviceProvider
                        .GetServices(typeof(IDeadline))
                        .Where(o => deadline == o!.GetType().Name)
                        .Cast<IDeadline>()
                        .First();

                    var startDate = deadlineClass.GetStartDateTime(year, flowchart, firstStage, null, null).Result;
                    var endDate = deadlineClass.GetEndDateTime(year, flowchart, firstStage, null, null).Result;

                    var result = false;
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        result = DateTime.Today.Between(startDate.Value, endDate.Value);
                    }
                    else if (startDate.HasValue)
                    {
                        result = DateTime.Today.AfterOrEqual(startDate.Value);
                    }
                    else if (endDate.HasValue)
                    {
                        result = DateTime.Today.BeforeOrEqual(endDate.Value);
                    }
                    if (endDate.HasValue)
                    {
                        // TODO
                        return result;
                    }
                    return result;
                }
            }
            catch (Exception er)
            {
                logger.LogError(er, $"流程開放時間設定錯誤:{flowchart.Id}");
            }

            return false;
        }
    }
}
