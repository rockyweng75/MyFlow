
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IActionJobService : IBasicCRUDService<ActionJobVM> 
    { 
        Task<IList<ActionJobVM>> GetList(FlowchartVM flowchart);
    }

    public class ActionJobService : BasicCRUDService<ActionJobDao, ActionJob, ActionJobVM>, IActionJobService
    {
        private IActionJobDao jobDao;

        public override BasicDao<ActionJob> dao {
            get{
                return (BasicDao<ActionJob>)jobDao;
            }
        }

        public ActionJobService(IActionJobDao jobDao)
        {
            this.jobDao = jobDao;
        }

        public async Task<IList<ActionJobVM>> GetList(FlowchartVM flowchart)
        {
            var result = await jobDao.GetList(flowchart.ToDataModel<Flowchart>());
            return result!.Select(o => o.ToViewModel<ActionJobVM>()).ToList();
        }
    }
}
